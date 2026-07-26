# Prompt History — Debugging

Activity index for real defect investigation and fixes during Core/Stretch work, plus the Part C debugging-notes artifact.

**Honesty note:** Debugging happened **inside implementation/Stretch chats** (not a separate early `ai-prompts/debugging/` export). The dedicated root [`../debugging-notes.md`](../debugging-notes.md) was authored in a **structure-gate remediation** session that documented evidence-backed issues only — no invented bugs, no application code changes in that session. Export that chat to [`debugging/05-debugging-notes.md`](debugging/05-debugging-notes.md) if not already present.

---

## Session 1 — Core test failures (SQLite isolation + missing priority)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-15 (during mandatory tests) |
| **Export** | [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) |
| **Prompt summary** | Implement mandatory integration tests; run all tests; fix failures. |
| **AI response summary** | Diagnosed parallel xUnit collections racing on one SQLite file on Windows; strengthened factory override, serialized collection, `SqliteConnection.ClearAllPools()`. Separately fixed omitted `priority` binding to enum default `Low` by making `Priority` nullable and validating null → 400. |
| **Accepted** | Serialized fixture + pool clear; nullable priority validation. |
| **Changed** | Test project fixture/factory; `CreateTicketRequest.Priority` type + service validation. |
| **Rejected** | Ignoring intermittent Windows failures; keeping silent Low default. |

Documented as Issues 1–2 in [`../debugging-notes.md`](../debugging-notes.md).

---

## Session 2 — Stretch Identity naming clash + dark theme contrast

| Field | Summary |
|-------|---------|
| **When** | 2026-07-15 |
| **Export** | [`implementation/authentication_and_dark_light_theme_implement.md`](implementation/authentication_and_dark_light_theme_implement.md) |
| **Prompt summary** | Implement minimal Identity; later user-reported dark-mode contrast problems on ticket list. |
| **AI response summary** | Renamed domain DbSet to `TicketUsers` to avoid Identity `Users` clash; added `[data-theme="dark"]` overrides so Bootstrap `.table-light` no longer forces black text on dark backgrounds. |
| **Accepted** | Separate Identity principals vs ticket assignees; CSS overrides without editing Bootstrap vendor files. |
| **Changed** | `AppDbContext` / repositories / seeder naming; `site.css` / layout link color. |
| **Rejected** | Merging assignees into AspNetUsers; disabling theme feature instead of fixing contrast. |

Documented as Issues 3–4 in [`../debugging-notes.md`](../debugging-notes.md).

---

## Session 3 — Submission hygiene (dead / duplicated code)

| Field | Summary |
|-------|---------|
| **When** | End of Core submission review (same implementation export) |
| **Export** | [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) |
| **Prompt summary** | Review solution vs guide/AC; clean dead code and duplicated status parsing without changing Core behaviour. |
| **AI response summary** | Removed unused exception type and orphaned Error view/action; consolidated optional status-filter parsing; synced docs. |
| **Accepted** | Minimal hygiene cleanup; build + 22/22 still green. |
| **Changed** | Web/API helpers and unused types/views. |
| **Rejected** | Behavioural refactors unrelated to cleanliness. |

Documented as Issue 5 in [`../debugging-notes.md`](../debugging-notes.md).

---

## Session 4 — Remediation: author `debugging-notes.md` (documentation only)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 (structure-gate remediation) |
| **Export** | [`debugging/05-debugging-notes.md`](debugging/05-debugging-notes.md) *(export this remediation chat if missing)* |
| **Prompt summary** | Create root file named exactly `debugging-notes.md` (not `debugging.md`) using Part C template per issue. Use evidence from the repo; do not invent bugs; do not change application code. |
| **AI response summary** | Wrote Problem / Investigation / AI help / Validation / Final fix for five evidenced issues; linked prompt exports. |
| **Accepted** | Exact filename; evidence-only issues. |
| **Changed** | Documentation only. |
| **Rejected** | Fabricated scenarios; renaming to `debugging.md`. |

---

## Accepted vs rejected (debugging overall)

| Fix | Decision | Why |
|-----|----------|-----|
| Serialized SQLite tests + pool clear | Accepted | Real Windows flake; suite green |
| Nullable priority → 400 | Accepted | Matches test strategy |
| `TicketUsers` rename | Accepted | Identity clash |
| Dark-theme CSS overrides | Accepted | Real contrast bug |
| Invented “demo” bugs for the notes file | Rejected | Guide: evidence only |

---

## Detailed exports

| Path | Role |
|------|------|
| [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) | Core debug fixes during tests/submission |
| [`implementation/authentication_and_dark_light_theme_implement.md`](implementation/authentication_and_dark_light_theme_implement.md) | Identity + theme debug |
| [`debugging/05-debugging-notes.md`](debugging/05-debugging-notes.md) | Remediation export (root debugging-notes) |
