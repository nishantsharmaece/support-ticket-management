# Support ticket management structure
_Exported on 7/13/2026 at 07:20:35 GMT+5:30 from Cursor (3.10.20)_

---

**User**

Create only the initial repository structure inside folder @support-ticket-management 

docs/
tool-specific/
tool-specific/cursor-workflow/
ai-prompts/
database/
cursor/rules/

Also create placeholder files (empty or with TODO sections only) for:

tool-specific/cursor-workflow/
- project-context.md
- spec.md
- tasks.md
- cursor-rules-or-instructions.md

Create a minimal README.md describing the project purpose and technology stack.

Do NOT populate these documents yet.
Do NOT create planning documents yet.
Do NOT create the .NET solution or application code.


---

**Cursor**

I'll check the workspace, then create the requested folder structure and placeholder files under `support-ticket-management/`.


Creating the folder structure and minimal placeholder files. The existing README is a stub; I'll replace it with a minimal purpose and stack description.


Initial repository structure is in place under `support-ticket-management/`.

## Structure

```
support-ticket-management/
├── README.md
├── docs/
│   └── .gitkeep
├── tool-specific/
│   └── cursor-workflow/
│       ├── project-context.md
│       ├── spec.md
│       ├── tasks.md
│       └── cursor-rules-or-instructions.md
├── ai-prompts/
│   └── .gitkeep
├── database/
│   └── .gitkeep
└── cursor/
    └── rules/
        └── .gitkeep
```

## What was created

- **Placeholder workflow files** — Each has only a heading and a `<!-- TODO: ... -->` comment; no real content yet.
- **`README.md`** — Short description of the project purpose, planned .NET + database stack, and a table mapping each folder to its role.
- **Empty directories** — `.gitkeep` files in `docs/`, `ai-prompts/`, `database/`, and `cursor/rules/` so Git tracks those folders.

No planning documents, .NET solution, or application code were added. Say when you want to start filling in the workflow files or scaffolding the solution.

---

**User**

Create the project rules at:

.cursor/rules/project-rules.md

These rules will provide persistent guidance for all future planning and implementation.

The project will use:

- ASP.NET Core MVC (.NET 8)
- ASP.NET Core Web API
- EF Core
- SQLite
- Bootstrap 5
- xUnit
- Lightweight Clean Architecture

Include concise rules for:

- Technology stack
- Project architecture
- Coding and naming conventions
- Folder organization
- REST API conventions
- MVC best practices
- EF Core best practices
- Input validation
- Error handling
- Logging
- Testing expectations
- Documentation updates
- Security best practices (no secrets in source control)
- AI collaboration guidelines

The AI collaboration guidelines should instruct that:
- Planning should be completed before implementation.
- Approved project context and planning documents should always be reused.
- Only the requested feature should be implemented.
- Related documentation should be updated whenever implementation changes the design or behavior.
- Keep the implementation simple and focused on the assessment scope.

Keep the rules concise, practical, and suitable for a small assessment project.

---

**Cursor**

Checking the repo layout and any existing Cursor rules before adding `project-rules.md`.


Creating `.cursor/rules/project-rules.md` with concise, practical guidance for the assessment project.


Updating the README to point to `.cursor/rules/`, the canonical Cursor rules location.


Created `.cursor/rules/project-rules.md` with `alwaysApply: true` so it applies to every session.

## File created

**`.cursor/rules/project-rules.md`** — concise rules covering:

| Section | Highlights |
|---------|------------|
| **Technology stack** | .NET 8, MVC, Web API, EF Core, SQLite, Bootstrap 5, xUnit |
| **Architecture** | Lightweight Clean Architecture (Domain → Application → Infrastructure/Web) |
| **Conventions** | Standard C# naming, async I/O, thin controllers |
| **Folder organization** | Proposed `src/` + `tests/` layout aligned with layers |
| **REST API** | Resource naming, status codes, DTOs, validation at boundary |
| **MVC** | View models, Tag Helpers, Bootstrap 5, anti-forgery |
| **EF Core** | Single `DbContext`, migrations, no lazy loading, `AsNoTracking` for reads |
| **Validation / errors / logging** | Boundary validation, domain exceptions, `ILogger<T>`, no secrets in logs |
| **Testing** | xUnit naming, cover happy path and failures |
| **Documentation** | Sync workflow docs when behavior changes |
| **Security** | No secrets in source control, user secrets for local config |
| **AI collaboration** | Plan first, reuse approved docs, implement only requested scope, keep it simple |

Also updated `README.md` to reference `.cursor/rules/` instead of `cursor/rules/`.

**Note:** Cursor’s rule skill often uses `.mdc` files; you asked for `.md`, which works with the YAML frontmatter (`alwaysApply: true`). If rules don’t load automatically, rename to `project-rules.mdc`.

