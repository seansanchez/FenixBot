using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using Discord;
using Fenix.Discord.Entities;
using Fenix.Discord.Enums;

namespace Fenix.Discord.MapperProfiles
{
    public class DiscordProfile : Profile
    {
        public DiscordProfile()
        {
            this.CreateMap<ActivityType, ActivityTypeEnum>()
                .ConvertUsingEnumMapping(x =>
                {
                    x.MapValue(ActivityType.Playing, ActivityTypeEnum.Playing);
                    x.MapValue(ActivityType.Streaming, ActivityTypeEnum.Streaming);
                    x.MapValue(ActivityType.Listening, ActivityTypeEnum.Listening);
                    x.MapValue(ActivityType.Watching, ActivityTypeEnum.Watching);
                    x.MapValue(ActivityType.CustomStatus, ActivityTypeEnum.CustomStatus);
                });

            this.CreateMap<IActivity, DiscordActivityEntity>();

            this.CreateMap<IMessage, DiscordMessageEntity>();

            this.CreateMap<IUser, DiscordUserEntity>();

            this.CreateMap<UserStatus, UserStatusEnum>()
                .ConvertUsingEnumMapping(x =>
                {
                    x.MapValue(UserStatus.Offline, UserStatusEnum.Offline);
                    x.MapValue(UserStatus.Online, UserStatusEnum.Online);
                    x.MapValue(UserStatus.Idle, UserStatusEnum.Idle);
                    x.MapValue(UserStatus.AFK, UserStatusEnum.AFK);
                    x.MapValue(UserStatus.DoNotDisturb, UserStatusEnum.DoNotDisturb);
                    x.MapValue(UserStatus.Invisible, UserStatusEnum.Invisible);
                });
        }
    }
}