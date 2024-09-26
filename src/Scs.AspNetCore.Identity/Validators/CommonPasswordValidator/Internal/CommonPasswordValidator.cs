using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

//source code: https://github.com/andrewlock/CommonPasswordsValidator

namespace Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal
{
    /// <summary>
    /// Provides an abstraction for validating that the supplied password is not in a list of common passwords
    /// </summary>
    internal abstract class CommonPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        private readonly string _errorMessage;

        public CommonPasswordValidator(HashSet<string> passwords, IOptions<CommonPasswordValidatorOptions> options)
        {
            Passwords = passwords;
            _errorMessage = options.Value.ErrorMessage;
        }

        /// <summary>
        /// The collection of common passwords which should not be allowed
        /// </summary>
        protected HashSet<string> Passwords { get; }

        ///<inheritdoc />
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager,
                                                  TUser user,
                                                  string password)
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(manager);

            var result = Passwords.Contains(password)
            ? IdentityResult.Failed(new IdentityError
            {
                Code = "CommonPassword",
                Description = _errorMessage
            })
            : IdentityResult.Success;

            return Task.FromResult(result);
        }
    }
}