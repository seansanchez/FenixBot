using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Discord;
using Discord.WebSocket;
using Fenix.Discord;
using Fenix.Discord.Extensions;
using Fenix.Discord.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fenix.Worker.DiscordListener.Services
{
    public class DiscordService : IDiscordService, IDisposable
    {
        private readonly IDiscordActivityQueue _discordActivityQueue;
        private readonly IMapper _mapper;
        private readonly IOptions<DiscordOptions> _discordOptions;
        private readonly ILogger<DiscordService> _logger;

        private readonly DiscordSocketClient _discordSocketClient = new DiscordSocketClient();

        public DiscordService(
            IDiscordActivityQueue discordActivityQueue,
            IMapper mapper,
            IOptions<DiscordOptions> discordOptions,
            ILogger<DiscordService> logger)
        {
            this._discordActivityQueue = discordActivityQueue ?? throw new ArgumentNullException(nameof(discordActivityQueue));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._discordOptions = discordOptions ?? throw new ArgumentNullException(nameof(discordOptions));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InitializeAsync()
        {
            if (this._discordSocketClient.ConnectionState != ConnectionState.Connected)
            {
                await this._discordSocketClient.LoginAsync(TokenType.Bot, this._discordOptions.Value.BotToken).ConfigureAwait(false);
                await this._discordSocketClient.StartAsync().ConfigureAwait(false);

                this._discordSocketClient.MessageReceived += this.HandleMessageReceivedAsync;
            }
        }

        public void Dispose()
        {
            this._discordSocketClient?.Dispose();
        }

        private async Task HandleMessageReceivedAsync(SocketMessage arg)
        {
            if (arg.Source == MessageSource.User)
            {
                await this._discordActivityQueue
                    .SendMessageAsync(this._mapper.ToActivity(arg), CancellationToken.None)
                    .ConfigureAwait(false);
            }
        }
    }
}