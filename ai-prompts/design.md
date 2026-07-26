# Prompt History — Design

Activity index for architecture, data model, API/UI contracts, implementation plan, and consolidated design notes.

**Honesty note:** Original **design prompts lived inside the planning chat export** ([`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md)) — there was no separate `ai-prompts/design/` folder during Core build. A later **structure-gate remediation** session created root design artifacts (`design-notes.md`, `implementation-plan.md`, `api-contract.md`, `data-model.md`, `ui-flow.md`) from `docs/`. That remediation should be exported to [`design/03-root-design-and-plan.md`](design/03-root-design-and-plan.md) if not already present.

---

## Session 1 — Architecture, data model, API, UI, implementation plan (original)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-13 |
| **Export** | [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) (design segments within the same chat) |
| **Prompt summary** | Create only `docs/architecture.md`, then `docs/data-model.md`, `docs/api-contract.md`, `docs/ui-flow.md`, `docs/implementation-plan.md` — each as a separate Plan Mode → approve → implement cycle. No schema SQL, controllers, or application code. |
| **AI response summary** | Produced Clean Architecture layering, entity/field model, REST contract (status codes including 409 for invalid transitions), MVC screen flows, and phased implementation plan. |
| **Accepted** | Four-project layering + Web host; Domain state machine as single source of truth; DTOs at Application boundary; server-side transition enforcement; Core-only phases. |
| **Changed** | Human kept business rules in Domain/Application (not Infrastructure); aligned statuses/priorities with assessment wording. |
| **Rejected** | Pushing transition rules into EF/`SaveChanges`; treating Swagger/auth as Core design deliverables; over-detailed class-level design in planning docs. |

### Design docs created (canonical detailed sources under `docs/`)

| Doc | Purpose |
|-----|---------|
| `docs/architecture.md` | Layers, dependencies, folder structure |
| `docs/data-model.md` | Entities, fields, relationships |
| `docs/api-contract.md` | REST endpoints, payloads, status codes |
| `docs/ui-flow.md` | MVC screens and flows |
| `docs/implementation-plan.md` | Phased delivery plan |

---

## Session 2 — Remediation: root design & plan artifacts

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 (structure-gate remediation) |
| **Export** | [`design/03-root-design-and-plan.md`](design/03-root-design-and-plan.md) *(export this remediation chat if missing)* |
| **Prompt summary** | Guide requires root `implementation-plan.md`, `design-notes.md`, `api-contract.md`, `data-model.md`, `ui-flow.md`. Adapt from `docs/` (architecture → design-notes). Do not change application code. |
| **AI response summary** | Created/updated root Part C design artifacts; left detailed narrative under `docs/`. |
| **Accepted** | Exact root filenames for structure gate; consolidation of architecture into `design-notes.md`. |
| **Changed** | Headings/structure for Part C; cross-links to `docs/` sources. |
| **Rejected** | Redesigning the running system; deleting `docs/architecture.md`. |

---

## Accepted vs rejected (design overall)

| Decision | Outcome | Why |
|----------|---------|-----|
| Lightweight Clean Architecture | Accepted | Matches assessment + reviewable boundaries |
| State machine in Domain | Accepted | Single source of truth for BR-01–BR-03 |
| Rules in Infrastructure interceptor | Rejected | Layering violation |
| Domain events / status audit trail | Rejected | Out of Core scope |
| Optimistic concurrency (rowversion) | Rejected | No Core concurrent-edit requirement |

Further rejection detail: [`../code-review-notes.md`](../code-review-notes.md), [`../final-ai-usage-summary.md`](../final-ai-usage-summary.md).

---

## Detailed exports

| Path | Role |
|------|------|
| [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) | Original design document prompts |
| [`design/03-root-design-and-plan.md`](design/03-root-design-and-plan.md) | Remediation export (root design/plan files) |
