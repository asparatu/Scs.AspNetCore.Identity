using Microsoft.AspNetCore.Identity;

namespace Scs.AspNetCore.Identity.Validators;

/// <inheritdoc />
internal abstract class PasswordChangeValidatorBase<TUser>
    : ChangePasswordOnlyValidatorBase<TUser, string> where TUser : IdentityUser<string> { }

/// <summary>
/// A base class that only applies validations when the user is changing their password
/// </summary>
internal abstract class ChangePasswordOnlyValidatorBase<TUser, TKey>
    : IPasswordValidator<TUser> where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
{
    /// <inheritdoc />
    public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(manager);

        var isNewUser = (user == null
            || user.Id == null
            || user.Id.Equals(default(TKey))
            || string.IsNullOrEmpty(user.PasswordHash));

        return isNewUser ? Task.FromResult(IdentityResult.Success) : ValidatePasswordChangeAsync(manager, user, password);
    }

    internal abstract Task<IdentityResult> ValidatePasswordChangeAsync(UserManager<TUser> manager, TUser user, string password);
}