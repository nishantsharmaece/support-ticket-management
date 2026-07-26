# Tool Workflow

Part A — AI Workflow Foundation for the .NET AI Capability Assessment.

**Primary AI tool:** Cursor  
**Project:** Option 1 — Support Ticket Management  
**Context sources:** [`tool-specific/cursor-workflow/project-context.md`](tool-specific/cursor-workflow/project-context.md), [`.cursor/rules/project-rules.md`](.cursor/rules/project-rules.md)

---

## Primary AI Tool Used

Cursor is the primary AI tool for this assessment. Work is split by mode:

| Mode | Typical use |
|------|-------------|
| **Plan Mode** | Requirement analysis, architecture, API/UI contracts, implementation plans, and task breakdowns before coding |
| **Agent Mode** | Scoped implementation, tests, documentation updates, and targeted fixes against approved artifacts |
| **Ask / explore** | Codebase questions, locating conventions, and clarifying existing behaviour without changing files |

Persistent guidance lives in `.cursor/rules/project-rules.md` (always applied). Session grounding starts from `tool-specific/cursor-workflow/project-context.md`.

---

## How Project Context Is Provided to the Tool

Context is supplied deliberately so prompts stay aligned with assessment scope:

1. **Persistent rules** — `.cursor/rules/project-rules.md` encodes stack, Clean Architecture boundaries, naming, API/MVC/EF conventions, validation, security, testing, and AI collaboration guardrails.
2. **Primary project brief** — `tool-specific/cursor-workflow/project-context.md` is the source of truth for business context, Core vs Stretch scope, state machine rules, architecture, tech stack, workflow, and success criteria.
3. **Approved planning artifacts** — prompts reference `docs/` (requirements, acceptance criteria, architecture, data model, API contract, UI flow, implementation plan, test strategy) and `tool-specific/cursor-workflow/tasks.md` for phase boundaries.
4. **Scoped prompts** — each session states the phase or feature, cites those documents, and constrains output (e.g., implement only Phase X; do not start Stretch until Core acceptance criteria pass).
5. **Prompt history** — important chats are exported under `ai-prompts/` (planning, implementation, etc.) so prior decisions remain visible and reusable.

This reduces re-explaining the project each time and keeps AI output reviewable against written requirements.

---

## How AI Is Used Across the Lifecycle

### Requirement analysis

- Load assessment Core scope from `project-context.md`.
- Use Plan Mode to draft functional/non-functional requirements, acceptance criteria, and explicit out-of-scope items.
- Review and edit AI drafts so the enforced status state machine and mandatory integration-test tier stay non-negotiable.

### Planning and design

- Produce architecture (lightweight Clean Architecture), data model, API contract, UI flows, implementation plan, and task checklist before application code.
- Prefer document-first sessions: approve plans, then implement against them.
- Keep Stretch features deferred until Core acceptance criteria are met.

### Code generation

- Implement in phases (solution setup → domain → persistence → Application services → API → MVC → state machine → search/filter → tests → docs).
- Reference `project-rules.md` and approved contracts so controllers stay thin, Domain stays framework-free, and DTOs (not entities) cross API/UI boundaries.
- Reject scope creep: no unrequested features, broad refactors, or extra dependencies without justification.

### Validation

- After each significant phase: `dotnet build`, then manual checks of MVC flows where relevant.
- Confirm backend validation (required fields, invalid input → clear errors) and that invalid status transitions are rejected server-side (HTTP 409 / UI error), matching acceptance criteria.

### Testing

- Generate and refine xUnit integration tests for the mandatory state-machine tier (valid transitions succeed; invalid transitions rejected).
- Record results in `tool-specific/cursor-workflow/test-results.md`.
- Update or add tests when behaviour changes; keep test naming consistent with project rules (`MethodName_Scenario_ExpectedResult`).

### Debugging

- Reproduce failures with build/test output and failing assertions.
- Ask Cursor for root-cause hypotheses constrained to the relevant layer (Domain rules, Application services, Infrastructure, Web).
- Prefer minimal fixes; verify with `dotnet test` and targeted manual UI checks. Document non-obvious fixes in lifecycle notes when they affect future work.

### Code review

- Treat AI output as a draft: review naming, layer boundaries, validation behaviour, error handling, and security (no secrets, no stack traces to clients).
- Manually adjust accepted code where judgement is required (e.g., nullable priority so omitted values return 400; Identity vs ticket-user naming).
- Keep documentation (`README.md`, `docs/`, cursor-workflow artifacts) in sync when design or behaviour changes.

---

## Information Avoided Sharing with AI

Do not share with AI tools:

- Secrets, API keys, passwords, tokens, certificates, or private keys
- Production connection strings or credentials
- Unnecessary personal data or PII beyond what assessment artifacts require
- Proprietary third-party material not needed for this exercise
- Instructions that would bypass security, validation, or assessment integrity rules

Local secrets stay in user secrets / gitignored config. Repository guidance (including README seed credentials for Stretch demo accounts) is treated carefully and never expanded into real credentials.

---

## Reusing This Workflow on a Real Project

The same pattern scales beyond the assessment:

1. **Write durable context first** — project brief + always-on rules (stack, architecture, coding standards, security).
2. **Plan before build** — requirements, contracts, and task breakdown approved before large Agent sessions.
3. **Scope every prompt** — cite the source-of-truth docs; limit the change set; avoid “do everything” prompts.
4. **Validate continuously** — build, tests, and manual checks after each slice; own every accepted change.
5. **Capture prompt history** — export important sessions for auditability, onboarding, and later reuse.
6. **Protect sensitive data** — keep secrets out of prompts and source control; share only the minimum context needed.

On a team project, store shared context in the repo (rules, ADRs, API contracts), use branch-scoped Agent work, and require human review of AI-generated PRs the same way as any other contribution.
