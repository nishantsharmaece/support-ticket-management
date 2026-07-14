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

SQLite connection string placeholder (configured in `appsettings.json`):

```
Data Source=database/support-tickets.db
```

Database creation, migrations, and seed data will be added in a later phase.

## Test

```bash
dotnet test
```

## Repository Layout

| Path | Purpose |
|------|---------|
| `src/` | Application source projects |
| `tests/` | xUnit test projects |
| `database/` | Database files and setup notes |
| `docs/` | Project documentation |
| `tool-specific/cursor-workflow/` | Cursor workflow artifacts |
| `.cursor/rules/` | Cursor IDE rules |
