using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fenix.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDiscordSettings(this IServiceCollection source, string section = null)
        {
            if (section == null)
            {
                section = "AzureStorageSettings";
            }

            source.AddOptions<AzureStorageSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}