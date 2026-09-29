using Microsoft.AspNetCore.Identity;

namespace Bfn.DevOps.Models;

public sealed class ApplicationUser : IdentityUser
{
    public bool IsFounder { get; set; }
    public bool MustChangePassword { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public string Theme { get; set; } = "bfnorbi";
}
