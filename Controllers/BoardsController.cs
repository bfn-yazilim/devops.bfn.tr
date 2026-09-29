using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Controllers;

[Authorize]
public sealed class BoardsController(ApplicationDbContext db, UserManager<ApplicationUser> users) : Controller
{
    public async Task<IActionResult> Index(int? id, int? openCard = null)
    {
        var boardId = id ?? await db.Boards.OrderBy(x => x.SortOrder).Select(x => x.Id).FirstAsync();
        var board = await db.Boards.AsNoTracking()
            .Include(x => x.Columns.OrderBy(c => c.SortOrder)).ThenInclude(c => c.Cards.Where(cd => !cd.IsArchived).OrderBy(cd => cd.SortOrder)).ThenInclude(cd => cd.AssignedUser)
            .FirstOrDefaultAsync(x => x.Id == boardId);
        if (board is null) return NotFound();
        ViewData["OpenCard"] = openCard;
        ViewData["ArchivedCount"] = await db.BoardCards.CountAsync(x => x.Column.BoardId == boardId && x.IsArchived);
        return View(board);
    }

    public async Task<IActionResult> ArchivedCards(int boardId)
    {
        var cards = await db.BoardCards.AsNoTracking()
            .Include(x => x.Column)
            .Where(x => x.Column.BoardId == boardId && x.IsArchived)
            .OrderByDescending(x => x.UpdatedAtUtc)
            .ToListAsync();
        return PartialView("_ArchivedCards", cards);
    }

    public async Task<IActionResult> CardDetail(int id)
    {
        var card = await db.BoardCards.AsNoTracking()
            .Include(x => x.Column)
            .Include(x => x.AssignedUser).Include(x => x.CreatedByUser)
            .Include(x => x.Comments.OrderBy(c => c.CreatedAtUtc)).ThenInclude(c => c.Author)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (card is null) return NotFound();
        var columns = await db.BoardColumns.AsNoTracking().Where(x => x.BoardId == card.Column.BoardId).OrderBy(x => x.SortOrder).ToListAsync();
        var allUsers = await users.Users.AsNoTracking().OrderBy(x => x.UserName).ToListAsync();
        return PartialView("_CardDetail", new BoardCardDetailViewModel { Card = card, Columns = columns, Users = allUsers });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCard(int columnId, string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > 200) return BadRequest();
        var column = await db.BoardColumns.FindAsync(columnId); if (column is null) return NotFound();
        var order = (await db.BoardCards.Where(x => x.BoardColumnId == columnId).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1;
        db.BoardCards.Add(new BoardCard { BoardColumnId = columnId, Title = title.Trim(), SortOrder = order, CreatedByUserId = users.GetUserId(User) });
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = column.BoardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddColumn(int boardId, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 80) return BadRequest();
        if (!await db.Boards.AnyAsync(x => x.Id == boardId)) return NotFound();
        var order = (await db.BoardColumns.Where(x => x.BoardId == boardId).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1;
        db.BoardColumns.Add(new BoardColumn { BoardId = boardId, Name = name.Trim(), SortOrder = order });
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = boardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RenameColumn(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 80) return BadRequest();
        var column = await db.BoardColumns.FindAsync(id); if (column is null) return NotFound();
        column.Name = name.Trim();
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = column.BoardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteColumn(int id)
    {
        var column = await db.BoardColumns.FindAsync(id); if (column is null) return NotFound();
        var boardId = column.BoardId; db.Remove(column); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = boardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ReorderColumns(int boardId, [FromBody] int[] ids)
    {
        var columns = await db.BoardColumns.Where(x => x.BoardId == boardId).ToListAsync();
        if (ids.Length != columns.Count || ids.Distinct().Count() != ids.Length || ids.Any(id => columns.All(x => x.Id != id))) return BadRequest();
        for (var i = 0; i < ids.Length; i++) columns.Single(x => x.Id == ids[i]).SortOrder = i;
        await db.SaveChangesAsync(); return Ok();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> MoveCard([FromBody] MoveCardRequest request)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == request.CardId);
        var target = await db.BoardColumns.FindAsync(request.ColumnId);
        if (card is null || target is null || card.Column.BoardId != target.BoardId) return BadRequest();
        card.BoardColumnId = target.Id; card.SortOrder = Math.Max(0, request.SortOrder); card.UpdatedAtUtc = DateTime.UtcNow;
        var siblings = await db.BoardCards.Where(x => x.BoardColumnId == target.Id && x.Id != card.Id).OrderBy(x => x.SortOrder).ToListAsync();
        siblings.Insert(Math.Min(card.SortOrder, siblings.Count), card);
        for (var i = 0; i < siblings.Count; i++) siblings[i].SortOrder = i;
        await db.SaveChangesAsync(); return Ok();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateCard(BoardCardUpdateViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest();
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == model.Id);
        if (card is null) return NotFound();
        var targetColumn = await db.BoardColumns.FindAsync(model.BoardColumnId);
        if (targetColumn is null || targetColumn.BoardId != card.Column.BoardId) return BadRequest();
        if (card.BoardColumnId != targetColumn.Id)
        {
            var order = (await db.BoardCards.Where(x => x.BoardColumnId == targetColumn.Id).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1;
            card.BoardColumnId = targetColumn.Id;
            card.SortOrder = order;
        }
        card.Title = model.Title.Trim();
        card.Description = string.IsNullOrWhiteSpace(model.Description) ? null : model.Description.Trim();
        card.Priority = model.Priority;
        card.DueAtUtc = model.DueAtUtc;
        card.Tags = string.IsNullOrWhiteSpace(model.Tags)
            ? null
            : string.Join(", ", model.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        card.AssignedUserId = string.IsNullOrWhiteSpace(model.AssignedUserId) ? null : model.AssignedUserId;
        card.UpdatedAtUtc = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = card.Column.BoardId, openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(BoardCardCommentViewModel model)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Body)) return BadRequest();
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == model.BoardCardId);
        if (card is null) return NotFound();
        db.BoardCardComments.Add(new BoardCardComment { BoardCardId = card.Id, Body = model.Body.Trim(), AuthorUserId = users.GetUserId(User) });
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = card.Column.BoardId, openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ArchiveCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        card.IsArchived = true; card.UpdatedAtUtc = DateTime.UtcNow;
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = card.Column.BoardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RestoreCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        card.IsArchived = false; card.UpdatedAtUtc = DateTime.UtcNow;
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = card.Column.BoardId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        var boardId = card.Column.BoardId; db.Remove(card); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = boardId });
    }
}

public sealed record MoveCardRequest(int CardId, int ColumnId, int SortOrder);
