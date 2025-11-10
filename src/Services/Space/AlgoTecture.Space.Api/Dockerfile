FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Space/AlgoTecture.Space.Api/AlgoTecture.Space.Api.csproj", "src/Services/Space/AlgoTecture.Space.Api/"]
COPY ["src/Services/Space/AlgoTecture.Space.Application/AlgoTecture.Space.Application.csproj", "src/Services/Space/AlgoTecture.Space.Application/"]
COPY ["src/Services/Space/AlgoTecture.Space.Domain/AlgoTecture.Space.Domain.csproj", "src/Services/Space/AlgoTecture.Space.Domain/"]
COPY ["src/Services/Space/AlgoTecture.Space.Infrastructure/AlgoTecture.Space.Infrastructure.csproj", "src/Services/Space/AlgoTecture.Space.Infrastructure/"]

COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Space.Contracts/AlgoTecture.Space.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Space.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]

RUN dotnet restore "src/Services/Space/AlgoTecture.Space.Api/AlgoTecture.Space.Api.csproj"

COPY . .

WORKDIR "/src/src/Services/Space/AlgoTecture.Space.Api"
RUN dotnet publish "AlgoTecture.Space.Api.csproj" \
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

ENTRYPOINT ["dotnet", "AlgoTecture.Space.Api.dll"]
