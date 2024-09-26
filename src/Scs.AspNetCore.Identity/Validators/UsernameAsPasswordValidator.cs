using Microsoft.AspNetCore.Identity;

namespace Scs.AspNetCore.Identity.Validators;

/// <inheritdoc />
internal class UsernameAsPasswordValidator<TUser> : UsernameAsPasswordValidator<TUser, string> where TUser : IdentityUser<string> { }

internal class UsernameAsPasswordValidator<TUser, TKey> : IPasswordValidator<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(manager);

        return user is not null && string.Equals(user.UserName, password, StringComparison.OrdinalIgnoreCase)
            ? Task.FromResult(IdentityResult.Failed(new IdentityError
            {
                Code = "UsernameAsPassword",
                Description = "You cannot use your username as your password"
            }))
            : Task.FromResult(IdentityResult.Success);
    }
}