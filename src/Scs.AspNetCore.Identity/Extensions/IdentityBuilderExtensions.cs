using Microsoft.AspNetCore.Identity;

using Scs.AspNetCore.Identity.Validators;
using Scs.AspNetCore.Identity.Validators.CommonPasswordValidator;
using Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdentityBuilderExtensions
{
    #region ConfigureOptions

    private static void ConfigureCommonPasswordValidatorOptions(IdentityBuilder builder, string errorMessage)
    {
        builder.Services.Configure<CommonPasswordValidatorOptions>(x => x.ErrorMessage = errorMessage);
    }

    #endregion ConfigureOptions

    #region AddPasswordLists

    private static void AddPasswordLists(IdentityBuilder builder)
    {
        builder.Services.AddSingleton<PasswordLists>();
    }

    #endregion AddPasswordLists

    #region AddUsernameAsPasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the user's Username is not the same as the provided password
    /// </summary>
    /// <typeparam name="TUser">The type of the TUser</typeparam>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddUsernameAsPasswordValidator<TUser>(this IdentityBuilder builder) where TUser : IdentityUser
    {
        return builder is null
            ? throw new ArgumentNullException(nameof(builder))
            : builder.AddPasswordValidator<UsernameAsPasswordValidator<TUser, string>>();
    }

    #endregion AddUsernameAsPasswordValidator<TUser>

    #region AddUsernameAsPasswordValidator<TUser, TKey>

    /// <summary>
    ///  Adds a password validator that checks the user's Username is not the same as the provided password
    /// </summary>
    /// <typeparam name="TUser">The type of the TUser</typeparam>
    /// <typeparam name="TKey">The key type of the TUser</typeparam>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddUsernameAsPasswordValidator<TUser, TKey>(this IdentityBuilder builder) where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        return builder is null
            ? throw new ArgumentNullException(nameof(builder))
            : builder.AddPasswordValidator<UsernameAsPasswordValidator<TUser, TKey>>();
    }

    #endregion AddUsernameAsPasswordValidator<TUser, TKey>

    #region AddEmailAsPasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the user's Email is not the same as the provided password
    /// </summary>
    /// <typeparam name="TUser">The type of the TUser</typeparam>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddEmailAsPasswordValidator<TUser>(this IdentityBuilder builder) where TUser : IdentityUser
    {
        return builder is null
            ? throw new ArgumentNullException(nameof(builder))
            : builder.AddPasswordValidator<EmailAsPasswordValidator<TUser, string>>();
    }

    #endregion AddEmailAsPasswordValidator<TUser>

    #region AddEmailAsPasswordValidator<TUser, TKey>

    /// <summary>
    ///  Adds a password validator that checks the user's Email is not the same as the provided password
    /// </summary>
    /// <typeparam name="TUser">The type of the TUser</typeparam>
    /// <typeparam name="TKey">The key type of the TUser</typeparam>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddEmailAsPasswordValidator<TUser, TKey>(this IdentityBuilder builder) where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
    {
        return builder is null
            ? throw new ArgumentNullException(nameof(builder))
            : builder.AddPasswordValidator<EmailAsPasswordValidator<TUser, TKey>>();
    }

    #endregion AddEmailAsPasswordValidator<TUser, TKey>

    #region AddTop100PasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the password is not one of the top 100 most common passwords
    /// </summary>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddTop100PasswordValidator<TUser>(this IdentityBuilder builder) where TUser : class
    {
        AddPasswordLists(builder);
        return builder.AddPasswordValidator<Top100PasswordValidator<TUser>>();
    }

    #endregion AddTop100PasswordValidator<TUser>

    #region AddTop500PasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the password is not one of the top 500 most common passwords
    /// </summary>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddTop500PasswordValidator<TUser>(this IdentityBuilder builder) where TUser : class
    {
        AddPasswordLists(builder);
        return builder.AddPasswordValidator<Top500PasswordValidator<TUser>>();
    }

    #endregion AddTop500PasswordValidator<TUser>

    #region AddTop1000PasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the password is not one of the top 1,000 most common passwords
    /// </summary>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddTop1000PasswordValidator<TUser>(this IdentityBuilder builder) where TUser : class
    {
        AddPasswordLists(builder);
        return builder.AddPasswordValidator<Top1000PasswordValidator<TUser>>();
    }

    #endregion AddTop1000PasswordValidator<TUser>

    #region AddTop10000PasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the password is not one of the top 10,000 most common passwords
    /// </summary>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddTop10000PasswordValidator<TUser>(this IdentityBuilder builder) where TUser : class
    {
        AddPasswordLists(builder);
        return builder.AddPasswordValidator<Top10000PasswordValidator<TUser>>();
    }

    #endregion AddTop10000PasswordValidator<TUser>

    #region AddTop100000PasswordValidator<TUser>

    /// <summary>
    /// Adds a password validator that checks the password is not one of the top 100,000 most common passwords
    /// </summary>
    /// <param name="builder">The Microsoft.AspNetCore.Identity.IdentityBuilder instance this method extends</param>
    /// <typeparam name="TUser">The user type whose password will be validated.</typeparam>
    /// <returns>The current Microsoft.AspNetCore.Identity.IdentityBuilder instance.</returns>
    public static IdentityBuilder AddTop100000PasswordValidator<TUser>(this IdentityBuilder builder) where TUser : class
    {
        AddPasswordLists(builder);
        return builder.AddPasswordValidator<Top100000PasswordValidator<TUser>>();
    }

    #endregion AddTop100000PasswordValidator<TUser>
}