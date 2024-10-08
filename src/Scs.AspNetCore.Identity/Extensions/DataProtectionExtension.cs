using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.Extensions.DependencyInjection;

namespace Scs.AspNetCore.Identity.Extensions;

internal static class DataProtectionExtension
{
    internal static void AddDataProtectionConfiguration(this IServiceCollection services) =>
        services.AddDataProtection().UseCryptographicAlgorithms(
            new AuthenticatedEncryptorConfiguration
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });
}