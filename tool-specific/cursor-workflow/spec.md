# Functional and Technical Spec

**Scope:** Core only  
**Primary AI tool:** Cursor  
**Traces to:** [project-context.md](project-context.md), [tasks.md](tasks.md)

This document is the Cursor-workflow **summary** of the functional and technical specification. Detailed contracts, models, UI behavior, and pass/fail criteria live in the lifecycle artifacts listed below — use those as the authoritative sources when implementing or reviewing.

---

## Detailed Sources of Truth

| Concern | Authoritative document | Also available |
|---------|------------------------|----------------|
| REST API endpoints, DTOs, status codes, validation | [`api-contract.md`](../../api-contract.md) | [`docs/api-contract.md`](../../docs/api-contract.md) |
| Entities, attributes, relationships, indexes, seed | [`data-model.md`](../../data-model.md) | [`docs/data-model.md`](../../docs/data-model.md) |
| Screens, navigation, form behavior, error UX | [`ui-flow.md`](../../ui-flow.md) | [`docs/ui-flow.md`](../../docs/ui-flow.md) |
| Observable pass/fail outcomes | [`acceptance-criteria.md`](../../acceptance-criteria.md) | [`docs/acceptance-criteria.md`](../../docs/acceptance-criteria.md) |

Related context and execution:

- What-and-why / assessment scope: [project-context.md](project-context.md)
- Implementation checklist: [tasks.md](tasks.md)
- Coding and AI guardrails: [cursor-rules-or-instructions.md](cursor-rules-or-instructions.md) (mirrors `.cursor/rules/project-rules.md`)

---

## Functional Summary

Internal users manage support tickets through an ASP.NET Core MVC UI backed by a REST API and SQLite persistence.

| Capability | Behavior |
|------------|----------|
| Create ticket | Title, description, priority, assignee, creator; status defaults to `Open` |
| List tickets | Show key fields; empty-state when none exist |
| View detail | Full ticket fields, timestamps, comments |
| Update ticket | Title, description, priority, assignee; `updatedAt` refreshed |
| Change status | Only valid state-machine transitions; invalid transitions rejected by backend |
| Add comments | Append-only messages with creator and timestamp |
| Search / filter | Keyword on title/description; filter by status; combinable |
| Persistence | Data survives restart; users seeded (no user-management UI in Core) |
| Validation | Server-side authoritative; reject invalid input before persistence |
| Errors | Clear UI feedback; no stack traces or secrets exposed |

### Status state machine (non-negotiable)

| From | To | Valid |
|------|-----|-------|
| Open | In Progress | Yes |
| Open | Cancelled | Yes |
| In Progress | Resolved | Yes |
| In Progress | Cancelled | Yes |
| Resolved | Closed | Yes |
| All other transitions | — | **No** — reject (typically HTTP 409 on API) |

---

## Technical Summary

| Layer | Choice |
|-------|--------|
| Runtime | .NET 8 |
| UI | ASP.NET Core MVC, Bootstrap 5 |
| API | ASP.NET Core Web API (`/api/tickets`, `/api/tickets/{id}`, `/api/tickets/{id}/status`, `/api/tickets/{id}/comments`) |
| Data access | Entity Framework Core |
| Database | SQLite |
| Architecture | Lightweight Clean Architecture: Domain ← Application ← Infrastructure / Web |
| Testing | xUnit; mandatory tier = state-machine integration tests |

**Domain entities:** User (seeded), Ticket, Comment — see data-model for attributes and relationships.

**API surface (Core):**

| Method | Route | Purpose |
|--------|-------|---------|
| POST | `/api/tickets` | Create |
| GET | `/api/tickets` | List / search / filter |
| GET | `/api/tickets/{id}` | Detail |
| PUT | `/api/tickets/{id}` | Update fields |
| PATCH | `/api/tickets/{id}/status` | Status transition |
| POST | `/api/tickets/{id}/comments` | Add comment |

**MVC screens:** ticket list (home), create, detail (status + comments), edit — see ui-flow for navigation and validation UX.

---

## Out of Scope (Core)

Authentication, user CRUD, pagination/sorting beyond Core search-filter, Swagger, Docker/CI, and other Stretch items — deferred until all Core acceptance criteria pass. See [project-context.md](project-context.md).

---

## How to Use This Spec

1. Read [project-context.md](project-context.md) for assessment boundaries and assumptions.
2. Use this file for a quick functional/technical overview.
3. Open the linked root (or `docs/`) artifacts for endpoint shapes, field rules, UI flows, and acceptance checks.
4. Execute work against [tasks.md](tasks.md); keep Cursor rules in [cursor-rules-or-instructions.md](cursor-rules-or-instructions.md) / `.cursor/rules/project-rules.md`.
