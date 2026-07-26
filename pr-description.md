# PR Description

Part C — Submission artifact for the .NET AI Capability Assessment.  
Describes the Support Ticket Management delivery (Core + Stretch) for review and submission packaging. Section structure follows the Participant Guide "PR Description" template.

**Traces to:** [`README.md`](README.md), [`acceptance-criteria.md`](acceptance-criteria.md), [`design-notes.md`](design-notes.md), [`test-results.md`](test-results.md), [`reflection.md`](reflection.md), [`final-ai-usage-summary.md`](final-ai-usage-summary.md)

**Structure-gate note:** This root file is required by the Participant Guide. Earlier evaluation stopped at the structure gate when mandated root ownership files were missing even though `docs/` content existed.

---

## Summary

Backend-heavy Support Ticket Management system (assessment Option 1) built with .NET 8, ASP.NET Core MVC + Web API, EF Core + SQLite, and Bootstrap 5. Core delivers ticket CRUD, comments, search/filter, server-side validation, and an enforced status state machine. Stretch adds ASP.NET Core Identity (MVC cookie auth) and a light/dark theme toggle. Solution builds cleanly; **22/22** integration tests pass.

---

## Features Implemented

### Core

- Create, list, view, and update support tickets
- Add comments to tickets
- Keyword search (title/description) and status filter (combinable)
- Enforced ticket status state machine (valid transitions succeed; invalid → HTTP 409 / clear MVC error; status unchanged)
- Server-side validation with meaningful API and MVC error feedback
- SQLite persistence with seed data; data survives restart
- Dual presentation: MVC UI and REST API over shared Application services

### Stretch

- ASP.NET Core Identity login/logout protecting MVC ticket pages
- Light/dark theme toggle persisted in `localStorage`

---

## Technical Changes

| Area | Change |
|------|--------|
| Architecture | Lightweight Clean Architecture: Domain → Application → Infrastructure → Web |
| Domain | Entities (`Ticket`, `Comment`, domain `User`), enums, `TicketStatusStateMachine` |
| Application | Ticket/Comment/User services, DTOs, `ServiceResult<T>`, validation and transition enforcement |
| Infrastructure | EF Core `AppDbContext`, repositories, migrations, seeding; Identity `ApplicationUser` (Stretch) |
| Web | MVC controllers/views, API controllers, exception middleware, DI composition root |
| Tests | xUnit + `WebApplicationFactory` integration suite (state machine, validation, search/filter) |
| Tooling | `.cursor/rules/project-rules.md`, `global.json` (.NET 8 pin), standard .NET `.gitignore` |

Key engineering decisions: MVC chosen over Razor Pages for clear controller/view structure alongside Web API; state machine rules live in Domain and are enforced in Application (thin controllers); Identity login users (`AspNetUsers`) kept separate from seeded ticket assignees (`TicketUsers`).

---

## Database Changes

- **Provider:** SQLite via EF Core (`Data Source=database/support-tickets.db`)
- **Core schema:** `Users` (ticket assignees), `Tickets`, `Comments` — InitialCreate migration
- **Indexes / FKs:** unique email; status index; Restrict on ticket→user; Cascade comment→ticket
- **Stretch:** Identity tables (`AspNetUsers`, roles, etc.) via `AddIdentity` migration on the same database
- **Seed:** 5 assignee users, 3 sample tickets, 2 comments; Stretch admin `admin@example.com` (local demo only)
- **Docs:** `database/setup-notes.md`; mirrored SQL under `database/schema-or-migrations/`
- Local `*.db` / `*.db-wal` / `*.db-shm` are gitignored

---

## Testing Done

| Check | Result |
|-------|--------|
| `dotnet build` | Pass (0 warnings, 0 errors) |
| `dotnet test` | **22/22** passed (duration ~0.5s) |
| State machine integration | 5 valid + 5 invalid transition cases |
| Validation integration | Missing/empty fields, not-found, invalid enum |
| Search/filter integration | Keyword, status, combined, empty, invalid status query |
| Manual MVC | Create/list/detail/update/comments/search/filter/status |
| Manual Stretch | Unauthenticated `/Tickets` → login redirect; theme contrast in dark mode |
| Runtime smoke check | App listens on `http://localhost:5030`; `GET /api/tickets` → 200; `GET /Tickets` → 302 (login redirect) |

Details: [`test-results.md`](test-results.md), [`tool-specific/cursor-workflow/test-results.md`](tool-specific/cursor-workflow/test-results.md), [`debugging-notes.md`](debugging-notes.md).

---

## AI Usage Summary

Cursor (Plan Mode for design; Agent Mode for scoped implementation) assisted across requirements, architecture, API/UI contracts, phased coding, tests, debugging, and review. Human ownership covered stack choice, phase boundaries, acceptance of/rejection of AI suggestions, and verification. Full lifecycle summary: [`final-ai-usage-summary.md`](final-ai-usage-summary.md). Prompt exports live under `ai-prompts/`.

Notable accepted AI help: scaffolding, planning docs, test isolation fix, nullable priority validation, Identity/`TicketUsers` separation, dark-theme CSS. Notable rejections: silent priority default, out-of-scope review ideas (audit trail, FluentResults, interceptor-based rules), premature Stretch/Swagger expansion.

---

## Branch & PR Delivery

Work was delivered through feature branches merged via pull requests into `dev`, then `dev` into `main`, keeping commit history organized and traceable.

| PR | Branch | Into | Purpose | Status |
|----|--------|------|---------|--------|
| #1 | `feature/project-setup` | `dev` | Solution scaffold, layering, tooling | Merged |
| #2 | `feature/core-implementation` | `dev` | Core: API, MVC, persistence, tests | Merged |
| #4 | `feature/stretch-features` | `dev` | Stretch: Identity auth, light/dark theme | Merged |
| #6 | `feature/bug-fix` | `dev` | Remove duplicate API controller (route ambiguity) | Merged |
| #3, #5, #7 | `dev` | `main` | Promotion of accumulated work | Merged |
| #8 | `feature/missing-docs` | `dev` | Align repo to Participant Guide required root structure (docs only) | Open |

PR #8 addresses the earlier structure-gate gap by adding the mandated root ownership/lifecycle documents and supporting `ai-prompts/`, `database/seed-data/`, and `tool-specific/cursor-workflow/` artifacts, with no application code changes.

---

## Screenshots/Demo Notes

Screenshots are not committed in this documentation-only ownership pass. For a live demo:

1. `dotnet run --project src/SupportTicketManagement.Web`
2. Open `http://localhost:5030` (see `launchSettings.json`)
3. Sign in with the seeded Stretch admin (see README Authentication section — local assessment only)
4. Exercise `/Tickets` list, create, details (status + comments), edit, search/filter
5. Toggle light/dark theme; confirm contrast on the ticket table
6. Optional: call REST endpoints under `/api/tickets` (API remains open by design in Stretch)

---

## Known Limitations

- REST API is not authenticated (MVC pages are)
- No Swagger/OpenAPI UI
- No user-management UI (assignees are seeded; Stretch admin is seeded only)
- No pagination; search is case-insensitive `Contains` suitable for SQLite/Core scale
- No Domain unit tests for the state machine (coverage is integration-level)
- Theme toggle is client-persisted only (`localStorage`)

---

## Future Improvements

- Swagger/OpenAPI; API authentication aligned with Identity
- Isolated unit tests for `TicketStatusStateMachine`
- CI pipeline; pagination and additional Stretch items from `project-context.md`
- Defensive `TryGetValue` in the state machine for future enum additions
