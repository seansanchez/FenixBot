using Fenix.Core.Extensions;
using Fenix.Discord.Extensions;
using Fenix.Discord.Persistence;
using Fenix.Discord.Persistence.Interfaces;
using Fenix.Worker.DiscordListener.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Fenix.Worker.DiscordListener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                    builder.ConfigureFenix();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddLogging();
                    services.AddAzureStorageSettings();
                    services.AddDiscordSettings();
                    services.AddAutoMapper(x =>
                    {
                        x.AddBotProfile();
                        x.AddDiscordProfile();
                    }, typeof(Program));
                    services.AddScoped<IDiscordActivityQueue, DiscordActivityQueue>();
                    services.AddScoped<IDiscordListener, Services.DiscordListener>();
                    services.AddHostedService<Worker>();
                });
    }
}
