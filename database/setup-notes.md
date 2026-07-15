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

Seed data runs idempotently after migrations on application startup. If users already exist, seeding is skipped.

| Data | Count | Notes |
|------|-------|-------|
| Users | 5 | Jane Agent, John Manager, Alice Agent, Bob Agent, Carol Manager |
| Tickets | 3 | Mixed statuses (Open, InProgress, Resolved) for demo and search |
| Comments | 2 | Sample comments on first two tickets |

No credentials or secrets are included in seed data.

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
