using AutoMapper;
using Discord;
using Discord.WebSocket;
using Fenix.Discord;
using Fenix.Discord.Extensions;
using Fenix.Discord.Persistence.Interfaces;
using Fenix.Worker.DiscordListener.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fenix.Worker.DiscordListener.Services
{
    public class DiscordListener : IDiscordListener, IDisposable
    {
        private readonly IDiscordActivityQueue _discordActivityQueue;
        private readonly IMapper _mapper;
        private readonly IOptions<DiscordSettings> _discordSettings;
        private readonly ILogger<DiscordListener> _logger;

        private readonly DiscordSocketClient _discordSocketClient = new DiscordSocketClient();

        public DiscordListener(
            IDiscordActivityQueue discordActivityQueue,
            IMapper mapper,
            IOptions<DiscordSettings> discordSettings,
            ILogger<DiscordListener> logger)
        {
            _discordActivityQueue = discordActivityQueue ?? throw new ArgumentNullException(nameof(discordActivityQueue));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _discordSettings = discordSettings ?? throw new ArgumentNullException(nameof(discordSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InitializeAsync()
        {
            if (_discordSocketClient.ConnectionState != ConnectionState.Connected)
            {
                await _discordSocketClient.LoginAsync(TokenType.Bot, _discordSettings.Value.BotToken).ConfigureAwait(false);
                await _discordSocketClient.StartAsync().ConfigureAwait(false);

                _discordSocketClient.MessageReceived += HandleMessageReceivedAsync;
            }
        }

        private async Task HandleMessageReceivedAsync(SocketMessage arg)
        {
            try
            {
                if (arg.Source == MessageSource.User)
                {
                    await _discordActivityQueue
                        .SendMessageAsync(_mapper.ToActivity(arg), CancellationToken.None)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _discordSocketClient?.Dispose();
        }
    }
}