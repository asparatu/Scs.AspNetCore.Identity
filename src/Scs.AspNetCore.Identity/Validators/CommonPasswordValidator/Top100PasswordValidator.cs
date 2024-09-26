using Microsoft.Extensions.Options;
using Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal;

namespace Scs.AspNetCore.Identity.Validators.CommonPasswordValidator
{
    /// <summary>
    /// Validates that the supplied password is not one of the 100 most common passwords
    /// </summary>
    internal class Top100PasswordValidator<TUser>(PasswordLists passwords, IOptions<CommonPasswordValidatorOptions> options)
        : CommonPasswordValidator<TUser>(passwords.Top100Passwords.Value, options) where TUser : class
    {
    }
}