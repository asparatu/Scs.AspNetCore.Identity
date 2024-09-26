namespace Scs.AspNetCore.Identity.Validators.CommonPasswordValidator.Internal
{
    internal class CommonPasswordValidatorOptions
    {
        /// <summary>
        /// The error message to display when validation fails
        /// </summary>
        public string ErrorMessage { get; set; } = "The password you chose is too common.";
    }
}