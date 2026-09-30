using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Bfn.DevOps.Data;

/// <summary>
/// Bu proje rol bazlı claim (SyRoleClaim) özelliğini kullanmıyor. Identity'nin varsayılan
/// RoleStore'u her girişte kullanıcının rolleri için GetClaimsAsync çağırdığı için
/// tabloyu tamamen kaldırabilmek adına bu metodları no-op yapıyoruz.
/// </summary>
public sealed class ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null)
    : RoleStore<IdentityRole, ApplicationDbContext>(context, describer)
{
    public override Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = default)
        => Task.FromResult<IList<Claim>>([]);

    public override Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default)
        => Task.CompletedTask;

    public override Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}
