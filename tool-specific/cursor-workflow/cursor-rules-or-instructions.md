# Cursor Rules and Instructions

**Scope:** Support Ticket Management — Cursor workflow documentation  
**Canonical rule file (always applied):** [`.cursor/rules/project-rules.md`](../../.cursor/rules/project-rules.md)

This document records the Cursor project rules in use for the assessment. The live, always-applied rule content lives under `.cursor/rules/`. Keep both in sync when conventions change.

---

## How Rules Are Applied

| Mechanism | Location | Behavior |
|-----------|----------|----------|
| Cursor Project Rules | `.cursor/rules/project-rules.md` | `alwaysApply: true` — loaded for planning, implementation, and AI collaboration |
| This artifact | `tool-specific/cursor-workflow/cursor-rules-or-instructions.md` | Assessment-required copy documenting rules in use |
| Workflow context | `project-context.md`, `spec.md`, `tasks.md` | Referenced by the AI collaboration guidelines below |

---

## Technology Stack

- .NET 8 for all projects
- ASP.NET Core MVC (UI) and Web API (REST)
- Entity Framework Core with SQLite
- Bootstrap 5 for UI
- xUnit for tests

Do not introduce additional frameworks or dependencies unless required and justified.

---

## Architecture

Lightweight Clean Architecture with clear boundaries:

| Layer | Responsibility |
|-------|----------------|
| **Domain** | Entities, enums, domain exceptions; no framework dependencies |
| **Application** | Use cases, DTOs, service interfaces, validation |
| **Infrastructure** | EF Core `DbContext`, repositories, migrations |
| **Web** | MVC/API controllers, views, middleware, DI |

**Dependency rule:** Domain ← Application ← Infrastructure / Web. Prefer interfaces only where they aid testing or swappable implementations.

---

## Coding and Naming

- Standard C#: `PascalCase` types/public members; `camelCase` parameters/locals; `_camelCase` private fields
- Domain-focused names (`Ticket`, `TicketStatus`, `CreateTicketRequest`)
- One public type per file; file name matches type name
- Prefer `async`/`await` for I/O; use `CancellationToken` on async public APIs where appropriate
- Avoid `var` when the type is not obvious from the RHS

---

## Folder Organization

```
src/
  SupportTicketManagement.Domain/
  SupportTicketManagement.Application/
  SupportTicketManagement.Infrastructure/
  SupportTicketManagement.Web/
tests/
  SupportTicketManagement.Tests/
docs/
tool-specific/cursor-workflow/
database/
ai-prompts/
```

- Group by feature within layers when helpful (e.g., `Tickets/`)
- Thin controllers; business logic in Application services
- View models with MVC features; API DTOs in Application

---

## REST API Conventions

- Attribute routing: `[Route("api/[controller]")]`
- Correct HTTP verbs; plural resources (`/api/tickets`)
- Appropriate status codes (`200`, `201` + `Location`, `204`, `400`, `404`, `409`, `500`)
- Validate at the boundary; return clear `400` details
- Expose DTOs, not entities
- Keep versioning out of Core unless required

---

## MVC Best Practices

- Thin controllers; strongly typed view models (never entities in views)
- Tag Helpers / partials for reuse; Bootstrap 5 consistently
- Anti-forgery on state-changing forms
- Proper results: `View`, `RedirectToAction`, `NotFound`

---

## EF Core Best Practices

- Single `DbContext` in Infrastructure; Fluent API / annotations for configuration
- Migrations for schema changes; commit with related code
- No lazy loading; explicit `Include` or projection
- Queries in repositories/query services — not controllers
- `AsNoTracking()` for read-only queries when safe

---

## Validation, Errors, and Logging

- Validate all external input; Data Annotations for simple rules; business rules in Application
- Domain exceptions for expected failures; map to HTTP status in middleware/filters
- User-friendly MVC messages; log details server-side; never expose stack traces or secrets
- `ILogger<T>`; appropriate levels; include useful context (e.g., ticket ID); never log secrets or full PII

---

## Testing Expectations

- xUnit for Application and domain logic; in-memory SQLite / test `DbContext` for integration when needed
- Naming: `MethodName_Scenario_ExpectedResult`
- Cover happy paths and key failures; add/update tests when behavior changes
- Mandatory Core tier: state-machine integration tests (valid succeed, invalid rejected)

---

## Documentation and Security

- Update `tool-specific/cursor-workflow/` when design or behavior changes
- Keep `README.md` accurate for setup, run, and test
- Document non-obvious decisions in `docs/` when they affect future work
- **Never commit secrets**; use user secrets or gitignored local config
- Validate/authorize state-changing operations; HTTPS/HSTS in production contexts; least privilege

---

## AI Collaboration Guidelines

1. **Plan before implementing** — planning and approval before application code for significant work
2. **Reuse approved documents** — `project-context.md`, `spec.md`, and `tasks.md` as source of truth
3. **Implement only what is requested** — no unrequested features, refactors, or scope creep
4. **Keep documentation in sync** — update related docs when implementation changes design or behavior
5. **Stay simple and focused** — smallest correct solution for assessment scope; avoid over-engineering

When in doubt, ask for clarification rather than assuming requirements.

---

## Full Rule Text

For the complete always-applied Cursor rule (including frontmatter), see:

**[`.cursor/rules/project-rules.md`](../../.cursor/rules/project-rules.md)**
