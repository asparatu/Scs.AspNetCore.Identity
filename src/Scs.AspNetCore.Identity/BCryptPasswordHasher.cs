using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Scs.AspNetCore.Identity
{
    /// <summary>
    /// ASP.NET Core Identity password hasher using the bcrypt password hashing algorithm.
    /// </summary>
    /// <typeparam name="TUser">your ASP.NET Core Identity user type (e.g. IdentityUser). User is not used by this implementation</typeparam>
    /// <remarks>
    /// Creates a new BCryptPasswordHasher.
    /// </remarks>
    /// /// <param name="optionsAccessor">optional BCryptPasswordHasherOptions</param>
    public class BCryptPasswordHasher<TUser>(IOptions<BCryptPasswordHasherOptions> optionsAccessor = null) : IPasswordHasher<TUser> where TUser : class
    {
        private readonly BCryptPasswordHasherOptions _options = optionsAccessor?.Value ?? new BCryptPasswordHasherOptions();

        /// <summary>
        /// Hashes a password using bcrypt.
        /// </summary>
        /// <param name="user">not used for this implementation</param>
        /// <param name="password">plaintext password</param>
        /// <returns>hashed password</returns>
        /// <exception cref="ArgumentNullException">missing plaintext password</exception>
        public virtual string HashPassword(TUser user, string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

#pragma warning disable 618
            return BCrypt.Net.BCrypt.HashPassword(password, _options.WorkFactor, _options.EnhancedEntropy);
#pragma warning restore 618
        }

        /// <summary>
        /// Verifies a plaintext password against a stored hash.
        /// </summary>
        /// <param name="user">not used for this implementation</param>
        /// <param name="hashedPassword">the stored, hashed password</param>
        /// <param name="providedPassword">the plaintext password to verify against the stored hash</param>
        /// <returns>If the password matches the stored password. Returns SuccessRehashNeeded if the work factor has changed</returns>
        /// <exception cref="ArgumentNullException">missing plaintext password or hashed password</exception>
        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword)) throw new ArgumentNullException(nameof(hashedPassword));
            if (string.IsNullOrWhiteSpace(providedPassword)) throw new ArgumentNullException(nameof(providedPassword));

#pragma warning disable 618
            var isValid = false;
            if (hashedPassword.StartsWith("$2a$"))
            {
                isValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword, _options.EnhancedEntropy);
            }

            //var isValid = BCrypt.Net.BCrypt.EnhancedVerify(providedPassword, hashedPassword);
#pragma warning restore 618

            return isValid && BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, _options.WorkFactor)
                ? PasswordVerificationResult.SuccessRehashNeeded
                : isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}