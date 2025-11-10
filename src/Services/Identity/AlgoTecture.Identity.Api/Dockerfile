FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/Identity/AlgoTecture.Identity.Api/AlgoTecture.Identity.Api.csproj", "src/Services/Identity/AlgoTecture.Identity.Api/"]
COPY ["src/Services/Identity/AlgoTecture.Identity.Application/AlgoTecture.Identity.Application.csproj", "src/Services/Identity/AlgoTecture.Identity.Application/"]
COPY ["src/Services/Identity/AlgoTecture.Identity.Domain/AlgoTecture.Identity.Domain.csproj", "src/Services/Identity/AlgoTecture.Identity.Domain/"]
COPY ["src/Services/Identity/AlgoTecture.Identity.Infrastructure/AlgoTecture.Identity.Infrastructure.csproj", "src/Services/Identity/AlgoTecture.Identity.Infrastructure/"]

COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/"]

RUN dotnet restore "src/Services/Identity/AlgoTecture.Identity.Api/AlgoTecture.Identity.Api.csproj"

COPY . .

WORKDIR "/src/src/Services/Identity/AlgoTecture.Identity.Api"
RUN dotnet publish "AlgoTecture.Identity.Api.csproj" \
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

ENTRYPOINT ["dotnet", "AlgoTecture.Identity.Api.dll"]
