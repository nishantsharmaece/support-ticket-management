# Prompt 9 — Database seed-data folder
_Exported on 7/26/2026 from Cursor agent transcript [b123350f-95b4-4416-af9a-6e2cc074a67f](b123350f-95b4-4416-af9a-6e2cc074a67f)_

---

**User**

Context: Official guide requires:
database/
  schema-or-migrations/
  seed-data/
  setup-notes.md

`schema-or-migrations/` and `setup-notes.md` already exist. `seed-data/` is missing. Seeders exist in code (`DatabaseSeeder`, `IdentitySeeder`). Do not break runtime seeding.

Task:
1. Create `database/seed-data/` with human-readable seed content (markdown and/or SQL/JSON) matching what the app actually seeds (users, sample tickets/comments if any, identity accounts if present).
2. Add `database/seed-data/README.md` explaining how seeding is applied at startup vs these files.
3. Update `database/setup-notes.md` only if needed to document seed-data location, DB choice (SQLite), migration commands, and verify-after-restart steps (no real secrets).
4. Remind me to export this chat to `ai-prompts/documentation/09-database-seed-data.md`.

---

**Cursor**

I'll inspect the existing database docs and the actual `DatabaseSeeder` / `IdentitySeeder` so the seed-data folder matches runtime seeding.

---

**Cursor**

Next I'll read the seeder implementations and related docs so the seed files mirror exact runtime data.

---

**Cursor**

I have the exact seed payloads; creating `database/seed-data/` and updating setup notes to point at it without changing runtime seeders.

---

**Cursor**

Fixing a path typo in the seed-data README, then updating setup notes.

---

**Cursor**

Created `database/seed-data/` as a documentation mirror of what `DatabaseSeeder` and `IdentitySeeder` insert. Runtime seeding is unchanged.

### What changed

| Path | Purpose |
|------|---------|
| `database/seed-data/README.md` | Startup seeding vs these files; reset/re-seed steps |
| `database/seed-data/users.md` + `users.json` | 5 ticket users |
| `database/seed-data/sample-tickets.md` | 3 tickets + 2 comments |
| `database/seed-data/identity-accounts.md` | Admin role + demo login (`admin@example.com`) |
| `database/setup-notes.md` | Linked `seed-data/`, clarified Identity seed, added verify-after-restart |

SQLite, migration commands, and seed counts were already in setup notes; they now also point at the mirror folder and include restart verification.

**Reminder:** Export this chat to `ai-prompts/documentation/09-database-seed-data.md`.

---
