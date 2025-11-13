# AlgoTecture Developer Onboarding Guide

## 1. Platform overview
AlgoTecture streamlines interactions between people who design, build, and operate buildings. The platform is composed of domain-oriented .NET 8 services, a Telegram bot front end, and supporting infrastructure provided via Docker Compose for local work.

### Core pillars
- **Domain services** – Identity, User, Space, Reservation, and ApiGateway live under `src/Services` and expose HTTP APIs, messaging endpoints, and database migrations.
- **AI Core** – `src/AICore` contains intent-recognition logic that turns natural language into structured commands.
- **Telegram Bot** – `src/TelegramBot` orchestrates the user experience by calling the gateway and reacting to domain events.
- **Building blocks** – Shared utilities and contracts under `src/BuildingBlocks` keep cross-cutting concerns in sync.

## 2. Repository layout at a glance
```text
├── docs/                        # Project documentation
├── infra/                       # Docker Compose files and local infrastructure volumes
├── src/
│   ├── AICore/                  # LLM-powered intent recognition service
│   ├── BuildingBlocks/          # Shared libraries (HTTP helpers, GeoAdmin client, contracts)
│   ├── Services/
│   │   ├── ApiGateway/          # YARP reverse proxy configuration
│   │   ├── Identity/            # Authentication domain (API, application, domain, infrastructure, migrator)
│   │   ├── Reservation/         # Parking reservation domain and migrator
│   │   ├── Space/               # Space catalogue with PostGIS support and providers
│   │   └── User/                # User profile management and migrator
│   └── TelegramBot/             # Bot API, EF Core storage, Refit clients, MassTransit consumers
├── tests/
│   ├── AlgoTecture.Identity.Tests/      # Integration tests for Identity
│   ├── AlgoTecture.Space.Tests/         # Integration tests for Space and API Gateway
│   └── AlgoTecture.TelegramBot.Tests/   # Test doubles and scenarios for the bot
└── docker-bake.hcl              # Centralised Docker build definitions for services
```

## 3. CI/CD, deployment, and secrets
### Build pipeline
`.github/workflows/dotnet-build.yml` provides a reusable `workflow_dispatch` build that restores and compiles the entire solution on `ubuntu-latest` with .NET 8.【F:.github/workflows/dotnet-build.yml†L1-L26】

### Image publishing
Each service has a dedicated "Publish" workflow (for example `publish-telegrambot.yml`) that logs into GitHub Container Registry (GHCR) and pushes versioned images using the GitHub actor credentials provided through `secrets.GITHUB_TOKEN`.【F:.github/workflows/publish-telegrambot.yml†L1-L35】

### Remote deployments
Matching "Deploy" workflows (for example `deploy-telegrambot.yml`) connect to the production host over SSH using the `appleboy/ssh-action`. They rely on repository secrets `PROD_SERVER_HOST`, `PROD_SERVER_USER`, and `PROD_SERVER_SSH_KEY`, then run Docker Compose remotely to pull the requested tag and restart the service.【F:.github/workflows/deploy-telegrambot.yml†L1-L32】

### Configuration templates and secrets management
Every service ships an `appsettings.Template.json` file describing the required configuration keys (database connection strings, message bus endpoints, API clients, etc.).【F:src/Services/Identity/AlgoTecture.Identity.Api/appsettings.Template.json†L1-L43】【F:src/TelegramBot/AlgoTecture.TelegramBot.Api/appsettings.Template.json†L1-L42】

Copy the template to `appsettings.Development.json` (or another environment-specific name) and provide real secrets either via
1. user secrets (`dotnet user-secrets`),
2. environment variables consumed by the host, or
3. Docker Compose overrides in deployment environments.

## 4. Local development on Windows (with Docker Desktop)
### Prerequisites
- Windows 11 with **WSL2** enabled and Docker Desktop configured to use WSL2 back end.
- .NET 8 SDK installed either on the Windows side or inside the WSL distribution used for development.
- Git and your preferred IDE (Visual Studio, JetBrains Rider, or VS Code).

### Clone and prepare the repository
```powershell
wsl --distribution Ubuntu
cd /mnt/c/Dev
git clone https://github.com/<your-org>/AlgoTecture.git
cd AlgoTecture
```

### Start shared infrastructure
Use Docker Desktop (PowerShell or WSL shell both work) to boot the dependencies:
```powershell
docker compose -f infra/docker-compose.infrastructure.yml up -d
```
This command exposes PostgreSQL (5432), RabbitMQ (5672/15672), Redis (6379), and Seq (5101/5341) locally.【F:infra/docker-compose.infrastructure.yml†L1-L43】

### Apply configuration templates
For each service you plan to run locally:
```powershell
Copy-Item src\Services\Identity\AlgoTecture.Identity.Api\appsettings.Template.json `
          src\Services\Identity\AlgoTecture.Identity.Api\appsettings.Development.json
```
Update the copied file with connection strings matching the Docker compose services (`Host=localhost;Port=5432;Username=postgres;Password=1234;Database=algotecture`). Repeat for Space, Reservation, User, ApiGateway, and the TelegramBot.

### Example: modifying AICore and verifying the change
1. Open the solution in your IDE and edit the relevant code under `src/AICore`. For example, adjust prompt construction in `IntentRecognitionService`.
2. Run unit tests or targeted projects:
   ```powershell
   dotnet test src/AICore/AlgoTecture.AICore.Application/AlgoTecture.AICore.Application.csproj
   ```
3. Launch the API with the development configuration:
   ```powershell
   dotnet run --project src/AICore/AlgoTecture.AICore.Api/AlgoTecture.AICore.Api.csproj
   ```

### Rebuilding and redeploying the Telegram bot locally
With infrastructure running and services configured:
1. Ensure the gateway and supporting services are running (`dotnet run` inside the Identity, User, Space, Reservation, and ApiGateway projects or start their Docker containers).
2. Rebuild the bot API:
   ```powershell
   dotnet run --project src/TelegramBot/AlgoTecture.TelegramBot.Api/AlgoTecture.TelegramBot.Api.csproj
   ```
   The bot will connect to Redis, RabbitMQ, and the REST APIs using the values from `appsettings.Development.json`.
3. To publish a Docker image for manual testing, use Docker Desktop:
   ```powershell
   docker build -f src/TelegramBot/AlgoTecture.TelegramBot.Api/Dockerfile -t algotecture-telegrambot:dev .
   docker run --rm -p 8080:8080 --env-file telegrambot.env algotecture-telegrambot:dev
   ```
   Craft the `telegrambot.env` file from the template to inject secrets such as the Telegram bot token.

### Cleaning up
When you finish, stop the infrastructure containers:
```powershell
docker compose -f infra/docker-compose.infrastructure.yml down
```
This preserves volumes under `infra/volumes` so that databases remain available for the next session.

## 5. Next steps for new contributors
- Review the contracts under `src/BuildingBlocks/Contracts` to understand integration events and request/response contracts.【F:src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/Events/IdentityCreated.cs†L1-L10】【F:src/BuildingBlocks/Contracts/AlgoTecture.Identity.Contracts/Commands/TelegramLoginCommand.cs†L1-L8】
- Explore integration tests in `tests/AlgoTecture.Space.Tests` and `tests/AlgoTecture.Identity.Tests` for practical examples of how services collaborate.【F:tests/AlgoTecture.Space.Tests/Integration/SpaceControllerTests.cs†L1-L34】【F:tests/AlgoTecture.Identity.Tests/IdentityControllerTests.cs†L1-L40】
- Use the GitHub Actions workflows as a reference when promoting changes to staging or production; publishing first, then triggering the corresponding deploy job mirrors the production flow.
