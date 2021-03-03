using Azure.Storage.Queues;
using Fenix.Core;
using Fenix.Discord.Persistence.Interfaces;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fenix.Discord.Persistence
{
    public class DiscordActivityQueue : IDiscordActivityQueue
    {
        public const string QueueName = "discord-activity-queue";

        private readonly IOptions<AzureStorageSettings> _options;

        public DiscordActivityQueue(IOptions<AzureStorageSettings> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task SendMessageAsync(IActivity activity, CancellationToken cancellationToken)
        {
            try
            {
                var queueClient = await GetQueueClientAsync().ConfigureAwait(false);

                await queueClient.SendMessageAsync(JsonConvert.SerializeObject(activity), cancellationToken).ConfigureAwait(false);
            }
            catch
            {
                throw;
            }
        }

        private async Task<QueueClient> GetQueueClientAsync()
        {
            var queueClient = new QueueClient(_options.Value.ConnectionString, QueueName, new QueueClientOptions()
            {
                MessageEncoding = QueueMessageEncoding.Base64
            });

            await queueClient.CreateIfNotExistsAsync().ConfigureAwait(false);

            return queueClient;
        }
    }
}