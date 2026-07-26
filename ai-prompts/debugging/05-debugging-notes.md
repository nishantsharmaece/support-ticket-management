# Prompt 5 — Debugging notes
_Exported on 7/26/2026 from Cursor agent transcript [2f1e006e-0312-42a7-ad91-9c61a5dd0c1e](2f1e006e-0312-42a7-ad91-9c61a5dd0c1e)_

---

**User**

Context: Official guide requires root file named exactly `debugging-notes.md` (NOT debugging.md). Do not invent fake bugs; use evidence from the repo. Do not change application code.

Task:
1. Create root `debugging-notes.md` using Part C template for each issue:
   Problem / How I Investigated / How AI Helped / What I Validated / Final Fix.
2. Include 3–6 real issues from `ai-prompts/implementation/`, `docs/reflection.md`, test fixture notes (e.g. SQLite/Windows test isolation), validation/naming fixes, Identity stretch work if present.
3. Filename must be `debugging-notes.md` at repo root.
4. Remind me to export this chat to `ai-prompts/debugging/05-debugging-notes.md`.

---

**Cursor**

I'll gather real debugging evidence from the implementation prompts, reflection docs, and test notes, then create root `debugging-notes.md` from the Part C template.

---

**Cursor**

Creating `debugging-notes.md` from the documented real issues.

---

**Cursor**

Created root **`debugging-notes.md`** with five evidence-based issues in the Part C format (Problem / How I Investigated / How AI Helped / What I Validated / Final Fix):

1. **SQLite/Windows test isolation** — parallel races, collection fixture, `ClearAllPools()`
2. **Missing priority validation** — nullable `Priority?` → 400 instead of defaulting to Low
3. **Identity naming clash** — domain `Users` → `TicketUsers` vs `AspNetUsers`
4. **Dark theme contrast** — Bootstrap `.table-light` forcing black text on dark UI
5. **Submission hygiene** — dead code / duplicated status parsing cleanup

Sources: `ai-prompts/implementation/`, `docs/reflection.md`, test fixtures, and `test-results.md`. No application code was changed.

**Reminder:** Export this chat to `ai-prompts/debugging/05-debugging-notes.md`.

---
