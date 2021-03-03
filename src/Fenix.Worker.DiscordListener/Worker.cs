using Fenix.Worker.DiscordListener.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fenix.Worker.DiscordListener
{
    public class Worker : BackgroundService
    {
        private readonly IDiscordListener _discordListener;
        private readonly ILogger<Worker> _logger;

        public Worker(IDiscordListener discordListener, ILogger<Worker> logger)
        {
            _discordListener = discordListener ?? throw new ArgumentNullException(nameof(discordListener));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _discordListener.InitializeAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}