using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Schema;

namespace Fenix.Discord.Persistence
{
    public interface IDiscordActivityQueue
    {
        Task SendMessageAsync(IActivity activity, CancellationToken cancellationToken);
    }
}