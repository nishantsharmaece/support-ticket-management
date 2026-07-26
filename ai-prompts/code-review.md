# Prompt History — Code Review

Activity index for AI-assisted Core review, rejected suggestions, and Done/Deferred fix tracking.

**Honesty note:** Informal review happened during implementation (e.g. priority validation, submission hygiene). The formal Part C artifacts [`../code-review-notes.md`](../code-review-notes.md) and [`../review-fixes.md`](../review-fixes.md) were produced in a **structure-gate remediation** session that was **documentation-only** (no application code changes). Export that chat to [`code-review/06-code-review-and-fixes.md`](code-review/06-code-review-and-fixes.md) if not already present.

Do **not** use a root file named `code-review.md` for Part C notes — the guide wants root `code-review-notes.md` / `review-fixes.md`. This flat file is the **prompt-history** index under `ai-prompts/` only.

---

## Session 1 — In-development review decisions (original)

| Field | Summary |
|-------|---------|
| **When** | During Core/Stretch implementation |
| **Export** | [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md), [`implementation/authentication_and_dark_light_theme_implement.md`](implementation/authentication_and_dark_light_theme_implement.md) |
| **Prompt summary** | Scoped implement prompts with stop conditions; submission review against AC; Stretch Identity constraints. |
| **AI response summary** | Produced working code; human accepted scaffolding and corrected validation/naming/CSS issues rather than rubber-stamping. |
| **Accepted** | State machine centralisation; `ServiceResult` + HTTP mapping; thin controllers; mandatory tests. |
| **Changed** | Priority nullability; Identity/`TicketUsers`; dark-theme CSS; dead-code cleanup. |
| **Rejected** | Out-of-scope Stretch during Core; large unscoped batches. |

These in-dev decisions appear as **Done** items in [`../review-fixes.md`](../review-fixes.md).

---

## Session 2 — Remediation: formal AI-assisted Core review (docs only)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 (structure-gate remediation) |
| **Export** | [`code-review/06-code-review-and-fixes.md`](code-review/06-code-review-and-fixes.md) *(export this remediation chat if missing)* |
| **Prompt summary** | Briefly review Core (Domain state machine, Application services, API/MVC, tests). Create root `code-review-notes.md` (Part C sections) and `review-fixes.md` (Done vs Deferred). Ask before changing application code; exact filenames (not root `code-review.md`). |
| **AI response summary** | Confirmed layering and state-machine/tests alignment; recorded minor observations (TryGetValue hardening, validation order, enum binding split, no Domain unit tests); authored rejected-suggestions table; deferred follow-ups without code edits. |
| **Accepted** | Documentation of strengths, observations, and rejections; Done vs Deferred split. |
| **Changed** | Documentation only — no Domain/Application/Web/Infrastructure/test code. |
| **Rejected (suggestions not adopted)** | See table below. |

### Suggestions rejected during formal review

| Suggestion | Why rejected |
|------------|--------------|
| Domain event / status audit trail | Out of Core scope |
| Replace `ServiceResult<T>` with FluentResults / exceptions | Unnecessary dependency; mapping already clear |
| EF `SaveChanges` interceptor for transitions | Would push rules into Infrastructure |
| Optimistic concurrency (rowversion) | No Core concurrent-edit requirement |
| Map tracked write entity without re-fetch | Would drop navigation graph for detail DTO |
| Client-side JS as authoritative transition enforcement | Server-side is authoritative (NFR) |

---

## Accepted vs rejected (code review overall)

| Item | Decision | Why |
|------|----------|-----|
| Record review without forcing code churn | Accepted | Minimal change; ask before app edits |
| Deferred hardening (TryGetValue, unit tests, check order) | Deferred | Safe today / out of Core |
| Premature Stretch/Swagger from “review” | Rejected | Lifecycle artifacts + Core AC first |

---

## Detailed exports

| Path | Role |
|------|------|
| [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) | In-dev review / hygiene |
| [`code-review/06-code-review-and-fixes.md`](code-review/06-code-review-and-fixes.md) | Remediation export (notes + fixes docs) |
| Root artifacts | [`../code-review-notes.md`](../code-review-notes.md), [`../review-fixes.md`](../review-fixes.md) |
