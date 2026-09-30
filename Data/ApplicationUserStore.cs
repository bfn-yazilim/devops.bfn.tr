using System.Security.Claims;
using Bfn.DevOps.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bfn.DevOps.Data;

/// <summary>
/// Bu proje kullanıcı bazlı claim (SyUserClaim) özelliğini kullanmıyor. Identity'nin
/// varsayılan UserStore'u her girişte GetClaimsAsync çağırdığı için tabloyu tamamen
/// kaldırabilmek adına bu metodları no-op yapıyoruz.
/// </summary>
public sealed class ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : UserStore<ApplicationUser, IdentityRole, ApplicationDbContext>(context, describer)
{
    public override Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        => Task.FromResult<IList<Claim>>([]);

    public override Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public override Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public override Task RemoveClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public override Task<IList<ApplicationUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default)
        => Task.FromResult<IList<ApplicationUser>>([]);
}
