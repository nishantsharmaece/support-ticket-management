# Database Setup Notes

**Scope:** Core only  
**Traces to:** [data-model.md](../docs/data-model.md), [architecture.md](../docs/architecture.md)

## Overview

The application uses **SQLite** via Entity Framework Core. Schema is managed through EF Core migrations in `src/SupportTicketManagement.Infrastructure/Persistence/Migrations/`. A mirrored SQL script is available in `database/schema-or-migrations/InitialCreate.sql` for assessment visibility.

## Connection String

Configured in `src/SupportTicketManagement.Web/appsettings.json`:

```
Data Source=database/support-tickets.db
```

The database file is created at the repository root under `database/` when the application starts. Run commands from the repository root so relative paths resolve correctly.

## Apply Migrations

### Option A: Application startup (recommended)

Migrations are applied automatically when the web application starts:

```bash
dotnet run --project src/SupportTicketManagement.Web
```

### Option B: EF Core CLI

From the repository root:

```bash
dotnet ef database update \
  --project src/SupportTicketManagement.Infrastructure/SupportTicketManagement.Infrastructure.csproj \
  --startup-project src/SupportTicketManagement.Web/SupportTicketManagement.Web.csproj \
  --connection "Data Source=database/support-tickets.db"
```

Requires the [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) global tool (8.0.x).

## Seed Data

**Human-readable mirror:** [`database/seed-data/`](seed-data/) (markdown/JSON for assessment; not loaded at runtime).

Seed data runs idempotently after migrations on application startup (`ApplyMigrationsAndSeedAsync`). Domain seeding is skipped if any user already exists. Identity seeding is skipped if the admin user already exists.

| Data | Count | Notes |
|------|-------|-------|
| Users | 5 | Jane Agent, John Manager, Alice Agent, Bob Agent, Carol Manager |
| Tickets | 3 | Mixed statuses (Open, InProgress, Resolved) for demo and search |
| Comments | 2 | Sample comments on first two tickets |
| Identity (stretch) | 1 role + 1 user | `Admin` role; demo login documented in [`seed-data/identity-accounts.md`](seed-data/identity-accounts.md) |

Ticket users have no passwords. The Identity admin password is a **local demo value only** (same as README); no production secrets are stored here.

### Verify after restart

1. From the repository root, start the app: `dotnet run --project src/SupportTicketManagement.Web`
2. Confirm SQLite file exists at `database/support-tickets.db`
3. Open the UI or API and confirm five assignee users and three sample tickets (with comments on the first two)
4. (Stretch) Sign in at `/Account/Login` with the demo admin from `seed-data/identity-accounts.md`
5. Stop and start again — data should persist; seeders should not duplicate rows

## Schema Summary

| Table | Purpose |
|-------|---------|
| Users | Seeded internal users (creator/assignee references) |
| Tickets | Support tickets with status, priority, and ownership |
| Comments | Append-only messages linked to tickets |

### Indexes

- `UQ_User_Email` — unique email on Users
- `IX_Ticket_Status` — status filter support

### Foreign Keys

- Ticket → User (CreatedBy, AssignedTo): **Restrict**
- Comment → Ticket: **Cascade**
- Comment → User (CreatedBy): **Restrict**

## Local Database Files

SQLite database files (`*.db`, `*.db-wal`, `*.db-shm`) are gitignored. Delete `database/support-tickets.db` to reset the local database; migrations and seed data will recreate it on next startup.
