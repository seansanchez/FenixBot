using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fenix.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAzureStorageOptions(this IServiceCollection source, string section = null)
        {
            if (section == null)
            {
                section = "AzureStorageOptions";
            }

            source.AddOptions<AzureStorageOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}