﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fenix.Discord.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDiscordOptions(this IServiceCollection source, string section = null)
        {
            if (section == null)
            {
                section = "DiscordOptions";
            }

            source.AddOptions<DiscordOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection(section).Bind(settings);
                });
        }
    }
}