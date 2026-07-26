# Identity Accounts (Stretch)

Human-readable mirror of ASP.NET Core Identity seed data created by `IdentitySeeder`.

**Source of truth:** `src/SupportTicketManagement.Infrastructure/Seeding/IdentitySeeder.cs`

These files are documentation only. The application does not load this markdown at runtime.

## Role

| Name |
|------|
| Admin |

Created only if the role does not already exist.

## Admin user (local demo only)

| Field | Value |
|-------|-------|
| Email / UserName | `admin@example.com` |
| Password | `Admin@123` |
| EmailConfirmed | `true` |
| Role | Admin |

Credentials are **local demo values only** (same as documented in the project README). They are not production secrets. Do not reuse them outside this assessment environment.

## Idempotency

- If the Admin role already exists, role creation is skipped.
- If a user with email `admin@example.com` already exists, user and role assignment seeding is skipped.
