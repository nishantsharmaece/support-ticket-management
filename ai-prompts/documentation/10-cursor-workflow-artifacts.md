# Prompt 10 — Cursor tool-specific completeness
_Exported on 7/26/2026 from Cursor agent transcript [6fdb9fb3-9ed9-4e59-8228-fd5dd6f3c789](6fdb9fb3-9ed9-4e59-8228-fd5dd6f3c789)_

---

**User**

Context: Participant Guide — Tool-Specific Expectations for Cursor require `tool-specific/cursor-workflow/` to contain:
- project-context.md  (exists)
- spec.md             (missing)
- tasks.md            (exists)
- cursor-rules-or-instructions.md  (missing; rules currently in `.cursor/rules/project-rules.md`)

Do not change application code.

Task:
1. Create `tool-specific/cursor-workflow/spec.md` summarizing functional/technical spec, pointing to root (or docs) api-contract, data-model, ui-flow, acceptance-criteria as the detailed sources.
2. Create `tool-specific/cursor-workflow/cursor-rules-or-instructions.md` that documents the Cursor rules in use — adapt from `.cursor/rules/project-rules.md` (can summarize + link; content must exist at the required path).
3. Do not delete existing `project-context.md` or `tasks.md`; lightly update them only if needed to reference the new files / root lifecycle artifacts.
4. Remind me to export this chat to `ai-prompts/documentation/10-cursor-workflow-artifacts.md`.

---

**Cursor**

I'll create the missing Cursor workflow docs and lightly sync the existing files. First I'm reading the current workflow folder, project rules, and root lifecycle artifacts.

---

**Cursor**

Checking the detailed sources so `spec.md` accurately summarizes and links them.

---

**Cursor**

Creating the two missing artifacts and lightly updating the existing workflow docs to reference them.

---

**Cursor**

Cursor workflow folder now has the required artifacts. No application code was changed.

### Created
- `tool-specific/cursor-workflow/spec.md` — functional/technical summary with links to root (and `docs/`) `api-contract`, `data-model`, `ui-flow`, and `acceptance-criteria`
- `tool-specific/cursor-workflow/cursor-rules-or-instructions.md` — documents rules in use, adapted from `.cursor/rules/project-rules.md`, with a link to the live always-applied file

### Lightly updated
- `project-context.md` — documentation strategy and AI workflow now reference `spec.md` and `cursor-rules-or-instructions.md`
- `tasks.md` — `Traces to` now includes the new files and root acceptance criteria

### Reminder
Export this chat to:

`ai-prompts/documentation/10-cursor-workflow-artifacts.md`

---
