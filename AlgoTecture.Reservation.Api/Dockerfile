FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AlgoTecture.Reservation.Api/AlgoTecture.Reservation.Api.csproj", "AlgoTecture.Reservation.Api/"]
COPY ["AlgoTecture.Reservation.Application/AlgoTecture.Reservation.Application.csproj", "AlgoTecture.Reservation.Application/"]
COPY ["AlgoTecture.Reservation.Contracts/AlgoTecture.Reservation.Contracts.csproj", "AlgoTecture.Reservation.Contracts/"]
COPY ["AlgoTecture.Reservation.Domain/AlgoTecture.Reservation.Domain.csproj", "AlgoTecture.Reservation.Domain/"]
COPY ["AlgoTecture.Reservation.Infrastructure/AlgoTecture.Reservation.Infrastructure.csproj", "AlgoTecture.Reservation.Infrastructure/"]

RUN dotnet restore "AlgoTecture.Reservation.Api/AlgoTecture.Reservation.Api.csproj"

COPY . .

WORKDIR "/src/AlgoTecture.Reservation.Api"
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
