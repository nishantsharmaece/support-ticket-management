---
description: Persistent project rules for planning, implementation, and AI collaboration
alwaysApply: true
---

# Support Ticket Management — Project Rules

## Technology Stack

- **.NET 8** — target framework for all projects
- **ASP.NET Core MVC** — server-rendered UI
- **ASP.NET Core Web API** — REST endpoints
- **Entity Framework Core** — data access with SQLite provider
- **SQLite** — development and assessment database
- **Bootstrap 5** — UI styling and layout
- **xUnit** — unit and integration tests

Do not introduce additional frameworks or dependencies unless required and justified.

## Project Architecture

Use **lightweight Clean Architecture** with clear layer boundaries:

| Layer | Responsibility |
|-------|----------------|
| **Domain** | Entities, enums, domain exceptions; no framework dependencies |
| **Application** | Use cases, DTOs, service interfaces, validation rules |
| **Infrastructure** | EF Core `DbContext`, repositories, migrations, external integrations |
| **Web** | MVC controllers/views, API controllers, middleware, DI registration |

**Dependency rule:** Domain ← Application ← Infrastructure / Web. Inner layers must not reference outer layers.

Keep abstractions minimal. Prefer interfaces only where they aid testing or swap implementations (e.g., repositories, key services).

## Coding and Naming Conventions

- Follow standard C# conventions: `PascalCase` for types and public members, `camelCase` for parameters and locals, `_camelCase` for private fields.
- Use meaningful, domain-focused names (`Ticket`, `TicketStatus`, `CreateTicketRequest`).
- One public type per file; file name matches type name.
- Prefer `async`/`await` for I/O-bound operations.
- Use `CancellationToken` on async public APIs where appropriate.
- Avoid `var` when the type is not obvious from the right-hand side.

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

- Group by feature within layers when it improves clarity (e.g., `Tickets/`).
- Keep controllers thin; business logic belongs in Application services.
- Co-locate view models with MVC features; keep API request/response DTOs in Application.

## REST API Conventions

- Use attribute routing: `[Route("api/[controller]")]`.
- Use HTTP verbs correctly: `GET` read, `POST` create, `PUT`/`PATCH` update, `DELETE` remove.
- Return appropriate status codes: `200`, `201` (+ `Location`), `204`, `400`, `404`, `409`, `500`.
- Use plural resource names (`/api/tickets`, `/api/tickets/{id}`).
- Validate input at the boundary; return `400 Bad Request` with clear error details.
- Do not expose entity types directly from API endpoints; use DTOs.
- Version only if required; keep the API simple for assessment scope.

## MVC Best Practices

- Keep controllers thin: delegate to Application services.
- Use strongly typed view models; never pass entities directly to views.
- Use Tag Helpers and partial views for reusable UI.
- Apply Bootstrap 5 consistently; avoid inline styles unless necessary.
- Use anti-forgery tokens on state-changing forms (`[ValidateAntiForgeryToken]`).
- Return proper HTTP results (`View`, `RedirectToAction`, `NotFound`).

## EF Core Best Practices

- Define a single `DbContext` in Infrastructure.
- Use Fluent API or data annotations for configuration; keep configuration in Infrastructure.
- Prefer explicit entity configurations for non-trivial mappings.
- Use migrations for schema changes; commit migration files with related code changes.
- Avoid lazy loading; use explicit `Include` or projection where needed.
- Keep queries in repositories or query services; do not query the database from controllers.
- Use `AsNoTracking()` for read-only queries when entities are not updated.

## Input Validation

- Validate all external input (API requests, form posts, query parameters).
- Use Data Annotations on view models and DTOs for simple rules.
- Enforce business rules in Application services and return clear validation errors.
- Reject invalid input early; never trust client-side validation alone.
- Sanitize or encode user-generated content displayed in views to prevent XSS.

## Error Handling

- Use domain-specific exceptions for expected business failures (e.g., `TicketNotFoundException`).
- Map exceptions to HTTP status codes in API middleware or filters.
- Show user-friendly messages in MVC; log detailed errors server-side.
- Do not expose stack traces, connection strings, or internal details to clients.
- Use a global exception handler for unhandled errors in Web.

## Logging

- Use `ILogger<T>` via constructor injection.
- Log at appropriate levels: `Information` for normal flow, `Warning` for recoverable issues, `Error` for failures.
- Include correlation context where useful (ticket ID, user action).
- Do not log secrets, passwords, or full PII.

## Testing Expectations

- Write xUnit tests for Application services and domain logic.
- Use in-memory SQLite or a test `DbContext` for integration tests when needed.
- Name tests clearly: `MethodName_Scenario_ExpectedResult`.
- Cover happy paths and key failure cases for each feature.
- Add or update tests whenever behavior changes.

## Documentation Updates

- Update `tool-specific/cursor-workflow/` documents when design or behavior changes.
- Keep `README.md` accurate for setup, run, and test instructions.
- Document non-obvious decisions briefly in `docs/` when they affect future work.
- Do not create planning documents unless explicitly requested.

## Security Best Practices

- **Never commit secrets** — no API keys, passwords, or connection strings with credentials in source control.
- Use `appsettings.Development.json` (gitignored) or user secrets for local configuration.
- Validate and authorize all state-changing operations.
- Use HTTPS in production; enable HSTS where appropriate.
- Apply principle of least privilege for database and service access.

## AI Collaboration Guidelines

1. **Plan before implementing** — complete planning and get approval before writing application code.
2. **Reuse approved documents** — always reference `tool-specific/cursor-workflow/project-context.md`, `spec.md`, and `tasks.md` as the source of truth.
3. **Implement only what is requested** — do not add unrequested features, refactors, or scope creep.
4. **Keep documentation in sync** — update related docs whenever implementation changes design or behavior.
5. **Stay simple and focused** — prefer the smallest correct solution that meets the assessment scope; avoid over-engineering.

When in doubt, ask for clarification rather than assuming requirements.
