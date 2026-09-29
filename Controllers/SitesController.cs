using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Bfn.DevOps.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bfn.DevOps.Controllers;

[Authorize(Roles = DatabaseSeeder.AdministratorRole)]
public sealed class SitesController(IIisService iis) : Controller
{
    public IActionResult Index()
    {
        try { return View(iis.GetSites()); }
        catch (Exception ex) { ViewBag.IisError = ex.Message; return View(Array.Empty<IisSiteInfo>()); }
    }
    public IActionResult Create() => View(new CreateSiteViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create(CreateSiteViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        try { iis.CreateSite(model); TempData["Message"] = "IIS sitesi ve application pool oluşturuldu."; return RedirectToAction(nameof(Index)); }
        catch (Exception ex) { ModelState.AddModelError("", ex.Message); return View(model); }
    }
}
