using Fenix.Discord.Enums;
using System;
using System.Collections.Generic;

namespace Fenix.Discord.Entities
{
    public class DiscordUserEntity
    {
        public ulong Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string Mention { get; set; }
        public DiscordActivityEntity Activity { get; set; }
        public UserStatusEnum Status { get; set; }
        public IEnumerable<DiscordActivityEntity> Activities { get; set; }
        public string AvatarId { get; set; }
        public string Discriminator { get; set; }
        public ushort DiscriminatorValue { get; set; }
        public bool IsBot { get; set; }
        public bool IsWebhook { get; set; }
        public string Username { get; set; }
    }
}