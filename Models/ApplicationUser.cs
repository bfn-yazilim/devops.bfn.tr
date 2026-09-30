using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bfn.DevOps.Models;

public sealed class ApplicationUser : IdentityUser, IAuditable
{
    public bool IsFounder { get; set; }
    public bool MustChangePassword { get; set; }
    public string Theme { get; set; } = "bfnorbi";

    public Guid UId { get; set; } = Guid.NewGuid();
    [MaxLength(450)] public string? CreUser { get; set; }
    public DateTime CreDate { get; set; } = DateTime.UtcNow;
    [MaxLength(450)] public string? ModUser { get; set; }
    public DateTime? ModDate { get; set; }
    [MaxLength(450)] public string? DelUser { get; set; }
    public DateTime? DelDate { get; set; }
    [MaxLength(50)] public string? Client { get; set; }
    [MaxLength(50)] public string? ClientIp { get; set; }
    public bool IsDeleted { get; set; }
}
