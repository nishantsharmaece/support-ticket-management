# Seed Data (Documentation Mirror)

This folder documents the data the application seeds at startup. It is **not** executed by the runtime.

## How seeding is applied at startup

On web application start (`Program.cs` → `ApplyMigrationsAndSeedAsync`):

1. EF Core applies pending migrations to SQLite (`database/support-tickets.db`).
2. `DatabaseSeeder` inserts domain users, then sample tickets and comments (idempotent: skipped if any user already exists).
3. `IdentitySeeder` ensures the `Admin` role and demo admin login account exist (idempotent).

**Code locations:**

| Seeder | File |
|--------|------|
| Domain users, tickets, comments | `src/SupportTicketManagement.Infrastructure/Seeding/DatabaseSeeder.cs` |
| Identity role + admin user | `src/SupportTicketManagement.Infrastructure/Seeding/IdentitySeeder.cs` |

## What these files are for

| File | Contents |
|------|----------|
| [users.md](users.md) / [users.json](users.json) | Five ticket users (Agent / Manager) |
| [sample-tickets.md](sample-tickets.md) | Three sample tickets and two comments |
| [identity-accounts.md](identity-accounts.md) | Stretch Identity Admin role and demo login |

Use this folder for assessment review and onboarding. To change seed content, edit the C# seeders, then update these files to match. Do not wire the app to read JSON/markdown from this directory—that would risk diverging from or breaking runtime seeding.

## Reset and re-seed

1. Stop the application.
2. Delete `database/support-tickets.db` (and any `-wal` / `-shm` sidecars if present).
3. Run `dotnet run --project src/SupportTicketManagement.Web` from the repository root.
4. Confirm users, sample tickets/comments, and the demo admin login are present again.

See also [../setup-notes.md](../setup-notes.md).
