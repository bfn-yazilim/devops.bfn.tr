using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bfn.DevOps.Controllers;

public sealed class AccountController(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn) : Controller
{
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Boards");
        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await signIn.PasswordSignInAsync(model.UserName, model.Password, false, true);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", result.IsLockedOut ? "Hesap geçici olarak kilitlendi." : "Kullanıcı adı veya parola hatalı.");
            return View(model);
        }
        var user = await users.FindByNameAsync(model.UserName);
        if (user?.MustChangePassword == true) return RedirectToAction(nameof(ChangePassword));
        return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : Url.Action("Index", "Boards")!);
    }

    [Authorize]
    public IActionResult ChangePassword() => View(new ChangePasswordViewModel());

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var user = (await users.GetUserAsync(User))!;
        var result = await users.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);
            return View(model);
        }
        user.MustChangePassword = false;
        await users.UpdateAsync(user);
        await signIn.RefreshSignInAsync(user);
        TempData["Message"] = "Parolanız değiştirildi.";
        return RedirectToAction("Index", "Boards");
    }

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout() { await signIn.SignOutAsync(); return RedirectToAction(nameof(Login)); }

    [AllowAnonymous] public IActionResult AccessDenied() => View();
}
