# Prompt History — Implementation

Activity index for Core phased implementation and Stretch (Identity + theme).

**Honesty note:** This activity has the richest **original** chat exports under [`implementation/`](implementation/). Remediation-phase work did not rewrite application code for implementation; structure-gate sessions only adjusted documentation layout.

---

## Session 1 — Core phased implementation (Phases 1–N)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-15 (and related Core build days) |
| **Export** | [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) |
| **Prompt summary** | Implement only Phase X from `tasks.md` / approved docs; stop after the phase. Sequence: solution setup → domain/DB → Application/Infrastructure → API → MVC → mandatory integration tests → submission hygiene. Reference architecture and project rules; no Stretch until Core complete. |
| **AI response summary** | Scaffolded .NET 8 solution, entities/EF configs, services with `ServiceResult<T>`, thin API/MVC controllers, state machine enforcement, search/filter, then 22 integration tests. Fixed Windows SQLite test isolation and nullable `Priority` during the test phase. |
| **Accepted** | Layered solution; feature folders; Domain `TicketStatusStateMachine`; Application services + DTOs; API/MVC sharing services; phase stop boundaries. |
| **Changed** | `CreateTicketRequest.Priority` → `Priority?` so omit returns 400; serialized SQLite test fixture + `ClearAllPools()`; dead-code / parser cleanup at submission review. |
| **Rejected** | Unscoped “build the whole app” batches; silent default of omitted priority to `Low`; premature Stretch/Swagger/CI expansion. |

### Phase highlights (from export)

| Phase focus | Outcome |
|-------------|---------|
| Solution setup | Five projects + DI stubs; build green |
| Domain + database | Entities, enums, EF, migration, seed |
| Application + Infrastructure | Services, repositories, result model |
| API | Core REST endpoints + error mapping |
| MVC | Ticket CRUD, comments, search/filter UI |
| Tests | 22/22 mandatory integration suite |
| Submission review | Hygiene cleanup without behaviour change |

---

## Session 2 — Stretch: authentication + light/dark theme

| Field | Summary |
|-------|---------|
| **When** | 2026-07-15 |
| **Export** | [`implementation/authentication_and_dark_light_theme_implement.md`](implementation/authentication_and_dark_light_theme_implement.md) |
| **Prompt summary** | Minimal ASP.NET Core Identity (login/logout only, seed Admin, `[Authorize]` on ticket MVC). Later: light/dark theme toggle. Keep architecture; update README if needed; verify build/login. |
| **AI response summary** | Identity on same SQLite DB; renamed domain `Users` → `TicketUsers`; Account controller/views; theme CSS + `localStorage`. Fixed dark-mode contrast (Bootstrap `.table-light`). |
| **Accepted** | Stretch Identity for MVC only; theme toggle; Identity tables separate from ticket assignees. |
| **Changed** | `TicketUsers` rename; dark-theme CSS overrides for headings/tables/alerts. |
| **Rejected** | Registration / forgot-password / role UI; API auth as part of this Stretch; changing Core API behaviour for tests. |

---

## Accepted vs rejected (implementation overall)

| Output | Decision | Why |
|--------|----------|-----|
| Phased scaffolding matching architecture | Accepted | Reviewable; matched approved plan |
| State machine + Application enforcement | Accepted | Assessment signature requirement |
| Silent priority default | Corrected/rejected | Contradicted test strategy |
| FluentResults / exception-driven services | Rejected | Unnecessary dependency; clear mapping already |
| Client-side JS as authoritative transitions | Rejected | Server-side enforcement is authoritative |

See also [`../final-ai-usage-summary.md`](../final-ai-usage-summary.md), [`debugging.md`](debugging.md).

---

## Detailed exports

| Path | Role |
|------|------|
| [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) | Core phases, tests, submission hygiene |
| [`implementation/authentication_and_dark_light_theme_implement.md`](implementation/authentication_and_dark_light_theme_implement.md) | Stretch Identity + theme |
