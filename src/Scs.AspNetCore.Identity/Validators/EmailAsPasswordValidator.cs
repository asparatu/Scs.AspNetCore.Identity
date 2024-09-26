using Microsoft.AspNetCore.Identity;

namespace Scs.AspNetCore.Identity.Validators
{
    /// <inheritdoc />
    internal class EmailAsPasswordValidator<TUser> : EmailAsPasswordValidator<TUser, string> where TUser : IdentityUser<string> { }

    /// <summary>
    /// Validates that the supplied password is not the same as the user's Email
    /// </summary>
    internal class EmailAsPasswordValidator<TUser, TKey> : IPasswordValidator<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(manager);

            return user is not null && string.Equals(user.Email, password, StringComparison.OrdinalIgnoreCase)
                ? Task.FromResult(IdentityResult.Failed(new IdentityError
                {
                    Code = "EmailAsPassword",
                    Description = "You cannot use your Email as your password"
                }))
                : Task.FromResult(IdentityResult.Success);
        }
    }
}