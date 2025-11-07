FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AlgoTecture.Space.Api/AlgoTecture.Space.Api.csproj", "AlgoTecture.Space.Api/"]
COPY ["AlgoTecture.Space.Application/AlgoTecture.Space.Application.csproj", "AlgoTecture.Space.Application/"]
COPY ["AlgoTecture.Space.Contracts/AlgoTecture.Space.Contracts.csproj", "AlgoTecture.Space.Contracts/"]
COPY ["AlgoTecture.Space.Domain/AlgoTecture.Space.Domain.csproj", "AlgoTecture.Space.Domain/"]
COPY ["AlgoTecture.Space.Infrastructure/AlgoTecture.Space.Infrastructure.csproj", "AlgoTecture.Space.Infrastructure/"]

RUN dotnet restore "AlgoTecture.Space.Api/AlgoTecture.Space.Api.csproj"

COPY . .

WORKDIR "/src/AlgoTecture.Space.Api"
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
