# Seeded Ticket Users

Human-readable mirror of domain users created by `DatabaseSeeder` (table: `Users` / DbSet `TicketUsers`).

**Source of truth:** `src/SupportTicketManagement.Infrastructure/Seeding/DatabaseSeeder.cs`

These files are documentation only. The application does not load this markdown or `users.json` at runtime.

| Name | Email | Role |
|------|-------|------|
| Jane Agent | jane@example.com | Agent |
| John Manager | john@example.com | Manager |
| Alice Agent | alice@example.com | Agent |
| Bob Agent | bob@example.com | Agent |
| Carol Manager | carol@example.com | Manager |

Notes:

- No passwords on these users; they are selectable creators/assignees for tickets, not login accounts.
- Seeding is skipped if any user row already exists.
- Structured copy: [users.json](users.json)
