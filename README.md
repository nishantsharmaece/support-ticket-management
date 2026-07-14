# Support Ticket Management

A system for creating, tracking, assigning, and resolving customer support tickets.

## Technology Stack

- **.NET 8** — target framework
- **ASP.NET Core MVC** — server-rendered UI
- **ASP.NET Core Web API** — REST endpoints
- **Entity Framework Core** — data access with SQLite provider
- **SQLite** — development and assessment database
- **Bootstrap 5** — UI styling and layout
- **xUnit** — integration tests

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Solution Structure

```
SupportTicketManagement.sln
src/
  SupportTicketManagement.Domain/
  SupportTicketManagement.Application/
  SupportTicketManagement.Infrastructure/
  SupportTicketManagement.Web/
tests/
  SupportTicketManagement.Tests/
database/
docs/
```

## Setup

1. Clone the repository.
2. Restore dependencies:

   ```bash
   dotnet restore
   ```

3. Build the solution:

   ```bash
   dotnet build
   ```

## Run

From the repository root:

```bash
dotnet run --project src/SupportTicketManagement.Web
```

The web application listens on the URLs configured in `src/SupportTicketManagement.Web/Properties/launchSettings.json`.

## Database

SQLite is configured in `src/SupportTicketManagement.Web/appsettings.json`:

```
Data Source=database/support-tickets.db
```

On application startup, pending EF Core migrations are applied and seed data is inserted idempotently. See [database/setup-notes.md](database/setup-notes.md) for migration commands, schema summary, and seed data details.

Migration source files: `src/SupportTicketManagement.Infrastructure/Persistence/Migrations/`  
Mirrored SQL script: `database/schema-or-migrations/InitialCreate.sql`

## Test

```bash
dotnet test
```

## Repository Layout

| Path | Purpose |
|------|---------|
| `src/SupportTicketManagement.Domain/` | Entities, enums, domain rules |
| `src/SupportTicketManagement.Application/` | DTOs, service interfaces, use-case services |
| `src/SupportTicketManagement.Infrastructure/` | EF Core persistence, repositories, seeding |
| `src/SupportTicketManagement.Web/` | MVC UI, API controllers, DI composition root |
| `tests/` | xUnit test projects |
| `database/` | Database files and setup notes |
| `docs/` | Project documentation |
| `tool-specific/cursor-workflow/` | Cursor workflow artifacts |
| `.cursor/rules/` | Cursor IDE rules |

## Application Layer

Use-case services are registered via `AddApplication()` and delegate to repository interfaces:

| Service | Operations |
|---------|------------|
| `ITicketService` | Create, list (search/filter), get by id, update |
| `ICommentService` | Add comment to ticket |
| `IUserService` | List seeded users |

Repository implementations live in Infrastructure and are registered via `AddInfrastructure()`. API and MVC controllers are not yet implemented.
