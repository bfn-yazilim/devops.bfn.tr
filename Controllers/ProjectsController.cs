using System.Text.Json;
using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Controllers;

[Authorize(Roles = DatabaseSeeder.AdministratorRole)]
public sealed class ProjectsController(ApplicationDbContext db, Bfn.DevOps.Services.IIisService iis) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Projects.AsNoTracking().OrderBy(x => x.Name).ToListAsync());
    public IActionResult Create() => View(new ProjectEditViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectEditViewModel model)
    {
        model.IisSiteName = model.Name.Trim();
        model.ApplicationPoolName = model.Name.Trim();
        ModelState.Remove(nameof(model.IisSiteName));
        ModelState.Remove(nameof(model.ApplicationPoolName));
        if (!ModelState.IsValid) return View(model);
        var name = model.Name.Trim();
        if (await db.Projects.AnyAsync(x => x.Name == name)) { ModelState.AddModelError(nameof(model.Name), "Bu proje adı zaten kullanılıyor."); return View(model); }
        var project = new DevOpsProject { Name = name, IisSiteName = name, ApplicationPoolName = name, ZeroDowntimeEnabled = true };
        db.Projects.Add(project);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Edit), new { id = project.UId });
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var p = await db.Projects.FirstOrDefaultAsync(x => x.UId == id);
        if (p is null) return NotFound();
        return View(new ProjectEditViewModel { Id = p.Id, UId = p.UId, Name = p.Name, Url = p.Url, IisSiteName = p.IisSiteName, ApplicationPoolName = p.ApplicationPoolName, RepositoryUrl = p.RepositoryUrl, WorkingDirectory = p.WorkingDirectory, ZeroDowntimeEnabled = p.ZeroDowntimeEnabled });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ProjectEditViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var p = await db.Projects.FindAsync(model.Id);
        if (p is null) return NotFound();
        if (await db.Projects.AnyAsync(x => x.Id != model.Id && x.Name == model.Name.Trim())) { ModelState.AddModelError(nameof(model.Name), "Bu proje adı zaten kullanılıyor."); return View(model); }
        p.Name = model.Name.Trim(); p.Url = model.Url?.Trim(); p.IisSiteName = model.IisSiteName.Trim();
        p.ApplicationPoolName = model.ApplicationPoolName.Trim(); p.RepositoryUrl = model.RepositoryUrl?.Trim();
        p.WorkingDirectory = model.WorkingDirectory?.Trim(); p.ZeroDowntimeEnabled = model.ZeroDowntimeEnabled; p.ModDate = DateTime.UtcNow;
        await db.SaveChangesAsync(); TempData["Message"] = "Proje güncellendi.";
        return RedirectToAction(nameof(Edit), new { id = p.UId });
    }

    public async Task<IActionResult> Steps(Guid id)
    {
        var project = await db.Projects.Include(x => x.DeploymentSteps.OrderBy(s => s.SortOrder)).FirstOrDefaultAsync(x => x.UId == id);
        return project is null ? NotFound() : View(project);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ApplyIis(int id)
    {
        var project = await db.Projects.FindAsync(id); if (project is null) return NotFound();
        try { iis.ApplyProject(project); TempData["Message"] = "IIS sitesi ve Application Pool ayarları uygulandı."; }
        catch (Exception ex) { TempData["Error"] = ex.Message; }
        return RedirectToAction(nameof(Edit), new { id = project.UId });
    }

    public async Task<IActionResult> StepEdit(Guid projectId, Guid? id)
    {
        var project = await db.Projects.FirstOrDefaultAsync(x => x.UId == projectId);
        if (project is null) return NotFound();
        if (id is null) return View(new DeploymentStepEditViewModel { ProjectId = project.Id, ProjectUId = project.UId });
        var step = await db.DeploymentSteps.FirstOrDefaultAsync(x => x.UId == id && x.ProjectId == project.Id);
        if (step is null) return NotFound();
        var model = new DeploymentStepEditViewModel { Id = step.Id, ProjectId = step.ProjectId, ProjectUId = project.UId, Name = step.Name, Type = step.Type, TimeoutSeconds = step.TimeoutSeconds, IsEnabled = step.IsEnabled, ContinueOnError = step.ContinueOnError, SettingsJson = step.SettingsJson };
        if (step.Type == DeploymentStepType.GitClone)
        {
            var git = JsonSerializer.Deserialize<GitCloneSettings>(step.SettingsJson) ?? new GitCloneSettings();
            model.GitRepositoryUrl = git.RepositoryUrl; model.GitUsername = git.Username; model.GitPassword = git.Password; model.GitBranch = git.Branch;
        }
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> StepEdit(DeploymentStepEditViewModel model)
    {
        if (model.Type == DeploymentStepType.GitClone)
        {
            ModelState.Remove(nameof(model.SettingsJson));
            model.SettingsJson = JsonSerializer.Serialize(new GitCloneSettings { RepositoryUrl = model.GitRepositoryUrl?.Trim() ?? "", Username = model.GitUsername?.Trim() ?? "", Password = model.GitPassword ?? "", Branch = model.GitBranch?.Trim() ?? "" });
        }
        else { try { JsonDocument.Parse(model.SettingsJson); } catch (JsonException) { ModelState.AddModelError(nameof(model.SettingsJson), "Geçerli bir JSON girin."); } }
        if (!ModelState.IsValid) return View(model);
        DeploymentStep step;
        if (model.Id == 0)
        {
            if (!await db.Projects.AnyAsync(x => x.Id == model.ProjectId)) return NotFound();
            step = new DeploymentStep { ProjectId = model.ProjectId, SortOrder = (await db.DeploymentSteps.Where(x => x.ProjectId == model.ProjectId).MaxAsync(x => (int?)x.SortOrder) ?? -1) + 1 };
            db.DeploymentSteps.Add(step);
        }
        else { step = await db.DeploymentSteps.FirstOrDefaultAsync(x => x.Id == model.Id && x.ProjectId == model.ProjectId) ?? throw new InvalidOperationException("Adım bulunamadı."); }
        step.Name = model.Name.Trim(); step.Type = model.Type; step.TimeoutSeconds = model.TimeoutSeconds; step.IsEnabled = model.IsEnabled; step.ContinueOnError = model.ContinueOnError; step.SettingsJson = model.SettingsJson;
        await db.SaveChangesAsync(); return RedirectToAction(nameof(Steps), new { id = model.ProjectUId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteStep(int id)
    {
        var step = await db.DeploymentSteps.Include(x => x.Project).FirstOrDefaultAsync(x => x.Id == id); if (step is null) return NotFound();
        var projectUId = step.Project.UId; db.Remove(step); await db.SaveChangesAsync(); return RedirectToAction(nameof(Steps), new { id = projectUId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> ReorderSteps(int projectId, [FromBody] int[] ids)
    {
        var steps = await db.DeploymentSteps.Where(x => x.ProjectId == projectId).ToListAsync();
        if (ids.Length != steps.Count || ids.Distinct().Count() != ids.Length || ids.Any(id => steps.All(x => x.Id != id))) return BadRequest();
        for (var i = 0; i < ids.Length; i++) steps.Single(x => x.Id == ids[i]).SortOrder = i;
        await db.SaveChangesAsync(); return Ok();
    }
}
