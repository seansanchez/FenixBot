using System;
using System.Threading;
using System.Threading.Tasks;
using Fenix.Worker.DiscordListener.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fenix.Worker.DiscordListener
{
    public class Worker : BackgroundService
    {
        private readonly IDiscordService _discordService;
        private readonly ILogger<Worker> _logger;

        public Worker(IDiscordService discordListener, ILogger<Worker> logger)
        {
            this._discordService = discordListener ?? throw new ArgumentNullException(nameof(discordListener));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._discordService.InitializeAsync().ConfigureAwait(false);

            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}