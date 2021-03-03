using AutoMapper;
using Discord.WebSocket;
using Microsoft.Bot.Schema;

namespace Fenix.Discord.Extensions
{
    public static class MapperExtensions
    {
        public static IActivity ToActivity(this IMapper mapper, SocketMessage source)
        {
            return mapper.Map<IActivity>(source);
        }
    }
}