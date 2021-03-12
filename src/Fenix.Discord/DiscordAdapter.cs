using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Rest;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fenix.Discord
{
    public class DiscordAdapter : BotAdapter
    {
        private readonly IOptions<DiscordOptions> _discordOptions;
        private readonly DiscordRestClient _discordRestClient;
        private readonly ILogger<DiscordAdapter> _logger;

        public DiscordAdapter(
            IOptions<DiscordOptions> discordOptions,
            ILogger<DiscordAdapter> logger)
        {
            this._discordOptions = discordOptions ?? throw new ArgumentNullException(nameof(discordOptions));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._discordRestClient = new DiscordRestClient();
        }

        public override async Task<ResourceResponse[]> SendActivitiesAsync(
            ITurnContext turnContext,
            Activity[] activities,
            CancellationToken cancellationToken)
        {
            if (this._discordRestClient.LoginState != LoginState.LoggedIn)
            {
                await this._discordRestClient
                    .LoginAsync(TokenType.Bot, this._discordOptions.Value.BotToken)
                    .ConfigureAwait(false);
            }

            var resourceResponses = new List<ResourceResponse>();

            foreach (var activity in activities)
            {
                IMessageChannel channel;

                if (activity.Conversation.IsGroup.GetValueOrDefault())
                {
                    channel = await this._discordRestClient
                        .GetGroupChannelAsync(Convert.ToUInt64(activity.ChannelId))
                        .ConfigureAwait(false);
                }
                else
                {
                    channel = await this._discordRestClient
                        .GetDMChannelAsync(Convert.ToUInt64(activity.ChannelId))
                        .ConfigureAwait(false);
                }

                var message = await channel.SendMessageAsync(activity.Text).ConfigureAwait(false);

                resourceResponses.Add(new ResourceResponse($"{message.Id}"));
            }

            return resourceResponses.ToArray();
        }

        public override Task<ResourceResponse> UpdateActivityAsync(
            ITurnContext turnContext,
            Activity activity,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override Task DeleteActivityAsync(
            ITurnContext turnContext,
            ConversationReference reference,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<InvokeResponse> ProcessActivityAsync(
            ClaimsIdentity claimsIdentity,
            Activity activity,
            BotCallbackHandler callback,
            CancellationToken cancellationToken)
        {
            using (var context = new TurnContext(this, activity))
            {
                await this.RunPipelineAsync(context, callback, cancellationToken).ConfigureAwait(false);
            }

            return new InvokeResponse();
        }
    }
}