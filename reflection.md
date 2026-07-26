# Reflection

Part C — Submission and Reflection (.NET AI Capability Assessment).  
Adapted from [`docs/reflection.md`](docs/reflection.md). `docs/reflection.md` remains the detailed narrative source; this root file uses the Part C ownership headings required by the Participant Guide.

**Structure-gate note:** An earlier submission scored 0/100 because the assessor’s structure gate expects these ownership files at **repository root** (`reflection.md`, `pr-description.md`, `final-ai-usage-summary.md`). Content already existed under `docs/` and elsewhere; placing the mandated filenames at root remediates that gate failure without changing application code.

---

## What I Built

I selected and built **Option 1 — Backend-Heavy Support Ticket Management** for the .NET AI Capability Assessment.

The solution is a .NET 8 application with ASP.NET Core MVC (server-rendered UI), ASP.NET Core Web API (REST), EF Core + SQLite (persistence), and Bootstrap 5. Core features include ticket CRUD, comments, keyword search, status filtering, server-side validation, and an enforced ticket status state machine — the signature engineering-judgment piece of the assessment.

After Core was complete and verified (`dotnet build`, 22 integration tests), I added two Stretch features: ASP.NET Core Identity authentication (login/logout protecting MVC pages) and a light/dark theme toggle persisted in `localStorage`.

Delivery followed a nine-phase plan across layered commits (solution setup → domain and database → application services → API → MVC → integration tests → documentation). Three pull requests merged feature work into `dev` and then `main`. Stretch continued on `feature/stretch-features`.

---

## How I Used AI

Primary tool: **Cursor**, with Plan Mode and Agent Mode deliberately separated.

- **Plan Mode** for significant design work (requirements, architecture, API contract, implementation plan). Cursor produced a plan; I reviewed it; then I prompted implementation with explicit scope boundaries (“Implement only Phase X… stop after Phase X is complete”).
- **Agent Mode** for scoped coding, referencing approved planning documents and `.cursor/rules/project-rules.md`.
- Each session loaded `tool-specific/cursor-workflow/project-context.md` as the source of truth.
- Prompt history was exported under `ai-prompts/` (planning, implementation, and related activity folders) as lifecycle artifacts.
- I reviewed AI-generated code before accepting changes — adjusting validation behaviour, fixing naming clashes, and removing dead code rather than accepting output blindly.
- After every major phase I ran `dotnet build`; after Core features I ran `dotnet test` and performed manual browser verification of MVC flows.

```mermaid
flowchart LR
  planDocs[PlanDocs] --> corePhases[CorePhases]
  corePhases --> verifyBuild[TestAndBuild]
  verifyBuild --> stretch[StretchAuthTheme]
  stretch --> reflection[Reflection]
```

---

## What AI Helped With Most

- Scaffolding the Clean Architecture solution and feature-folder layout.
- Drafting planning documents (requirements, acceptance criteria, architecture, API contract, UI flow, implementation plan, test strategy) that later guided every coding session.
- Generating boilerplate for EF Core configurations, DTOs, controllers, and integration-test harnesses.
- Proposing fixes under constraints: serialized SQLite test fixture + `SqliteConnection.ClearAllPools()`, nullable `Priority` for missing-field 400s, Identity vs `TicketUsers` rename, dark-theme CSS overrides for Bootstrap `.table-light`.
- Supporting code review passes that cross-checked behaviour against acceptance criteria without rewriting the stack.

---

## What AI Got Wrong

| Issue | What AI produced / assumed | Correction |
|-------|----------------------------|------------|
| Missing `priority` on create | Non-nullable enum silently defaulted to `Low` | Made `CreateTicketRequest.Priority` nullable; reject null with 400 |
| Parallel integration tests on Windows | Shared/racy SQLite file under parallel xUnit collections | Serialized collection fixture + clear connection pools on cleanup |
| Identity scaffolding | Domain `Users` DbSet clashed with Identity `Users` | Renamed domain set to `TicketUsers`; keep assignees separate from `AspNetUsers` |
| Dark theme | Incomplete CSS overrides; Bootstrap `.table-light` forced black text | Manual UI review + targeted `[data-theme="dark"]` overrides |
| Scope creep risk | Tendency to implement large batches or Stretch items early | Explicit phase stop boundaries; deferred Swagger, API auth, CI |
| Review suggestions | Audit trail, FluentResults, SaveChanges interceptor, optimistic concurrency | Rejected as out of Core scope or layering violations — see `code-review-notes.md` |

---

## How I Validated AI Output

- **Build:** `dotnet build` after every major phase.
- **Automated tests:** `dotnet test` — 22/22 integration tests (state machine, validation, search/filter). Recorded in `test-results.md` / `tool-specific/cursor-workflow/test-results.md`.
- **Manual MVC checks:** create, list, detail, update, comments, search/filter, status transitions, auth redirects, theme contrast.
- **Contract alignment:** compared API/MVC behaviour to `docs/api-contract.md`, `docs/ui-flow.md`, and acceptance criteria AC-01–AC-11.
- **Code review:** AI-assisted review documented in `code-review-notes.md`; accepted vs rejected suggestions recorded; deferred items in `review-fixes.md`.
- **Debug evidence:** real defects and fixes captured in `debugging-notes.md` with prompt-export references.
- **Structure gate remediation:** verified mandated Part C filenames exist at **repository root** (this file and companions), while leaving detailed sources under `docs/` in place.

---

## What I Would Improve Next

- Add Swagger/OpenAPI documentation (planned Stretch, not implemented).
- Add unit tests for `TicketStatusStateMachine` in isolation, complementing the integration suite.
- Apply authentication to the REST API, not only MVC pages.
- Add a CI pipeline, pagination, and other Stretch items from `project-context.md`.
- Merge `feature/stretch-features` through a pull request with a formal review, matching the Core workflow.
- Harden structure-gate readiness earlier: keep root Part C artifacts in sync with `docs/` from the first submission, not as a post-score remediation.

---

## Reusable Workflow

The pattern I would reuse on a real team project:

1. **Document first** — architecture, API contract, UI flow, and acceptance criteria before large code batches.
2. **Plan vs Agent** — design in Plan Mode; implement in Agent Mode with phase-scoped prompts and hard stop boundaries.
3. **Load context** — project rules + `project-context.md` + the specific approved doc for the phase.
4. **Verify continuously** — build after each phase; run integration tests when behaviour is testable; manually check UI for what automation misses.
5. **Own the judgment** — accept scaffolding and boilerplate; reject silent defaults, layering violations, and out-of-scope suggestions.
6. **Export prompts** — save chat history under `ai-prompts/` by activity so the lifecycle is auditable.
7. **Place submission artifacts where the guide says** — root ownership files (`reflection.md`, `pr-description.md`, `final-ai-usage-summary.md`) plus detailed planning under `docs/`, so structure gates and reviewers both succeed.

AI accelerated planning and implementation; judgment — what to build, what to defer, what to reject, and what to verify — remained mine.
