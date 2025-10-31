FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AlgoTecture.TelegramBot.Api/AlgoTecture.TelegramBot.Api.csproj", "AlgoTecture.TelegramBot.Api/"]
COPY ["AlgoTecture.AICore.Application/AlgoTecture.AICore.Application.csproj", "AlgoTecture.AICore.Application/"]
COPY ["AlgoTecture.AICore.Contracts/AlgoTecture.AICore.Contracts.csproj", "AlgoTecture.AICore.Contracts/"]
COPY ["AlgoTecture.GeoAdminSearch/AlgoTecture.GeoAdminSearch.csproj", "AlgoTecture.GeoAdminSearch/"]
COPY ["AlgoTecture.HttpClient/AlgoTecture.HttpClient.csproj", "AlgoTecture.HttpClient/"]
COPY ["AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "AlgoTecture.Identity.Contracts/"]
COPY ["AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "AlgoTecture.Shared.Contracts/"]
COPY ["AlgoTecture.Reservation.Application/AlgoTecture.Reservation.Application.csproj", "AlgoTecture.Reservation.Application/"]
COPY ["AlgoTecture.Reservation.Contracts/AlgoTecture.Reservation.Contracts.csproj", "AlgoTecture.Reservation.Contracts/"]
COPY ["AlgoTecture.Reservation.Domain/AlgoTecture.Reservation.Domain.csproj", "AlgoTecture.Reservation.Domain/"]
COPY ["AlgoTecture.Reservation.Infrastructure/AlgoTecture.Reservation.Infrastructure.csproj", "AlgoTecture.Reservation.Infrastructure/"]
COPY ["AlgoTecture.TelegramBot.Application/AlgoTecture.TelegramBot.Application.csproj", "AlgoTecture.TelegramBot.Application/"]
COPY ["AlgoTecture.Space.Contracts/AlgoTecture.Space.Contracts.csproj", "AlgoTecture.Space.Contracts/"]
COPY ["AlgoTecture.Space.Domain/AlgoTecture.Space.Domain.csproj", "AlgoTecture.Space.Domain/"]
COPY ["AlgoTecture.TelegramBot.Domain/AlgoTecture.TelegramBot.Domain.csproj", "AlgoTecture.TelegramBot.Domain/"]
COPY ["AlgoTecture.TelegramBot.Infrastructure/AlgoTecture.TelegramBot.Infrastructure.csproj", "AlgoTecture.TelegramBot.Infrastructure/"]
COPY ["AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "AlgoTecture.User.Contracts/"]
COPY ["AlgoTecture.Utils/AlgoTecture.Utils.csproj", "AlgoTecture.Utils/"]

RUN dotnet restore "AlgoTecture.TelegramBot.Api/AlgoTecture.TelegramBot.Api.csproj"

COPY . .

WORKDIR "/src/AlgoTecture.TelegramBot.Api"
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

