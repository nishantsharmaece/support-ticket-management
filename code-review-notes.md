# Code Review Notes

Part C — Submission artifact for the .NET AI Capability Assessment.
AI-assisted code review of the **Core** implementation (Domain state machine, Application services, API/MVC presentation, and integration tests).

**Traces to:** [`design-notes.md`](design-notes.md), [`acceptance-criteria.md`](acceptance-criteria.md), [`test-results.md`](test-results.md), [`review-fixes.md`](review-fixes.md)

**Scope reviewed:**

| Area | Key files |
|------|-----------|
| Domain state machine | `src/SupportTicketManagement.Domain/Services/TicketStatusStateMachine.cs`, `Enums/TicketStatus.cs`, `Entities/Ticket.cs` |
| Application services | `Application/Tickets/TicketService.cs`, `Comments/CommentService.cs`, `Common/StatusTransitionHelper.cs`, `Common/ServiceResult.cs` |
| Presentation (API + MVC) | `Web/Api/TicketsApiController.cs`, `Web/Api/ApiResults.cs`, `Web/Controllers/TicketsController.cs`, `Web/Middleware/ExceptionHandlingMiddleware.cs` |
| Infrastructure | `Infrastructure/Repositories/TicketRepository.cs` |
| Tests | `tests/SupportTicketManagement.Tests/Integration/*` |

---

## AI-Assisted Review Summary

This review was conducted with Cursor (Agent mode) reading the Core source tree and cross-checking behaviour against the acceptance criteria and design notes. The AI pass focused on: correctness of the ticket status state machine, layering discipline (business rules kept out of controllers/views), consistency of the result/error model, and whether the integration tests actually exercise the mandatory state-machine tier.

**Overall assessment:** the Core implementation is consistent with the documented Clean Architecture layering and the acceptance criteria. Business rules live in Domain and Application; controllers are thin; expected failures are returned as explicit results rather than thrown as exceptions.

**Strengths confirmed by the review:**

- **State machine is the single source of truth.** `TicketStatusStateMachine` holds one immutable transition table (BR-01–BR-03). Both the API and MVC paths route through it; neither controllers nor Razor views encode transition rules. `StatusTransitionHelper` is a thin Application-side adapter that only re-exposes the Domain rules as display strings.
- **Explicit result model over exceptions.** `ServiceResult<T>` distinguishes `Validation` / `NotFound` / `Conflict` / `None`, and `ApiResults.FromResult` maps those to `400 / 404 / 409 / 201-200` cleanly, with an unmapped kind falling back to `500`. This keeps HTTP status decisions out of the services.
- **Layering holds.** DTOs are returned at the Application boundary; entities are never exposed to views. Repositories use `AsNoTracking()` + `Include(...)` for read paths and keep EF concerns in Infrastructure.
- **Internals are not leaked.** `ExceptionHandlingMiddleware` returns a generic JSON error for `/api` and a friendly HTML page otherwise, with no stack traces or connection strings (AC: "No stack traces … exposed to the user").
- **Tests target the mandatory tier.** `StateMachineIntegrationTests` asserts both that valid transitions return `200` and persist, and that invalid transitions return `409` and leave status unchanged — exercised end-to-end through `WebApplicationFactory`.

---

## My Review Observations

Observations are grouped by severity. None are blocking for Core acceptance; the minor items are recorded as follow-ups in [`review-fixes.md`](review-fixes.md).

### Correctness / robustness

1. **`TicketStatusStateMachine` indexes the dictionary directly.** `CanTransition` and `GetAllowedTransitions` use `AllowedTransitions[currentStatus]`, which throws `KeyNotFoundException` for an enum value not present as a key. In practice this is safe today because every `TicketStatus` value is a key and callers guard `request.Status` with `Enum.IsDefined` before transitioning. It is still a latent trap if a new status is added to the enum without adding a table entry. A defensive `TryGetValue` (returning "no transitions allowed") would fail safe. — *Minor, deferred.*

2. **Order of checks in `TicketService.CreateAsync`.** User-existence checks run *before* field validation, so a request with a blank title but valid users returns `400`, while the same blank title with a non-existent user returns `404`. Both are defensible, but running the cheaper field validation first would give more predictable, input-focused feedback. — *Minor, deferred (behaviour is acceptance-criteria compliant either way).*

### Consistency

3. **Enum handling differs by entry point.** The API binds `ChangeTicketStatusRequest.Status` as a real enum and re-checks `Enum.IsDefined`, while MVC and the list filter parse strings via `TicketEnumParser`. This is a reasonable consequence of JSON vs. form binding, but it means "invalid status" is caught in two different places. Worth a one-line comment noting the split so future readers don't assume a single validation point. — *Nit.*

4. **Double fetch after writes.** `CreateAsync` / `UpdateAsync` / `ChangeStatusAsync` save, then call `GetByIdWithDetailsAsync` again to project the detail DTO with navigation properties. This is a deliberate correctness choice (the tracked write entity has no `Include`d graph) and is fine at Core scale; noting it so it isn't mistaken for an accidental extra round-trip. — *Informational.*

### Performance (scope-appropriate, not a Core problem)

5. **Search uses `ToLower().Contains(...)`.** `TicketRepository.ListAsync` lowercases both sides in the query for case-insensitive matching on SQLite. Correct and translatable for SQLite/Core, but it is a non-sargable scan that would not benefit from an index at larger scale. Acceptable for Core; flagged only for future scaling. — *Informational.*

### Testing

6. **No pure Domain unit tests for the state machine.** Coverage of the transition rules comes entirely from HTTP-level integration tests. This satisfies the mandatory tier and is explicitly the documented Core approach (unit suite is out of Core scope), but a handful of fast `TicketStatusStateMachine` unit tests would tighten the feedback loop and document intent at the Domain level. — *Deferred (out of Core scope).*

---

## Changes Made After Review

Per the task constraint, this review pass is **documentation only** — no application code was modified. Observations above are recorded rather than acted on, and the concrete follow-up list is tracked in [`review-fixes.md`](review-fixes.md).

| Change | Type | Status |
|--------|------|--------|
| Authored `code-review-notes.md` (this file) capturing the AI-assisted review, observations, and rejected suggestions | Documentation | Done |
| Authored `review-fixes.md` separating fixes already applied during development from deferred follow-ups | Documentation | Done |

No changes were made to Domain, Application, Web, Infrastructure, or test code. Any code change arising from the observations above will be proposed and confirmed before implementation, consistent with the "ask before changing application code" instruction.

---

## Suggestions Rejected (and Why)

These are review ideas that were considered and deliberately **not** adopted, to keep the change minimal and within Core scope.

| Suggestion | Decision | Rationale |
|------------|----------|-----------|
| Introduce a domain event / audit trail on status change | Rejected | Out of Core scope; no requirement for status history. Adds persistence and complexity with no acceptance-criteria payoff. |
| Replace `ServiceResult<T>` with exceptions or a third-party `Result` library (e.g. FluentResults) | Rejected | The hand-rolled result type is small, dependency-free, and already maps cleanly to HTTP codes. Adding a dependency violates dependency discipline for no functional gain. |
| Move status-transition validation into an EF `SaveChanges` interceptor | Rejected | Would push a business rule into Infrastructure, breaking the "rules in Domain/Application, not Infrastructure" boundary. The state machine belongs in Domain. |
| Add optimistic concurrency (rowversion) to `Ticket` | Rejected | No concurrent-edit requirement in Core; single-user assessment flows. Reasonable as a future hardening item, not a Core fix. |
| Collapse the post-write re-fetch by mapping the tracked entity directly | Rejected | The tracked write entity lacks the `Include`d navigation graph needed by `TicketDetailDto`; mapping it directly would return null/empty related data. The re-fetch is intentional. |
| Add client-side transition enforcement in JavaScript | Rejected | Server-side enforcement is authoritative (NFR-04); the UI already hides invalid options. Client checks would be supplementary only and add surface area without changing correctness. |
