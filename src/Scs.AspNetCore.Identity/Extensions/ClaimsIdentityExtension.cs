using System.Security.Claims;

namespace Scs.AspNetCore.Identity.Extensions;

public static class ClaimsIdentityExtension
{
    public static void AddEnumClaim<T>(this ClaimsIdentity identity, string type, T enumValue) where T : struct
    {
        if (typeof(T).IsEnum == false)
            throw new AggregateException("Object must be an enum");
        identity.AddClaim(new Claim(type, enumValue.ToString()));
    }
}