# Candidate Information

Part C — Submission and Reflection (.NET AI Capability Assessment).

| Field | Value |
|-------|-------|
| **Name** | Nishant Kumar Sharma |
| **Role** | Associate Technical Lead |
| **Primary Technology Stack** | .NET 8, ASP.NET Core MVC, ASP.NET Core Web API, Entity Framework Core, SQLite, Bootstrap 5, xUnit |
| **Primary AI Tool Used** | Cursor |
| **Project Option Selected** | Option 1 — Support Ticket Management |
| **Assessment Start Date** | 12th July 2026 |
| **Submission Date** | 15th July 2026 |

---

## Project Summary

Option 1 — Backend-Heavy Support Ticket Management — built for the .NET AI Capability Assessment. The solution is a .NET 8 app with ASP.NET Core MVC (UI), ASP.NET Core Web API (REST), EF Core + SQLite (persistence), and Bootstrap 5. Core capabilities include ticket create/list/detail/update, comments, keyword search and status filter, server-side validation, and an enforced ticket status state machine (invalid transitions rejected by the backend). After Core acceptance criteria were met, Stretch work added ASP.NET Core Identity cookie authentication for MVC pages and a light/dark theme toggle. Integration tests cover the mandatory state-machine tier (see `tool-specific/cursor-workflow/test-results.md`). Lifecycle artifacts (requirements, design, tests, prompt history, reflection) are part of the submission, not an afterthought.

## Tools Used

| Tool / artifact | Purpose |
|-----------------|---------|
| Cursor (Plan + Agent modes) | Requirement analysis, planning/design, scoped code generation, debugging, review support |
| `.cursor/rules/project-rules.md` | Persistent coding, architecture, and AI collaboration guardrails |
| `tool-specific/cursor-workflow/project-context.md` | Primary project context for AI sessions |
| .NET 8 SDK / `dotnet` CLI | Restore, build, run, test |
| xUnit | Integration tests (state machine, validation, search/filter) |
| EF Core + SQLite | Persistence, migrations, seed data |
| Git / GitHub | Source control and PR-based delivery |
| `ai-prompts/` | Exported prompt history by activity |

## Setup Summary

1. Prerequisites: [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. From the repository root: `dotnet restore`, then `dotnet build`.
3. Run: `dotnet run --project src/SupportTicketManagement.Web` (default HTTP profile typically `http://localhost:5030`).
4. Open `/Tickets` in a browser (unauthenticated users are redirected to `/Account/Login` when Stretch auth is enabled).
5. Database: SQLite file under `database/` via `Data Source=database/support-tickets.db`; migrations and seed run on startup. See `database/setup-notes.md` and `README.md`.
6. Tests: `dotnet test`.

No production secrets are required for local setup. Stretch demo login details (if used) are documented in `README.md` only for local assessment verification — do not treat them as real credentials.
