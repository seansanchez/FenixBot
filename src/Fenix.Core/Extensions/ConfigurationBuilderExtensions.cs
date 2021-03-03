using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace Fenix.Core.Extensions
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
            var keyVaultName = Environment.GetEnvironmentVariable("KeyVaultName");

            var keyVaultUri = $"https://{keyVaultName}.vault.azure.net/";

            if (string.Equals(GetEnvironment(), "local"))
            {
                source.AddAzureKeyVault(new Uri(keyVaultUri), new AzureCliCredential());
            }
            else
            {
                source.AddAzureKeyVault(new Uri(keyVaultUri), new ManagedIdentityCredential());
            }
        }

        private static string GetEnvironment()
        {
            return Environment.GetEnvironmentVariable(Constants.EnvironmentSettingName);
        }
    }
}