using AutoMapper;
using Discord;
using Discord.WebSocket;
using Microsoft.Bot.Schema;
using IActivity = Microsoft.Bot.Schema.IActivity;

namespace Fenix.Discord.MapperProfiles
{
    public class BotProfile : Profile
    {
        public BotProfile()
        {
            CreateMap<SocketMessage, IActivity>()
                .ConvertUsing(x => ToActivity(x));
        }

        private IActivity ToActivity(SocketMessage source)
        {
            var activity = new Activity()
            {
                DeliveryMode = DeliveryModes.Normal,
                Type = ActivityTypes.Message,
                Value = source.Content,
                Id = $"{source.Id}",
                Timestamp = source.Timestamp,
                ChannelId = $"{source.Channel.Id}",
                From = new ChannelAccount()
                {
                    Id = $"{source.Author.Id}",
                    Name = source.Author.Username
                },
                Recipient = new ChannelAccount()
                {
                    Id = $"{source.Channel.Id}",
                    Name = source.Channel.Name
                },
                Text = source.Content,
                Conversation = new ConversationAccount()
                {
                    Id = $"{source.Channel.Id}",
                    Name = source.Channel.Name,
                    IsGroup = source.Channel is IGroupChannel
                }
            };

            return activity;
        }
    }
}