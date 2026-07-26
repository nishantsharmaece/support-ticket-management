# Prompt 2 — Requirements + acceptance criteria
_Exported on 7/26/2026 from Cursor agent transcript [6586e0ca-4232-4103-8d61-7875ca13bc93](6586e0ca-4232-4103-8d61-7875ca13bc93)_

---

**User**

Context: Official guide requires root `requirements-analysis.md` and `acceptance-criteria.md`. Content exists under `docs/`. Do not change application code.

Task:
1. Create root `requirements-analysis.md` adapted from `docs/requirements-analysis.md`, structured per Part C template:
   Selected Project Option, My Understanding, Functional Requirements, Non-Functional Requirements, Assumptions, Clarifications, Edge Cases.
2. Create root `acceptance-criteria.md` adapted from `docs/acceptance-criteria.md`, with checklist sections: Core, Validation, Error Handling, Testing, Documentation.
3. Keep exact root filenames. Leave `docs/` copies in place.
4. Remind me to export this chat to `ai-prompts/planning/02-root-requirements-and-acceptance.md`.

---

**Cursor**

I'll read the existing docs and any Part C template guidance, then create the root `requirements-analysis.md` and `acceptance-criteria.md` files.

---

**Cursor**

Checking the planning chat export and project context for the Part C template structure.

---

**Cursor**

Creating both root files adapted to the Part C template structure.

---

**Cursor**

Created both Part C root artifacts. `docs/` copies are unchanged; no application code was touched.

### What changed

| File | Purpose |
|------|---------|
| `requirements-analysis.md` | Part C structure: Selected Project Option, My Understanding, Functional Requirements (incl. business rules), Non-Functional Requirements, Assumptions, Clarifications, Edge Cases |
| `acceptance-criteria.md` | Checklist sections: Core, Validation, Error Handling, Testing, Documentation |

Content is adapted from `docs/requirements-analysis.md` and `docs/acceptance-criteria.md`. Clarifications were added for Core vs Stretch, auth, validation authority, search behaviour, and secrets.

### Reminder

Export this chat to:

`ai-prompts/planning/02-root-requirements-and-acceptance.md`

---

**User**

verify the changes you just made one more time.

---

**Cursor**

I'll verify both root files exist with correct content and that the `docs/` copies remain in place.

---

**Cursor**

Now let me confirm the `docs/` copies are still in place and check git status.

---

**Cursor**

Git confirms both root files are new/untracked. The `ls` portion failed only due to PowerShell syntax. Let me verify the `docs/` copies directly.

The `&&` operator didn't work in PowerShell, so I'm checking git status to confirm the new root files are showing as untracked, then I'll use Glob to verify the docs were copied correctly.

---

**Cursor**

Verification complete. Everything checks out.

### Root files (created, untracked in git)

| File | Status | Structure |
|------|--------|-----------|
| `requirements-analysis.md` | Present (187 lines) | Selected Project Option → My Understanding → Functional Requirements → Non-Functional Requirements → Assumptions → Clarifications → Edge Cases — matches the Part C template exactly |
| `acceptance-criteria.md` | Present (100 lines) | Core → Validation → Error Handling → Testing → Documentation — all five checklist sections present |

### `docs/` copies (untouched)

Both `docs/requirements-analysis.md` and `docs/acceptance-criteria.md` are still in place.

### Content checks
- Root filenames are exact as requested.
- Requirements content adapted from `docs/` (FR-01–FR-18, NFR-01–NFR-09, BR-01–BR-09, state machine, edge cases) plus a new Clarifications section.
- Acceptance criteria checklists trace back to Core features, validation, error handling, the mandatory state-machine test tier, and documentation.
- Git shows only untracked additions (`requirements-analysis.md`, `acceptance-criteria.md`) — no modifications to existing files, no application code changed.

### Reminder

Export this chat to `ai-prompts/planning/02-root-requirements-and-acceptance.md`.

---
