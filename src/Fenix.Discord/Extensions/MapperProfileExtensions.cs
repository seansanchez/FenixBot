using AutoMapper;
using Fenix.Discord.MapperProfiles;

namespace Fenix.Discord.Extensions
{
    public static class MapperProfileExtensions
    {
        public static void AddBotProfile(this IMapperConfigurationExpression source)
        {
            source.AddProfile<BotProfile>();
        }

        public static void AddDiscordProfile(this IMapperConfigurationExpression source)
        {
            source.AddProfile<DiscordProfile>();
        }
    }
}