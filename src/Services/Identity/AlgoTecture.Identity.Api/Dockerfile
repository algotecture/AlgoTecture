FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AlgoTecture.Identity.Api/AlgoTecture.Identity.Api.csproj", "AlgoTecture.Identity.Api/"]
COPY ["AlgoTecture.Identity.Application/AlgoTecture.Identity.Application.csproj", "AlgoTecture.Identity.Application/"]
COPY ["AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "AlgoTecture.Identity.Contracts/"]
COPY ["AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "AlgoTecture.Shared.Contracts/"]
COPY ["AlgoTecture.Identity.Infrastructure/AlgoTecture.Identity.Infrastructure.csproj", "AlgoTecture.Identity.Infrastructure/"]
COPY ["AlgoTecture.Identity.Domain/AlgoTecture.Identity.Domain.csproj", "AlgoTecture.Identity.Domain/"]
COPY ["AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "AlgoTecture.User.Contracts/"]

RUN dotnet restore "AlgoTecture.Identity.Api/AlgoTecture.Identity.Api.csproj"

COPY . .

WORKDIR "/src/AlgoTecture.Identity.Api"
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
