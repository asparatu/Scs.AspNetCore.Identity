using Microsoft.Extensions.Options;
using Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal;

namespace Scs.AspNetCore.Identity.Validators.CommonPasswordValidator
{
    /// <summary>
    /// Validates that the supplied password is not one of the 10,000 most common passwords
    /// </summary>
    internal class Top10000PasswordValidator<TUser>(PasswordLists passwords, IOptions<CommonPasswordValidatorOptions> options)
        : CommonPasswordValidator<TUser>(passwords.Top10000Passwords.Value, options) where TUser : class
    {
    }
}