FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/TelegramBot/AlgoTecture.TelegramBot.Api/AlgoTecture.TelegramBot.Api.csproj", "src/TelegramBot/AlgoTecture.TelegramBot.Api/"]
COPY ["src/TelegramBot/AlgoTecture.TelegramBot.Application/AlgoTecture.TelegramBot.Application.csproj", "src/TelegramBot/AlgoTecture.TelegramBot.Application/"]
COPY ["src/TelegramBot/AlgoTecture.TelegramBot.Domain/AlgoTecture.TelegramBot.Domain.csproj", "src/TelegramBot/AlgoTecture.TelegramBot.Domain/"]
COPY ["src/TelegramBot/AlgoTecture.TelegramBot.Infrastructure/AlgoTecture.TelegramBot.Infrastructure.csproj", "src/TelegramBot/AlgoTecture.TelegramBot.Infrastructure/"]

COPY ["src/AICore/AlgoTecture.AICore.Application/AlgoTecture.AICore.Application.csproj", "src/AICore/AlgoTecture.AICore.Application/"]

COPY ["src/Services/Reservation/AlgoTecture.Reservation.Application/AlgoTecture.Reservation.Application.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Application/"]
COPY ["src/Services/Reservation/AlgoTecture.Reservation.Domain/AlgoTecture.Reservation.Domain.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Domain/"]
COPY ["src/Services/Reservation/AlgoTecture.Reservation.Infrastructure/AlgoTecture.Reservation.Infrastructure.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Infrastructure/"]

COPY ["src/BuildingBlocks/AlgoTecture.GeoAdminSearch/AlgoTecture.GeoAdminSearch.csproj", "src/BuildingBlocks/AlgoTecture.GeoAdminSearch/"]
COPY ["src/BuildingBlocks/AlgoTecture.HttpClient/AlgoTecture.HttpClient.csproj", "src/BuildingBlocks/AlgoTecture.HttpClient/"]
COPY ["src/BuildingBlocks/AlgoTecture.Utils/AlgoTecture.Utils.csproj", "src/BuildingBlocks/AlgoTecture.Utils/"]

COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Reservation.Contracts/AlgoTecture.Reservation.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Reservation.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Space.Contracts/AlgoTecture.Space.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Space.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecturer.TelegramBot.Contracts/AlgoTecturer.TelegramBot.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecturer.TelegramBot.Contracts/"]

RUN dotnet restore "src/TelegramBot/AlgoTecture.TelegramBot.Api/AlgoTecture.TelegramBot.Api.csproj"

COPY . .

WORKDIR "/src/src/TelegramBot/AlgoTecture.TelegramBot.Api"
RUN dotnet publish "AlgoTecture.TelegramBot.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish \
    /p:UseAppHost=false \
    /p:PublishReadyToRun=false \
    /p:PublishTrimmed=false



FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

RUN apk add --no-cache tzdata

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

RUN find /app -name "*.pdb" -delete && \
    find /app -name "*.xml" -delete

EXPOSE 8080

ENTRYPOINT ["dotnet", "AlgoTecture.TelegramBot.Api.dll"]

