# Prompt History — Planning

Activity index for planning sessions (repository structure, project context, requirements, acceptance criteria, and related Plan Mode work).

**Honesty note:** Original planning history is fully exported under [`planning/`](planning/). A later **structure-gate remediation** session mirrored requirements/acceptance criteria to repository root without changing application code; that remediation chat should be exported to [`planning/02-root-requirements-and-acceptance.md`](planning/02-root-requirements-and-acceptance.md) if not already present.

---

## Session 1 — Initial repository structure

| Field | Summary |
|-------|---------|
| **When** | 2026-07-13 |
| **Export** | [`planning/project-structure-chat-01.md`](planning/project-structure-chat-01.md) |
| **Prompt summary** | Create only the assessment folder layout (`docs/`, `tool-specific/cursor-workflow/`, `ai-prompts/`, `database/`, `.cursor/rules/`), placeholder workflow files, and a minimal `README.md`. Do not populate planning docs or write application code. |
| **AI response summary** | Created folders with `.gitkeep` / TODO placeholders and a short README mapping folders to roles. Follow-up prompt added `.cursor/rules/project-rules.md`. |
| **Accepted** | Exact folder layout and empty placeholders — matched guide structure without premature content. |
| **Changed** | None material; placeholders only. |
| **Rejected** | N/A — scope was intentionally empty. |

---

## Session 2 — Project context through test strategy (Plan Mode chain)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-13 |
| **Export** | [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) |
| **Prompt summary** | Document-first sequence: create one approved artifact at a time (`project-context.md`, `docs/requirements-analysis.md`, `docs/acceptance-criteria.md`, then architecture / data model / API / UI / implementation plan / tasks / test strategy). Plan Mode → human approve → implement plan only; no application code. |
| **AI response summary** | Drafted each Core-scoped doc with FR/NFR/BR IDs, state-machine rules, Clean Architecture boundaries, and Stretch explicitly deferred. |
| **Accepted** | Option 1 Core scope; MVC + Web API; lightweight Clean Architecture; state machine as non-negotiable; Stretch blocked until Core AC pass. |
| **Changed** | Human edited drafts for assessment alignment (kept Core vs Stretch clear; avoided undocumented Stretch claims). |
| **Rejected** | Implementation details in planning docs; premature Stretch/Swagger/auth design as Core deliverables. |

### Artifacts produced in this chain (under `docs/` / `tool-specific/`)

- `tool-specific/cursor-workflow/project-context.md`
- `docs/requirements-analysis.md`, `docs/acceptance-criteria.md`
- `docs/architecture.md`, `docs/data-model.md`, `docs/api-contract.md`, `docs/ui-flow.md`
- `docs/implementation-plan.md`, `tool-specific/cursor-workflow/tasks.md`, `docs/test-strategy.md`

*(Design-oriented docs in this chain are also indexed under [`design.md`](design.md). Test strategy is also indexed under [`testing.md`](testing.md).)*

---

## Session 3 — Remediation: root requirements & acceptance criteria

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 (structure-gate remediation) |
| **Export** | [`planning/02-root-requirements-and-acceptance.md`](planning/02-root-requirements-and-acceptance.md) *(export this remediation chat if missing)* |
| **Prompt summary** | Official guide requires root `requirements-analysis.md` and `acceptance-criteria.md`. Adapt from `docs/` using Part C templates. Do not change application code. |
| **AI response summary** | Created root mirrors of requirements and acceptance criteria with Part C headings; left `docs/` sources in place. |
| **Accepted** | Exact root filenames for the assessor structure gate. |
| **Changed** | Presentation/headings for Part C; content adapted from existing `docs/`. |
| **Rejected** | Deleting or relocating `docs/` copies; rewriting Core scope. |

---

## Accepted vs rejected (planning overall)

| Decision | Outcome | Why |
|----------|---------|-----|
| Document-first before coding | Accepted | Matches assessment lifecycle and keeps AI scoped |
| Phase-bounded Plan → Agent workflow | Accepted | Reviewable diffs; stop conditions |
| Large “plan everything then code the app in one shot” | Rejected | Unreviewable; used one-doc / one-phase prompts instead |
| Treating Stretch as Core in planning | Rejected | Assessment Core scope first |

---

## Detailed exports

| Path | Role |
|------|------|
| [`planning/project-structure-chat-01.md`](planning/project-structure-chat-01.md) | Repo bootstrap + project rules |
| [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) | Full planning document chain |
| [`planning/02-root-requirements-and-acceptance.md`](planning/02-root-requirements-and-acceptance.md) | Remediation export (root AC/requirements) |
