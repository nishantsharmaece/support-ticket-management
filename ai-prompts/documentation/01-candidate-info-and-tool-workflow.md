# Prompt 1 — Candidate info + tool workflow
_Exported on 7/26/2026 from Cursor agent transcript [d002cb31-16a0-4788-8d0f-50f3a27f1dbf](d002cb31-16a0-4788-8d0f-50f3a27f1dbf)_

---

**User**

Context: Align this Support Ticket Management repo to the official .NET AI Capability Exercise Required Repository Structure (Participant Guide). Do not change application code.

Authority: Root files must be named exactly as in the guide.

Task:
1. Create root `candidate-info.md` using the Part C template sections:
   - Name, Role, Primary Technology Stack
   - Primary AI Tool Used, Project Option Selected (Option 1 — Support Ticket Management)
   - Assessment Start Date, Submission Date
   - ## Project Summary / ## Tools Used / ## Setup Summary
   Ask me for Name/Role/dates if unknown; do not invent personal details.
2. Create root `tool-workflow.md` covering Part A expected points:
   - Primary AI tool used (Cursor)
   - How project context is provided to the tool
   - How AI is used for requirement analysis, planning/design, code generation, validation, testing, debugging, and code review
   - What information is avoided sharing with AI
   - How this workflow would be reused on a real project
   Reference `tool-specific/cursor-workflow/project-context.md` and `.cursor/rules/project-rules.md`.
3. Keep filenames exactly: `candidate-info.md`, `tool-workflow.md` (repo root).
4. List created files and remind me to export this chat under `ai-prompts/documentation/`.

---

**Cursor**

I'll align the repo docs to the Participant Guide structure. First I'll inspect the referenced workflow/rules files and any existing templates so we can create the exact root filenames without inventing personal details.

---

**Cursor**

Creating both root files now: `tool-workflow.md` fully from project context, and `candidate-info.md` with placeholders for Name/Role/dates so we don't invent personal details.

---

**Cursor**

Created the two required root files (no application code changed):

| File | Status |
|------|--------|
| `candidate-info.md` | Created — tech stack, Cursor, Option 1, and Part C summaries filled; personal fields left as placeholders |
| `tool-workflow.md` | Created — Part A workflow covering context loading, lifecycle AI use, data not shared, and real-project reuse (references `tool-specific/cursor-workflow/project-context.md` and `.cursor/rules/project-rules.md`) |

Please send these so I can replace the placeholders in `candidate-info.md`:

1. **Name**
2. **Role**
3. **Assessment Start Date**
4. **Submission Date**

**Reminder:** Export this chat and save it under `ai-prompts/documentation/` (assessment prompt-history evidence).

---

**User**

Name- Nishant Kumar Sharma 
Role  - Associate Technical Lead
Assessment start Date - 12th July
Submission Date - 15th July

---

**Cursor**

Updating `candidate-info.md` with your details.

---

**Cursor**

Updated `candidate-info.md` with:

- **Name:** Nishant Kumar Sharma  
- **Role:** Associate Technical Lead  
- **Assessment Start Date:** 12th July  
- **Submission Date:** 15th July

---

**User**

verify the changes you just made one more time.

---

**Cursor**

I'll re-read both files to verify.

---

**Cursor**

Verified both files. Everything is correct.

**`candidate-info.md`** — all Part C fields present and accurate:
- Name: Nishant Kumar Sharma
- Role: Associate Technical Lead
- Primary Technology Stack, Primary AI Tool (Cursor), Project Option (Option 1)
- Assessment Start Date: 12th July / Submission Date: 15th July
- `## Project Summary`, `## Tools Used`, `## Setup Summary` sections all populated — no leftover placeholders.

**`tool-workflow.md`** — all Part A points covered: primary tool (Cursor), how context is provided, AI use across requirement analysis / planning / code generation / validation / testing / debugging / code review, information not shared with AI, and real-project reuse. It references both `tool-specific/cursor-workflow/project-context.md` and `.cursor/rules/project-rules.md`.

Both filenames are exactly correct at the repo root: `candidate-info.md`, `tool-workflow.md`.

One optional note: the dates read "12th July" and "15th July" without a year. If you'd like them as full dates (e.g., "12 July 2025"), let me know and I'll adjust.

**Reminder:** export this chat under `ai-prompts/documentation/` as prompt-history evidence.

---

**User**

yes, update year to 2026

---

**Cursor**

Updated both dates in `candidate-info.md` to **12th July 2026** and **15th July 2026**.

---
