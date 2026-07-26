# Review Fixes

Part C — Submission artifact for the .NET AI Capability Assessment.
Companion to [`code-review-notes.md`](code-review-notes.md). This file separates quality fixes **already applied during development** (Done) from **deferred** follow-ups surfaced by the code review.

**Traces to:** [`code-review-notes.md`](code-review-notes.md), [`design-notes.md`](design-notes.md), [`test-results.md`](test-results.md)

> Note: the review pass itself was documentation-only — no application code was changed. The "Done" items below are decisions and fixes evidenced in the current Core codebase (made while building the solution); the "Deferred" items are new observations from the review that are intentionally not being changed now.

---

## Done — fixes / decisions already applied during development

| # | Item | Where | Why it matters |
|---|------|-------|----------------|
| D1 | Status transition rules centralised in one immutable table | `Domain/Services/TicketStatusStateMachine.cs` | Single source of truth for BR-01–BR-03; no rule duplication in controllers/views |
| D2 | Expected failures returned as `ServiceResult<T>` instead of thrown exceptions | `Application/Common/ServiceResult.cs`, all services | Validation/not-found/conflict become predictable results, not control-flow exceptions |
| D3 | Consistent HTTP mapping for result kinds (`400/404/409/500`) | `Web/Api/ApiResults.cs` | Uniform API error contract; status decisions kept out of services |
| D4 | Global exception handler hides internals (no stack traces/connection strings) | `Web/Middleware/ExceptionHandlingMiddleware.cs` | Satisfies AC "no internal details exposed"; separate JSON vs HTML responses |
| D5 | Enum validation before transition on status change | `Application/Tickets/TicketService.ChangeStatusAsync` | Rejects undefined status with `400`; prevents `KeyNotFoundException` in the state machine |
| D6 | Invalid transition returns `409` and leaves status unchanged | `TicketService` + `ApiResults` + tests | Verified by `StateMachineIntegrationTests` invalid-transition theory |
| D7 | Whitespace-only input treated as empty; inputs trimmed before persist | `TicketService.ValidateTicketFields`, `CommentService.ValidateMessage` | Prevents blank titles/messages passing validation |
| D8 | Read paths use `AsNoTracking()` + explicit `Include(...)` | `Infrastructure/Repositories/TicketRepository.cs` | Correct navigation graph for DTOs; no accidental tracking on reads |
| D9 | DTOs returned at the Application boundary; entities never exposed to views | `Application/Tickets/*Dto*`, controllers | Preserves layering; presentation binds to view models/DTOs only |
| D10 | Terminal states hide transition UI; only valid next statuses offered | `StatusTransitionHelper`, MVC details view model build | Prevents users attempting rejected transitions |
| D11 | Mandatory state-machine integration tests (valid + invalid) green | `tests/.../StateMachineIntegrationTests.cs`; see `test-results.md` (22/22) | Satisfies assessment criterion #11 |

---

## Deferred — follow-ups not changed in this pass

Each maps to an observation in [`code-review-notes.md`](code-review-notes.md). These are intentionally deferred (out of Core scope, low risk, or requiring code changes that need confirmation first).

| # | Item | Ref | Reason for deferral | Suggested action |
|---|------|-----|---------------------|------------------|
| R1 | Harden `TicketStatusStateMachine` against undefined enum keys | Obs 1 | Safe today (all values are keys; callers guard with `Enum.IsDefined`) | Use `TryGetValue` returning empty set; fail safe for future enum additions |
| R2 | Run field validation before user-existence checks in `CreateAsync` | Obs 2 | Current behaviour is acceptance-criteria compliant | Reorder checks so blank input reliably returns `400` regardless of user validity |
| R3 | Document the two-place "invalid status" handling (API enum vs MVC string) | Obs 3 | Cosmetic; behaviour is correct | Add a short code comment noting binding-driven split |
| R4 | Add fast Domain unit tests for the state machine | Obs 6 | Unit-test suite is explicitly out of Core scope | Add xUnit tests over `CanTransition`/`GetAllowedTransitions`/`IsTerminal` |
| R5 | Revisit search implementation for scale | Obs 5 | `ToLower().Contains` is fine for SQLite/Core | Consider collation/FTS or indexed strategy if data volume grows |
| R6 | Consider status-change history / audit trail | Rejected in notes | No Core requirement | Only if a future requirement introduces auditing |

---

## How to use this list

- **Done** items require no action; they are recorded as evidence of quality decisions taken during the build.
- **Deferred** items are candidate follow-ups. Per the working agreement, any of R1–R4 that touch application code will be proposed and confirmed before implementation.
