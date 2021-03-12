using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Fenix.Discord;
using Fenix.Discord.Persistence;
using Microsoft.Azure.WebJobs;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace Fenix.Bot.Functions
{
    public class DiscordFunctions
    {
        private readonly IBot _bot;
        private readonly DiscordAdapter _discordAdapter;
        private readonly ILogger<DiscordFunctions> _logger;

        public DiscordFunctions(
            IBot bot,
            DiscordAdapter discordAdapter,
            ILogger<DiscordFunctions> logger)
        {
            this._bot = bot ?? throw new ArgumentNullException(nameof(bot));
            this._discordAdapter = discordAdapter ?? throw new ArgumentNullException(nameof(discordAdapter));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [FunctionName("discord")]
        public async Task Run([QueueTrigger(DiscordActivityQueue.QueueName)]Activity activity)
        {
            await this._discordAdapter
                .ProcessActivityAsync(new GenericIdentity("bot"), activity, this.Callback, CancellationToken.None)
                .ConfigureAwait(false);
        }

        private async Task Callback(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            await this._bot.OnTurnAsync(turnContext, cancellationToken).ConfigureAwait(false);
        }
    }
}
