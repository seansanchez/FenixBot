using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fenix.Bot.Functions
{
    public class BotFunctions
    {
        private readonly IBot _bot;
        private readonly IOptions<BotSettings> _botSettings;
        private readonly ILogger<BotFunctions> _logger;

        public BotFunctions(
            IBot bot,
            IOptions<BotSettings> botSettings,
            ILogger<BotFunctions> logger)
        {
            _bot = bot ?? throw new ArgumentNullException(nameof(bot));
            _botSettings = botSettings ?? throw new ArgumentNullException(nameof(botSettings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private BotFrameworkAdapter BotFrameworkAdapter => new BotFrameworkAdapter(new MicrosoftAppCredentials(_botSettings.Value.AadApplicationId, _botSettings.Value.AadApplicationPassword), new AuthenticationConfiguration());

        [FunctionName("bot")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "messages")] HttpRequest request)
        {
            var requestBody = await new StreamReader(request.Body).ReadToEndAsync().ConfigureAwait(false);

            var activity = JsonConvert.DeserializeObject<Activity>(requestBody);
            await BotFrameworkAdapter.ProcessActivityAsync(request.Headers[@"Authorization"].FirstOrDefault(), activity, Callback, CancellationToken.None);

            return new OkResult();
        }

        private async Task Callback(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            await _bot.OnTurnAsync(turnContext, cancellationToken).ConfigureAwait(false);
        }
    }
}
