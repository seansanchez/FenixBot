FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /home/site/wwwroot

FROM base AS final
WORKDIR /home/site/wwwroot
ADD /app .
ENTRYPOINT ["dotnet", "Fenix.Worker.DiscordListener.dll"]