using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace Fenix.Bot.Bots
{
    public class EchoBot : ActivityHandler
    {
        protected override async Task OnMessageActivityAsync(
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            try
            {
                var replyText = $"Echo: {turnContext.Activity.Text}";
                await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override async Task OnMembersAddedAsync(
            IList<ChannelAccount> membersAdded,
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";

            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
