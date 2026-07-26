# Sample Tickets and Comments

Human-readable mirror of ticket/comment rows created by `DatabaseSeeder` after users are inserted. Timestamps are relative to seed time (`DateTime.UtcNow` at startup).

**Source of truth:** `src/SupportTicketManagement.Infrastructure/Seeding/DatabaseSeeder.cs`

These files are documentation only. The application does not load this markdown at runtime.

## Tickets (3)

| # | Title | Description | Priority | Status | Assigned To | Created By | CreatedAt | UpdatedAt |
|---|-------|-------------|----------|--------|-------------|------------|-----------|-----------|
| 1 | Cannot log in to portal | User reports invalid credentials after password reset. | High | Open | Jane Agent | John Manager | now − 2 days | now − 2 days |
| 2 | Printer not responding | Office printer on floor 2 shows offline status. | Medium | InProgress | Alice Agent | Jane Agent | now − 1 day | now − 3 hours |
| 3 | Email sync delay | Mailbox sync is delayed by several hours. | Low | Resolved | Bob Agent | Carol Manager | now − 5 days | now − 1 day |

Enum storage in SQLite: `Priority` and `TicketStatus` are stored as integers by EF Core (matching domain enums).

## Comments (2)

| # | Ticket | Message | Created By | CreatedAt |
|---|--------|---------|------------|-----------|
| 1 | Cannot log in to portal | Confirmed password reset email was delivered. | Jane Agent | now − 1 day |
| 2 | Printer not responding | Restarted print spooler and checked network cable. | Alice Agent | now − 2 hours |

No comment is seeded on ticket 3 (Email sync delay).
