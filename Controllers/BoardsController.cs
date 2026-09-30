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
    private async Task<Guid> BoardUIdAsync(int boardId) => await db.Boards.Where(x => x.Id == boardId).Select(x => x.UId).FirstAsync();

    public async Task<IActionResult> Index(Guid? id, int? openCard = null)
    {
        int boardId;
        if (id is null) { boardId = await db.Boards.OrderBy(x => x.SortOrder).Select(x => x.Id).FirstAsync(); }
        else
        {
            var found = await db.Boards.Where(x => x.UId == id).Select(x => (int?)x.Id).FirstOrDefaultAsync();
            if (found is null) return NotFound();
            boardId = found.Value;
        }
        var board = await db.Boards.AsNoTracking()
            .Include(x => x.Columns.OrderBy(c => c.SortOrder)).ThenInclude(c => c.Cards.Where(cd => !cd.IsDeleted).OrderBy(cd => cd.SortOrder)).ThenInclude(cd => cd.AssignedUser)
            .Include(x => x.Columns.OrderBy(c => c.SortOrder)).ThenInclude(c => c.Cards.Where(cd => !cd.IsDeleted).OrderBy(cd => cd.SortOrder)).ThenInclude(cd => cd.Subtasks.OrderBy(s => s.SortOrder))
            .FirstOrDefaultAsync(x => x.Id == boardId);
        if (board is null) return NotFound();
        ViewData["OpenCard"] = openCard;
        ViewData["ArchivedCount"] = await db.BoardCards.CountAsync(x => x.Column.BoardId == boardId && x.IsDeleted);
        return View(board);
    }

    public async Task<IActionResult> ArchivedCards(int boardId)
    {
        var cards = await db.BoardCards.AsNoTracking()
            .Include(x => x.Column)
            .Where(x => x.Column.BoardId == boardId && x.IsDeleted)
            .OrderByDescending(x => x.ModDate)
            .ToListAsync();
        return PartialView("_ArchivedCards", cards);
    }

    public async Task<IActionResult> CardDetail(int id)
    {
        var card = await db.BoardCards.AsNoTracking()
            .Include(x => x.Column)
            .Include(x => x.AssignedUser).Include(x => x.CreatedByUser)
            .Include(x => x.Comments.OrderBy(c => c.CreDate)).ThenInclude(c => c.Author)
            .Include(x => x.Subtasks.OrderBy(s => s.SortOrder))
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
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(column.BoardId) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddColumn(int boardId, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 80) return BadRequest();
        if (!await db.Boards.AnyAsync(x => x.Id == boardId)) return NotFound();
        var order = (await db.BoardColumns.Where(x => x.BoardId == boardId).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1;
        db.BoardColumns.Add(new BoardColumn { BoardId = boardId, Name = name.Trim(), SortOrder = order });
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(boardId) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RenameColumn(int id, string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 80) return BadRequest();
        var column = await db.BoardColumns.FindAsync(id); if (column is null) return NotFound();
        column.Name = name.Trim();
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(column.BoardId) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteColumn(int id)
    {
        var column = await db.BoardColumns.FindAsync(id); if (column is null) return NotFound();
        var boardId = column.BoardId; db.Remove(column); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(boardId) });
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
        card.BoardColumnId = target.Id; card.SortOrder = Math.Max(0, request.SortOrder); card.ModDate = DateTime.UtcNow;
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
        card.AttachmentUrl = string.IsNullOrWhiteSpace(model.AttachmentUrl) ? null : model.AttachmentUrl.Trim();
        card.AttachmentLabel = string.IsNullOrWhiteSpace(model.AttachmentLabel) ? null : model.AttachmentLabel.Trim();
        card.ModDate = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId), openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddSubtask(int cardId, string title)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Length > 200) return BadRequest();
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == cardId);
        if (card is null) return NotFound();
        var order = (await db.BoardCardSubtasks.Where(x => x.BoardCardId == cardId).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1;
        db.BoardCardSubtasks.Add(new BoardCardSubtask { BoardCardId = cardId, Title = title.Trim(), SortOrder = order });
        card.ModDate = DateTime.UtcNow;
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId), openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleSubtask(int id)
    {
        var subtask = await db.BoardCardSubtasks.FirstOrDefaultAsync(x => x.Id == id);
        if (subtask is null) return NotFound();
        subtask.IsDone = !subtask.IsDone;
        await db.SaveChangesAsync();
        return Ok(new { done = subtask.IsDone });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteSubtask(int id)
    {
        var subtask = await db.BoardCardSubtasks.Include(x => x.Card).ThenInclude(c => c.Column).FirstOrDefaultAsync(x => x.Id == id);
        if (subtask is null) return NotFound();
        var card = subtask.Card;
        db.Remove(subtask);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId), openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(BoardCardCommentViewModel model)
    {
        if (!ModelState.IsValid || string.IsNullOrWhiteSpace(model.Body)) return BadRequest();
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == model.BoardCardId);
        if (card is null) return NotFound();
        db.BoardCardComments.Add(new BoardCardComment { BoardCardId = card.Id, Body = model.Body.Trim(), AuthorUserId = users.GetUserId(User) });
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId), openCard = card.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ArchiveCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        card.IsDeleted = true; card.ModDate = DateTime.UtcNow; card.DelDate = DateTime.UtcNow; card.DelUser = users.GetUserId(User);
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> RestoreCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        card.IsDeleted = false; card.ModDate = DateTime.UtcNow; card.DelDate = null; card.DelUser = null;
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(card.Column.BoardId) });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCard(int id)
    {
        var card = await db.BoardCards.Include(x => x.Column).FirstOrDefaultAsync(x => x.Id == id); if (card is null) return NotFound();
        var boardId = card.Column.BoardId; db.Remove(card); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index), new { id = await BoardUIdAsync(boardId) });
    }
}

public sealed record MoveCardRequest(int CardId, int ColumnId, int SortOrder);
