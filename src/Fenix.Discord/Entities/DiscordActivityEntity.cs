using Fenix.Discord.Enums;

namespace Fenix.Discord.Entities
{
    public class DiscordActivityEntity
    {
        public string Name { get; set; }

        public ActivityTypeEnum Type { get; set; }

        public string Details { get; set; }
    }
}