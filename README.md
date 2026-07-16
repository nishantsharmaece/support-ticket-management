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

No additional configuration or secrets are required. The SQLite connection string in `appsettings.json` points to a local file under `database/`.

For the Stretch authentication feature, a seeded admin account is created on first startup (see [Authentication](#authentication)).

## Run

From the repository root:

```bash
dotnet run --project src/SupportTicketManagement.Web
```

The web application listens on the URLs configured in `src/SupportTicketManagement.Web/Properties/launchSettings.json`:

| Profile | URL |
|---------|-----|
| http (default) | `http://localhost:5030` |
| https | `https://localhost:7199` |
| IIS Express | `http://localhost:6040` |

Open `/Tickets` in a browser for the ticket list UI. Unauthenticated users are redirected to `/Account/Login`.

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

Integration tests cover the mandatory assessment tier:

- Valid and invalid ticket status transitions (state machine)
- Backend validation (400/404 responses)
- Search and filter on the list endpoint

Results: [tool-specific/cursor-workflow/test-results.md](tool-specific/cursor-workflow/test-results.md)

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
| `ITicketService` | Create, list (search/filter), get by id, update, change status |
| `ICommentService` | Add comment to ticket |
| `IUserService` | List seeded users |

Repository implementations live in Infrastructure and are registered via `AddInfrastructure()`.

Status transitions are enforced in the Application layer via `TicketStatusStateMachine` (Domain). Invalid transitions return HTTP 409 (API) or a user-visible error (MVC).

## MVC UI

The default route is the ticket list (`/Tickets`). MVC controllers call Application services directly (no HTTP loopback). Ticket management pages require authentication (`[Authorize]`).

| Screen | Route |
|--------|-------|
| Login | `/Account/Login` |
| Ticket list (search/filter) | `/Tickets` |
| Create ticket | `/Tickets/Create` |
| Ticket details | `/Tickets/Details/{id}` |
| Edit ticket | `/Tickets/Edit/{id}` |

Ticket details includes status change (state machine) and add-comment forms. Bootstrap 5 is used for layout and styling.

## REST API

Base URL when running locally: `http://localhost:5030` (see `launchSettings.json`).

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/tickets` | Create ticket (status = Open) |
| GET | `/api/tickets` | List tickets |
| GET | `/api/tickets?search={keyword}` | Search by title/description |
| GET | `/api/tickets?status={status}` | Filter by status |
| GET | `/api/tickets/{id}` | Get ticket detail with comments |
| PUT | `/api/tickets/{id}` | Update ticket fields (not status) |
| PATCH | `/api/tickets/{id}/status` | Change ticket status (state machine) |
| POST | `/api/tickets/{id}/comments` | Add comment |

Enum values in JSON: `Low`, `Medium`, `High` (priority); `Open`, `InProgress`, `Resolved`, `Closed`, `Cancelled` (status).

Error responses use the `ErrorResponse` shape (`title`, `status`, `errors[]`). Status codes: 400 validation, 404 not found, 409 invalid status transition, 500 unhandled error (no stack traces).

## Documentation

| Document | Purpose |
|----------|---------|
| [docs/requirements-analysis.md](docs/requirements-analysis.md) | Functional and non-functional requirements |
| [docs/acceptance-criteria.md](docs/acceptance-criteria.md) | Pass/fail criteria for Core features |
| [docs/architecture.md](docs/architecture.md) | Layering and project structure |
| [docs/api-contract.md](docs/api-contract.md) | REST API contract |
| [docs/ui-flow.md](docs/ui-flow.md) | MVC screen flows |
| [docs/test-strategy.md](docs/test-strategy.md) | Integration test approach |
| [docs/reflection.md](docs/reflection.md) | Project reflection and lessons learned |
| [tool-specific/cursor-workflow/project-context.md](tool-specific/cursor-workflow/project-context.md) | Persistent project context |

## Authentication

Stretch feature: ASP.NET Core Identity with cookie authentication, using the same SQLite database as ticket data.

| Item | Value |
|------|-------|
| Login URL | `/Account/Login` |
| Seeded admin email | `admin@example.com` |
| Seeded admin password | `Admin@123` |

The admin user and `Admin` role are seeded idempotently on startup. Registration, password reset, email confirmation, and role management UI are not implemented. The REST API remains open (no `[Authorize]` on API controllers).

Logout is available from the navigation bar when signed in.

## Security

No credentials or API keys are stored in source control. Local SQLite paths only. User secrets and `.env` files are gitignored.
