FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AlgoTecture.User.Api/AlgoTecture.User.Api.csproj", "AlgoTecture.User.Api/"]
COPY ["AlgoTecture.User.Application/AlgoTecture.User.Application.csproj", "AlgoTecture.User.Application/"]
COPY ["AlgoTecture.User.Contracts/AlgoTecture.User.Contracts.csproj", "AlgoTecture.User.Contracts/"]
COPY ["AlgoTecture.Shared.Contracts/AlgoTecture.Shared.Contracts.csproj", "AlgoTecture.Shared.Contracts/"]
COPY ["AlgoTecture.User.Infrastructure/AlgoTecture.User.Infrastructure.csproj", "AlgoTecture.User.Infrastructure/"]
COPY ["AlgoTecture.User.Domain/AlgoTecture.User.Domain.csproj", "AlgoTecture.User.Domain/"]
COPY ["AlgoTecture.Identity.Contracts/AlgoTecture.Identity.Contracts.csproj", "AlgoTecture.Identity.Contracts/"]

RUN dotnet restore "AlgoTecture.User.Api/AlgoTecture.User.Api.csproj"

COPY . .

WORKDIR "/src/AlgoTecture.User.Api"
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
