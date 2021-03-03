using System.Threading.Tasks;

namespace Fenix.Worker.DiscordListener.Services.Interfaces
{
    public interface IDiscordListener
    {
        Task InitializeAsync();
    }
}