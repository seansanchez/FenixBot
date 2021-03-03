using System;

namespace Fenix.Discord.Entities
{
    public class DiscordMessageEntity
    {
        public ulong Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsTTS { get; set; }
        public bool IsPinned { get; set; }
        public bool IsSuppressed { get; set; }
        public bool MentionedEveryone { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTimeOffset? EditedTimestamp { get; set; }
        public DiscordUserEntity Author { get; set; }
    }
}