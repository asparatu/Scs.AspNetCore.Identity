using System.Security.Claims;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Scs.AspNetCore.Identity.Factory;

public class ClaimsFactory<TUser>(UserManager<TUser> userManager, IOptions<IdentityOptions> optionsAccessor) : 
    UserClaimsPrincipalFactory<TUser>(userManager, optionsAccessor) where TUser : class
{
    protected override Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
    {
        var identity = base.GenerateClaimsAsync(user).Result;
        var roles = UserManager.GetRolesAsync(user).Result;

        foreach (var role in roles)
        {
            identity.AddClaim(new Claim(ClaimTypes.Role, role));
        }

        return Task.FromResult(identity);
    }
}