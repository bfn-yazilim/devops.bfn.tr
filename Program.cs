using Bfn.DevOps.Data;
using Bfn.DevOps.Models;
using Bfn.DevOps.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bfn.DevOps;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Directory.CreateDirectory(Path.Combine(builder.Environment.ContentRootPath, "App_Data"));

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Strict;
            options.ExpireTimeSpan = TimeSpan.FromHours(8);
        });
        builder.Services.Configure<IisOptions>(builder.Configuration.GetSection("Iis"));
        builder.Services.AddScoped<IIisService, IisService>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();
        app.Use(async (context, next) =>
        {
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var users = context.RequestServices.GetRequiredService<UserManager<ApplicationUser>>();
                var user = await users.GetUserAsync(context.User);
                var path = context.Request.Path;
                if (user?.MustChangePassword == true &&
                    !path.StartsWithSegments("/Account/ChangePassword") &&
                    !path.StartsWithSegments("/Account/Logout"))
                {
                    context.Response.Redirect("/Account/ChangePassword");
                    return;
                }
            }
            await next();
        });
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default", pattern: "{controller=Sites}/{action=Index}/{id?}");

        await DatabaseSeeder.SeedAsync(app.Services);
        await app.RunAsync();
    }
}
