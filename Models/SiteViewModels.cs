using System.ComponentModel.DataAnnotations;

namespace Bfn.DevOps.Models;

public sealed record IisSiteInfo(string Name, long Id, string State, string PhysicalPath, IReadOnlyList<string> Bindings);

public sealed class CreateSiteViewModel
{
    [Required, RegularExpression("^[a-zA-Z0-9._-]+$"), Display(Name = "Site adı")] public string Name { get; set; } = "";
    [Range(1, 65535), Display(Name = "Port")] public int Port { get; set; } = 80;
    [RegularExpression("^$|^[a-zA-Z0-9.-]+$"), Display(Name = "Host adı")] public string HostName { get; set; } = "";
}
