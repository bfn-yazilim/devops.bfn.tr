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

        var genel = await db.Boards.Include(x => x.Columns).FirstOrDefaultAsync();
        if (genel is null)
        {
            genel = new Board { Name = "Genel" };
            db.Boards.Add(genel);
        }
        string[] expectedColumns = ["Yapılacak", "Kuyrukta", "Çalışıyor", "Review", "Tamamlandı"];
        foreach (var name in expectedColumns)
        {
            if (!genel.Columns.Any(x => x.Name == name)) genel.Columns.Add(new BoardColumn { Name = name });
        }
        var orderedColumns = genel.Columns
            .OrderBy(x => Array.IndexOf(expectedColumns, x.Name) is var i && i >= 0 ? i : int.MaxValue)
            .ThenBy(x => x.SortOrder)
            .ToList();
        for (var i = 0; i < orderedColumns.Count; i++) orderedColumns[i].SortOrder = i;
        await db.SaveChangesAsync();

        var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var usersOnLegacyDefault = await users.Users.Where(x => x.Theme == "" || x.Theme == "bfnlight").ToListAsync();
        foreach (var existingUser in usersOnLegacyDefault) { existingUser.Theme = "bfnorbi"; await users.UpdateAsync(existingUser); }
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
