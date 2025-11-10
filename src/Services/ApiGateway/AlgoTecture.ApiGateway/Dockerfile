FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Services/ApiGateway/AlgoTecture.ApiGateway/AlgoTecture.ApiGateway.csproj", "src/Services/ApiGateway/AlgoTecture.ApiGateway/"]


COPY ["src/BuildingBlocks/AlgoTecture.Utils/AlgoTecture.Utils.csproj", "src/BuildingBlocks/AlgoTecture.Utils/"]
COPY ["src/BuildingBlocks/AlgoTecture.HttpClient/AlgoTecture.HttpClient.csproj", "src/BuildingBlocks/AlgoTecture.HttpClient/"]
COPY ["src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "src/BuildingBlocks/Contracts/AlgoTecture.Shared.Contracts/"]

RUN dotnet restore "src/Services/ApiGateway/AlgoTecture.ApiGateway/AlgoTecture.ApiGateway.csproj"

COPY . .

WORKDIR "/src/src/Services/ApiGateway/AlgoTecture.ApiGateway"
RUN dotnet publish "AlgoTecture.ApiGateway.csproj" \
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

ENTRYPOINT ["dotnet", "AlgoTecture.ApiGateway.dll"]
