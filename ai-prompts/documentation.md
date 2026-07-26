# Prompt History — Documentation

Activity index for README/docs lifecycle work, Part C ownership artifacts, and this `ai-prompts/` seven-file index.

**Honesty note:** Early documentation was produced during **planning and implementation** chats. After a **0/100 structure-gate** score (missing exact root ownership filenames), a series of **remediation-only** sessions (2026-07-26) created/updated root Part C files without changing application code. Much of the “documentation activity” evidence for assessor layout is therefore **remediation-phase**. Do **not** use `docs.md` as the prompt-history filename — this file is exactly `documentation.md`.

---

## Session 1 — Planning docs & README (original)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-13 → Core build |
| **Export** | [`planning/project-structure-chat-01.md`](planning/project-structure-chat-01.md), [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md), [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) |
| **Prompt summary** | Document-first Core artifacts under `docs/` and cursor-workflow; keep README setup/run accurate as phases complete. |
| **AI response summary** | Authored planning suite and updated README/setup notes during phases; Stretch README auth section in Identity chat. |
| **Accepted** | Document-first workflow; docs as source of truth for later Agent prompts. |
| **Changed** | README and setup notes as features shipped. |
| **Rejected** | Documenting unimplemented Stretch (e.g. Swagger) as done. |

---

## Session 2 — Structure-gate remediation stream (2026-07-26)

Remediation chats aligned the repo to the Participant Guide’s **exact root filenames**. Each was documentation-only unless noted.

| # | Focus | Expected detailed export | Root artifacts produced |
|---|--------|--------------------------|-------------------------|
| 1 | Candidate info + tool workflow alignment | [`documentation/01-candidate-info-and-tool-workflow.md`](documentation/01-candidate-info-and-tool-workflow.md) | `candidate-info.md`, `tool-workflow.md`, related root alignment |
| 2 | Requirements + acceptance criteria | [`planning/02-root-requirements-and-acceptance.md`](planning/02-root-requirements-and-acceptance.md) | `requirements-analysis.md`, `acceptance-criteria.md` |
| 3 | Design + plan root files | [`design/03-root-design-and-plan.md`](design/03-root-design-and-plan.md) | `design-notes.md`, `implementation-plan.md`, `api-contract.md`, `data-model.md`, `ui-flow.md` |
| 4 | Test strategy + results | [`testing/04-root-test-strategy-and-results.md`](testing/04-root-test-strategy-and-results.md) | `test-strategy.md`, `test-results.md` |
| 5 | Debugging notes | [`debugging/05-debugging-notes.md`](debugging/05-debugging-notes.md) | `debugging-notes.md` |
| 6 | Code review notes + fixes list | [`code-review/06-code-review-and-fixes.md`](code-review/06-code-review-and-fixes.md) | `code-review-notes.md`, `review-fixes.md` |
| 7 | Ownership / reflection | [`documentation/07-ownership-artifacts.md`](documentation/07-ownership-artifacts.md) | `reflection.md`, `pr-description.md`, `final-ai-usage-summary.md` |

| Field | Summary (typical remediation prompt) |
|-------|--------------------------------------|
| **Prompt summary** | Create exact guide filenames at repo root from existing `docs/` / workflow sources; do not change application code; leave detailed sources in place. |
| **AI response summary** | Mirrored/adapted content with Part C headings; cross-linked to `docs/`; reminded user to export each chat under the matching `ai-prompts/<activity>/` path. |
| **Accepted** | Exact root names for structure gate; dual layout (`docs/` detailed + root ownership). |
| **Changed** | Presentation and filenames only. |
| **Rejected** | Deleting `docs/` copies; inventing features; application code edits. |

---

## Session 3 — This chat: seven flat prompt-history files

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 |
| **Export** | **Export this chat to** [`documentation/08-ai-prompts-seven-files.md`](documentation/08-ai-prompts-seven-files.md) |
| **Prompt summary** | Create/update exactly these seven flat files under `ai-prompts/`: `planning.md`, `design.md`, `implementation.md`, `testing.md`, `debugging.md`, `code-review.md`, `documentation.md`. Summarize sessions (prompt / AI / accepted-changed-rejected); link existing exports; be honest about remediation-only history; do not use `docs.md` or `review.md`; do not change application code; do not delete subfolder exports. |
| **AI response summary** | Authored the seven index files with session tables, honesty notes, and links to [`planning/`](planning/), [`implementation/`](implementation/), and expected remediation export paths. |
| **Accepted** | Exact seven filenames; flat files at `ai-prompts/` root; preserve subfolder chat exports. |
| **Changed** | Prompt-history documentation only. |
| **Rejected** | Renaming to `docs.md` / `review.md`; deleting existing exports; touching application code. |

---

## Accepted vs rejected (documentation overall)

| Decision | Outcome | Why |
|----------|---------|-----|
| Keep detailed narrative under `docs/` + root Part C names | Accepted | Structure gate + readable deep docs |
| Claim Swagger/CI as implemented in docs | Rejected | Not built |
| Fabricate prompt history | Rejected | Honesty / assessment integrity |
| Replace subfolder exports with only flat summaries | Rejected | Guide wants history; subfolders retain full chats |

---

## Map of the seven required flat files

| File | Activity |
|------|----------|
| [`planning.md`](planning.md) | Planning |
| [`design.md`](design.md) | Design |
| [`implementation.md`](implementation.md) | Implementation |
| [`testing.md`](testing.md) | Testing |
| [`debugging.md`](debugging.md) | Debugging |
| [`code-review.md`](code-review.md) | Code review |
| [`documentation.md`](documentation.md) | Documentation (this file) |

Lifecycle AI summary: [`../final-ai-usage-summary.md`](../final-ai-usage-summary.md).

---

## Detailed exports

| Path | Role |
|------|------|
| [`planning/`](planning/), [`implementation/`](implementation/) | Original full chat exports (present) |
| [`documentation/07-ownership-artifacts.md`](documentation/07-ownership-artifacts.md) | Remediation: reflection / PR / AI usage summary |
| [`documentation/08-ai-prompts-seven-files.md`](documentation/08-ai-prompts-seven-files.md) | **This session — please export here** |
| Other `ai-prompts/<activity>/0N-*.md` paths | Remediation exports from prior chats (export if missing) |
