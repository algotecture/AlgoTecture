FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Reservation/AlgoTecture.Reservation.Api/AlgoTecture.Reservation.Api.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Api/"]
COPY ["src/Services/Reservation/AlgoTecture.Reservation.Application/AlgoTecture.Reservation.Application.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Application/"]
COPY ["src/Services/Reservation/AlgoTecture.Reservation.Domain/AlgoTecture.Reservation.Domain.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Domain/"]
COPY ["src/Services/Reservation/AlgoTecture.Reservation.Infrastructure/AlgoTecture.Reservation.Infrastructure.csproj", "src/Services/Reservation/AlgoTecture.Reservation.Infrastructure/"]

COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Reservation.Contracts/AlgoTecture.Reservation.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Reservation.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]

RUN dotnet restore "src/Services/Reservation/AlgoTecture.Reservation.Api/AlgoTecture.Reservation.Api.csproj"

COPY . .

WORKDIR "/src/src/Services/Reservation/AlgoTecture.Reservation.Api"
RUN dotnet publish "AlgoTecture.Reservation.Api.csproj" \
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

ENTRYPOINT ["dotnet", "AlgoTecture.Reservation.Api.dll"]
