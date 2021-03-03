using Microsoft.Bot.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace Fenix.Discord.Persistence.Interfaces
{
    public interface IDiscordActivityQueue
    {
        Task SendMessageAsync(IActivity activity, CancellationToken cancellationToken);
    }
}