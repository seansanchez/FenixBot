using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fenix.Discord.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAzureStorageSettings(this IServiceCollection source, string section = null)
        {
            if (section == null)
            {
                section = "DiscordSettings";
            }

            source.AddOptions<DiscordSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}