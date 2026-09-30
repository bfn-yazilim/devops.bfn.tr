using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Bfn.DevOps.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Controllers;

[Authorize(Roles = DatabaseSeeder.AdministratorRole)]
public sealed class DeploymentsController(ApplicationDbContext db, IDeploymentRunner runner) : Controller
{
    public async Task<IActionResult> Index()
    {
        var projects = await db.Projects.AsNoTracking().OrderBy(x => x.Name).ToListAsync();
        var latestRuns = await db.DeploymentRuns.AsNoTracking()
            .Where(x => projects.Select(p => p.Id).Contains(x.ProjectId))
            .GroupBy(x => x.ProjectId)
            .Select(g => g.OrderByDescending(x => x.Id).First())
            .ToListAsync();
        var items = projects.Select(p => new ProjectListItemViewModel { Project = p, LatestRun = latestRuns.FirstOrDefault(r => r.ProjectId == p.Id) }).ToList();
        return View(items);
    }

    public async Task<IActionResult> Detail(Guid id)
    {
        var project = await db.Projects.Include(x => x.DeploymentSteps.OrderBy(s => s.SortOrder)).ThenInclude(s => s.StepType).FirstOrDefaultAsync(x => x.UId == id);
        if (project is null) return NotFound();
        var latestRun = await db.DeploymentRuns.AsNoTracking().Where(x => x.ProjectId == project.Id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        var model = new DeploymentDetailViewModel { Project = project, Steps = project.DeploymentSteps, LatestRun = latestRun };
        if (latestRun is not null)
        {
            var stepStatuses = await db.DeploymentRunSteps.AsNoTracking().Where(x => x.DeploymentRunId == latestRun.Id).ToListAsync();
            model.StepStatuses = stepStatuses.ToDictionary(x => x.DeploymentStepId, x => x.Status);
        }
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> StartDeployment(Guid id)
    {
        var project = await db.Projects.FirstOrDefaultAsync(x => x.UId == id);
        if (project is null) return NotFound();
        var alreadyRunning = await db.DeploymentRuns.AnyAsync(x => x.ProjectId == project.Id && x.Status == "Running");
        if (alreadyRunning) { TempData["Error"] = "Bu proje için zaten devam eden bir güncelleme var."; return RedirectToAction(nameof(Index)); }
        var run = new DeploymentRun { ProjectId = project.Id, Status = "Running", StartedAtUtc = DateTime.UtcNow, RequestedByUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "" };
        db.DeploymentRuns.Add(run);
        await db.SaveChangesAsync();
        runner.QueueRun(run.Id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RunStatus(int id)
    {
        var run = await db.DeploymentRuns.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        if (run is null) return NotFound();
        return Json(new { status = run.Status, log = run.Log });
    }
}
