using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps.Data;

public static class DatabaseSeeder
{
    public const string AdministratorRole = "Administrator";

    public static async Task SeedAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.MigrateAsync();
        var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roles.RoleExistsAsync(AdministratorRole)) await roles.CreateAsync(new IdentityRole(AdministratorRole));

        var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        if (await users.Users.AnyAsync()) return;
        var admin = new ApplicationUser { UserName = "admin", IsFounder = true, MustChangePassword = true, CreatedAtUtc = DateTime.UtcNow };
        var create = await users.CreateAsync(admin);
        if (!create.Succeeded) throw new InvalidOperationException(string.Join("; ", create.Errors.Select(x => x.Description)));
        admin.PasswordHash = users.PasswordHasher.HashPassword(admin, "admin");
        admin.SecurityStamp = Guid.NewGuid().ToString();
        await users.UpdateAsync(admin);
        await users.AddToRoleAsync(admin, AdministratorRole);
    }
}
