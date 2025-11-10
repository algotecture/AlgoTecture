FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/User/AlgoTecture.User.Api/AlgoTecture.User.Api.csproj", "src/Services/User/AlgoTecture.User.Api/"]
COPY ["src/Services/User/AlgoTecture.User.Application/AlgoTecture.User.Application.csproj", "src/Services/User/AlgoTecture.User.Application/"]
COPY ["src/Services/User/AlgoTecture.User.Domain/AlgoTecture.User.Domain.csproj", "src/Services/User/AlgoTecture.User.Domain/"]
COPY ["src/Services/User/AlgoTecture.User.Infrastructure/AlgoTecture.User.Infrastructure.csproj", "src/Services/User/AlgoTecture.User.Infrastructure/"]

COPY ["src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.User.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]

RUN dotnet restore "src/Services/User/AlgoTecture.User.Api/AlgoTecture.User.Api.csproj"

COPY . .

WORKDIR "/src/src/Services/User/AlgoTecture.User.Api"
RUN dotnet publish "AlgoTecture.User.Api.csproj" \
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

ENTRYPOINT ["dotnet", "AlgoTecture.User.Api.dll"]
