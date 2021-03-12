using System.Threading.Tasks;

namespace Fenix.Worker.DiscordListener.Services
{
    public interface IDiscordService
    {
        Task InitializeAsync();
    }
}