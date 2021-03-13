using System;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Fenix.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void ConfigureFenix(this IConfigurationBuilder source)
        {
            source.AddSecrets();

            source
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .AddJsonFile($"appsettings.{GetEnvironment()}.json", optional: true, reloadOnChange: false)
                .AddEnvironmentVariables();
        }

        private static void AddSecrets(this IConfigurationBuilder source)
        {
            var keyVaultUri = Environment.GetEnvironmentVariable("KeyVaultUri");

            source.AddAzureKeyVault(new Uri(keyVaultUri), new ManagedIdentityCredential());
        }

        private static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable(Constants.EnvironmentSettingName);
        }
    }
}