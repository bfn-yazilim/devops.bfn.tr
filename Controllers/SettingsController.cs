using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bfn.DevOps.Controllers;

[Authorize]
public sealed class SettingsController(UserManager<ApplicationUser> users) : Controller
{
    public const string GuestThemeCookie = "theme";

    public async Task<IActionResult> Index() => View((await users.GetUserAsync(User))!);

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Theme(string theme, string? returnUrl)
    {
        if (!ThemeCatalog.Values.Contains(theme)) return BadRequest();
        var user = (await users.GetUserAsync(User))!; user.Theme = theme; await users.UpdateAsync(user);
        return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : Url.Action(nameof(Index))!);
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public IActionResult GuestTheme(string theme, string? returnUrl)
    {
        if (!ThemeCatalog.Values.Contains(theme)) return BadRequest();
        Response.Cookies.Append(GuestThemeCookie, theme, new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddYears(1),
            SameSite = SameSiteMode.Strict,
            IsEssential = true
        });
        return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : Url.Action("Login", "Account")!);
    }
}
