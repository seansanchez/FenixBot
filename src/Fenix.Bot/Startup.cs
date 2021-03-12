using Fenix.Bot.Bots;
using Fenix.Discord;
using Fenix.Discord.Extensions;
using Fenix.Extensions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Bot.Builder;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Fenix.Bot.Startup))]

namespace Fenix.Bot
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient<IBot, EchoBot>();

            builder.Services.AddAzureStorageOptions();
            builder.Services.AddDiscordOptions();
            builder.Services.AddSingleton<DiscordAdapter>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.ConfigurationBuilder.ConfigureFenix();
        }
    }
}