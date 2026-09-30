using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Controllers;

[Authorize(Roles = DatabaseSeeder.AdministratorRole)]
public sealed class UsersController(UserManager<ApplicationUser> users) : Controller
{
    public async Task<IActionResult> Index() => View(await users.Users.OrderByDescending(x => x.IsFounder).ThenBy(x => x.UserName).ToListAsync());
    public IActionResult Create() => View(new CreateUserViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateUserViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = new ApplicationUser { UserName = model.UserName.Trim(), MustChangePassword = true };
        var result = await users.CreateAsync(user, model.Password);
        if (result.Succeeded && model.IsAdministrator) result = await users.AddToRoleAsync(user, DatabaseSeeder.AdministratorRole);
        if (result.Succeeded) { TempData["Message"] = "Kullanıcı oluşturuldu."; return RedirectToAction(nameof(Index)); }
        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
        return View(model);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await users.FindByIdAsync(id);
        if (user is null) return NotFound();
        if (user.IsFounder) { TempData["Error"] = "Kurucu kullanıcı silinemez."; return RedirectToAction(nameof(Index)); }
        if (user.Id == users.GetUserId(User)) { TempData["Error"] = "Kendi hesabınızı silemezsiniz."; return RedirectToAction(nameof(Index)); }
        var result = await users.DeleteAsync(user);
        TempData[result.Succeeded ? "Message" : "Error"] = result.Succeeded ? "Kullanıcı silindi." : "Kullanıcı silinemedi.";
        return RedirectToAction(nameof(Index));
    }
}
