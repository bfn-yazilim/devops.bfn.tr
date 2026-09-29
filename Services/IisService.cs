using Bfn.DevOps.Models;
using Microsoft.Extensions.Options;
using Microsoft.Web.Administration;

namespace Bfn.DevOps.Services;

public sealed class IisOptions { public string SitesRoot { get; set; } = @"C:\inetpub\wwwroot"; }
public interface IIisService { IReadOnlyList<IisSiteInfo> GetSites(); void CreateSite(CreateSiteViewModel model); void ApplyProject(DevOpsProject project); }

public sealed class IisService(IOptions<IisOptions> options) : IIisService
{
    private readonly string _root = Path.GetFullPath(options.Value.SitesRoot);

    public IReadOnlyList<IisSiteInfo> GetSites()
    {
        EnsureWindows();
        using var manager = new ServerManager();
        return manager.Sites.Select(site => new IisSiteInfo(site.Name, site.Id, site.State.ToString(),
            site.Applications["/"]?.VirtualDirectories["/"]?.PhysicalPath ?? "",
            site.Bindings.Select(x => $"{x.Protocol}://{x.BindingInformation}").ToArray())).ToArray();
    }

    public void CreateSite(CreateSiteViewModel model)
    {
        EnsureWindows();
        var sitePath = Path.GetFullPath(Path.Combine(_root, model.Name));
        if (!sitePath.StartsWith(_root + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Site dizini izin verilen kökün dışında olamaz.");
        using var manager = new ServerManager();
        if (manager.Sites.Any(x => x.Name.Equals(model.Name, StringComparison.OrdinalIgnoreCase))) throw new InvalidOperationException("Bu isimde bir IIS sitesi zaten var.");
        var binding = $"*:{model.Port}:{model.HostName.Trim()}";
        if (manager.Sites.SelectMany(x => x.Bindings).Any(x => x.Protocol == "http" && x.BindingInformation.Equals(binding, StringComparison.OrdinalIgnoreCase))) throw new InvalidOperationException("Bu IIS binding başka bir site tarafından kullanılıyor.");
        Directory.CreateDirectory(sitePath);
        var site = manager.Sites.Add(model.Name, "http", binding, sitePath);
        site.ApplicationDefaults.ApplicationPoolName = model.Name;
        if (manager.ApplicationPools[model.Name] is null) manager.ApplicationPools.Add(model.Name);
        manager.CommitChanges();
    }

    public void ApplyProject(DevOpsProject project)
    {
        EnsureWindows();
        var sitePath = Path.GetFullPath(string.IsNullOrWhiteSpace(project.WorkingDirectory) ? Path.Combine(_root, project.Name) : project.WorkingDirectory);
        if (!sitePath.StartsWith(_root + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Çalışma dizini izin verilen IIS kökünün altında olmalıdır.");
        Directory.CreateDirectory(sitePath);
        using var manager = new ServerManager();
        if (manager.ApplicationPools[project.ApplicationPoolName] is null) manager.ApplicationPools.Add(project.ApplicationPoolName);
        var site = manager.Sites[project.IisSiteName];
        if (site is null)
        {
            var host = project.Url?.Trim() ?? "";
            if (manager.Sites.SelectMany(x => x.Bindings).Any(x => x.Protocol == "http" && x.BindingInformation.Equals($"*:80:{host}", StringComparison.OrdinalIgnoreCase))) throw new InvalidOperationException("Bu host adı başka bir IIS sitesi tarafından kullanılıyor.");
            site = manager.Sites.Add(project.IisSiteName, "http", $"*:80:{host}", sitePath);
        }
        site.ApplicationDefaults.ApplicationPoolName = project.ApplicationPoolName;
        site.Applications["/"].ApplicationPoolName = project.ApplicationPoolName;
        site.Applications["/"].VirtualDirectories["/"].PhysicalPath = sitePath;
        manager.CommitChanges();
    }

    private static void EnsureWindows() { if (!OperatingSystem.IsWindows()) throw new PlatformNotSupportedException("IIS yönetimi yalnızca Windows üzerinde kullanılabilir."); }
}
