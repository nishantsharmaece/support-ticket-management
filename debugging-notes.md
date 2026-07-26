# Debugging Notes

Part C — Submission artifact for the .NET AI Capability Assessment.

**Traces to:** `ai-prompts/implementation/project-implementation-chat-01.md`, `ai-prompts/implementation/authentication_and_dark_light_theme_implement.md`, `docs/reflection.md`, `tool-specific/cursor-workflow/test-results.md`, `tests/SupportTicketManagement.Tests/Integration/`

These notes record real defects found during Core and Stretch work. They are not invented scenarios — each issue has evidence in prompt exports, reflection, test fixtures, or code review outcomes.

---

## Issue 1 — SQLite / Windows integration test isolation

### Problem

Integration tests failed intermittently on Windows when multiple test classes ran in parallel. Classes shared (or raced on) the same SQLite file because the WebApplicationFactory connection-string override was unreliable. Concurrent migrations and host shutdown left `.db` files locked, causing migration races and `IOException` on cleanup.

### How I Investigated

- Ran `dotnet test` after implementing mandatory integration coverage; failures clustered around fixture setup/teardown rather than assertion logic.
- Compared factory configuration against `docs/test-strategy.md` (isolated SQLite per run / cleanup between tests).
- Observed that parallel xUnit collections competed for the database file on Windows even when a temp path was intended.

### How AI Helped

Cursor (Agent Mode) proposed a serialized collection fixture, a stronger connection-string override in `CustomWebApplicationFactory`, and `SqliteConnection.ClearAllPools()` before deleting the temp database — constrained to the test project only.

### What I Validated

- `dotnet test SupportTicketManagement.sln` → **22/22 passed** (~5.3s), recorded in `tool-specific/cursor-workflow/test-results.md`.
- Confirmed `IntegrationTestCollection` uses `DisableParallelization = true` and a shared `IntegrationTestFixture`.
- Confirmed dispose path clears SQLite pools and tolerates brief Windows file locks.

### Final Fix

- Unique temp DB path per fixture (`stm-tests-{Guid}.db`).
- Dual override: `UseSetting` + in-memory configuration for `ConnectionStrings:DefaultConnection`.
- `[CollectionDefinition("Integration", DisableParallelization = true)]`.
- `SqliteConnection.ClearAllPools()` in `DisposeAsync`, with a catch for transient `IOException` on file delete.

**Evidence:** `ai-prompts/implementation/project-implementation-chat-01.md`, `docs/reflection.md` §5, `IntegrationTestFixture.cs`, `CustomWebApplicationFactory.cs`.

---

## Issue 2 — Missing `priority` on create defaulted to Low instead of 400

### Problem

Backend validation for ticket create treated an omitted `priority` as `Priority.Low` (enum default `0`) instead of rejecting the request with HTTP 400. That contradicted `docs/test-strategy.md` and the planned validation integration tests.

### How I Investigated

- Integration test “Create ticket — missing priority (400)” failed while empty-title and other validation cases behaved correctly.
- Traced binding from API DTO `CreateTicketRequest` into `TicketService` create path.
- Confirmed non-nullable `Priority` caused model binding / defaulting rather than a validation failure.

### How AI Helped

Cursor located the DTO/service contract and applied the minimal change: make `Priority` nullable and reject null in validation — matching the test strategy without rewriting the create flow.

### What I Validated

- Validation suite passes, including missing-priority → **400**.
- Full suite remains **22/22** green after the change.
- Behaviour noted in `tool-specific/cursor-workflow/test-results.md` and `docs/reflection.md` §5.

### Final Fix

- `CreateTicketRequest.Priority` changed to `Priority?`.
- Omitted or null priority returns **400** with field errors instead of silently inserting `Low`.

**Evidence:** `ai-prompts/implementation/project-implementation-chat-01.md` (Fixes applied), `tool-workflow.md` (code review example), `CreateTicketRequest.cs`.

---

## Issue 3 — Identity `Users` DbSet naming clash with ticket assignees

### Problem

Adding ASP.NET Core Identity (Stretch) as `IdentityDbContext<ApplicationUser>` introduced Identity’s built-in `Users` set. The existing domain assignee entity was also exposed as `Users`, causing a naming clash / build conflict and conflating login accounts (`AspNetUsers`) with seeded ticket assignees.

### How I Investigated

- Build/errors surfaced while creating the `AddIdentity` migration and wiring Identity into Infrastructure.
- Reviewed `AppDbContext` against the data model: ticket assignees are domain `User` rows used for assignment, not Identity principals.
- Cross-checked seed and repository usage that queried the assignee set.

### How AI Helped

Cursor identified the clash during Identity scaffolding and renamed the domain DbSet / call sites to `TicketUsers`, keeping Identity tables separate and moving Identity registration into the Web project as required by the Stretch prompt.

### What I Validated

- `dotnet build` succeeds after the rename and Identity migration.
- Login/logout HTTP checks: unauthenticated `/Tickets` → 302 to `/Account/Login`; seeded admin can sign in; logout restores the redirect.
- Integration tests continue to hit the API without MVC auth (API left unauthorized by design).

### Final Fix

- Domain DbSet renamed to `TicketUsers` (`AppDbContext`, repositories, `DatabaseSeeder`).
- Identity owns `AspNetUsers` / roles; ticket assignees remain seeded domain users.
- Migration `AddIdentity` adds standard Identity schema on the same SQLite database.

**Evidence:** `ai-prompts/implementation/authentication_and_dark_light_theme_implement.md`, `docs/reflection.md` §4–§5, `AppDbContext.cs`.

---

## Issue 4 — Dark theme contrast on ticket list (Bootstrap `.table-light`)

### Problem

After the light/dark theme Stretch feature shipped, dark mode left dark text on dark backgrounds on the ticket list. Headings, labels, table header/body, and some alerts were hard to read. Bootstrap 5.1’s `.table-light { color: #000 }` overrode theme CSS variables.

### How I Investigated

- Manual UI review of `/Tickets` in dark mode (reflection notes that automated tests would not catch this).
- Inspected `Index.cshtml` (`thead` uses `table-light`) and Bootstrap’s compiled CSS vs `site.css` theme rules.
- Confirmed missing dark overrides for headings, form labels, alerts, and badges.

### How AI Helped

Cursor traced the conflict to Bootstrap’s hardcoded `.table-light` color and proposed comprehensive `[data-theme="dark"]` overrides in `site.css` (and link color via CSS variables in `_Layout.cshtml.css`) without changing Bootstrap vendor files.

### What I Validated

- Browser hard-refresh of ticket list in dark mode: title, filters, table header, assignee names, and timestamps use light text.
- Theme toggle / switch still persists via `localStorage`.
- Light theme appearance unchanged.

### Final Fix

- Dark-theme overrides for `.table-light` (background, text, borders, hover), headings, labels, table cells, alerts, badges, and outline buttons.
- Layout link color switched to `var(--stm-link-color)`.

**Evidence:** `ai-prompts/implementation/authentication_and_dark_light_theme_implement.md` (user-reported contrast review), `docs/reflection.md` §4, `wwwroot/css/site.css`.

---

## Issue 5 — Submission hygiene: dead code and duplicated status parsing

### Problem

During final Core submission review against the assessment guide and acceptance criteria, unused exception type, orphaned MVC `Error` action/view, and duplicated API status-query parsing remained in the tree. These were not runtime “bugs” but defects against cleanliness and consistency requirements (no dead code / no duplicated logic).

### How I Investigated

- Systematic review of the solution vs assessment guide, acceptance criteria, and planning docs.
- Searched for unused types and duplicate enum/status parsing across API controllers.
- Verified build, tests, secrets policy, and documentation alignment in the same pass.

### How AI Helped

Cursor assisted with codebase search and minimal cleanup: remove unused types/views, consolidate optional status-filter parsing into a shared helper, and sync docs (README, acceptance criteria, setup notes) without changing Core behaviour.

### What I Validated

- `dotnet build` — 0 errors, 0 warnings.
- `dotnet test` — 22/22 passed.
- No `.db` / `.env` / secrets tracked; `.env` added to `.gitignore`.
- Assessment criteria #10 (no secrets) and #11 (mandatory tests) still satisfied.

### Final Fix

- Removed unused `InvalidTicketStatusTransitionException`.
- Removed orphaned `Error` action and `Error.cshtml` (middleware owns error responses).
- Consolidated API status parsing via `TicketEnumParser.TryParseOptionalStatusFilter`.
- Documentation updates for launch profiles, test coverage, and EF CLI startup project.

**Evidence:** `ai-prompts/implementation/project-implementation-chat-01.md` (Submission Review Complete), `docs/reflection.md` §2 / §6.

---

## Summary

| # | Issue | Layer | Outcome |
|---|--------|--------|---------|
| 1 | SQLite/Windows test isolation | Tests | Serialized fixture + pool clear; 22/22 green |
| 2 | Missing priority → 400 | Application / API | `Priority?` + validation |
| 3 | Identity vs ticket-user naming | Infrastructure / Identity | `TicketUsers` + Identity tables |
| 4 | Dark theme `.table-light` contrast | Web CSS | Theme overrides for list/table |
| 5 | Dead / duplicated code at review | Web / API | Cleanup + shared parser |

No application code was changed while authoring this artifact; fixes above were already applied during implementation and Stretch sessions.
