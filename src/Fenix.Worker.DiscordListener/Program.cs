using Fenix.Discord.Extensions;
using Fenix.Discord.Persistence;
using Fenix.Extensions;
using Fenix.Worker.DiscordListener.Services;
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
                    services.AddAzureStorageOptions();
                    services.AddDiscordOptions();
                    services.AddAutoMapper(
                        x =>
                        {
                            x.AddBotProfile();
                            x.AddDiscordProfile();
                        },
                        typeof(Program));
                    services.AddScoped<IDiscordActivityQueue, DiscordActivityQueue>();
                    services.AddScoped<IDiscordService, DiscordService>();
                    services.AddHostedService<Worker>();
                });
    }
}
