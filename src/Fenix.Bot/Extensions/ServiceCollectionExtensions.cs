using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fenix.Bot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBotOptions(this IServiceCollection source, string section = null)
        {
            if (section == null)
            {
                section = "BotOptions";
            }

            source.AddOptions<BotOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}