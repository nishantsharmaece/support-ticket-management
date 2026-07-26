# Test Strategy

Part C — Submission artifact for the .NET AI Capability Assessment.  
Adapted from [`docs/test-strategy.md`](docs/test-strategy.md). Scope: **Core only**.

**Traces to:** [`acceptance-criteria.md`](acceptance-criteria.md), [`docs/acceptance-criteria.md`](docs/acceptance-criteria.md), [`docs/api-contract.md`](docs/api-contract.md)  
**Results:** [`test-results.md`](test-results.md)

---

## Test Scope

### In Scope (Core)

| Area | Verification Level |
|------|------------------|
| State machine transitions | Automated integration tests (mandatory) |
| Backend validation | Integration tests with targeted API negative scenarios |
| Search and filter | Integration tests on list endpoint |
| Error handling | Integration tests for 400, 404, and 409 responses |
| Data persistence | Implicit via integration test database lifecycle |

### Out of Scope (Core)

- Unit tests (Stretch)
- Edge-case and failure test suites beyond the mandatory tier (Stretch)
- UI or browser automation (manual exploratory only; not required for submission)
- Authentication, pagination, performance, and load testing
- Stretch features

### Objectives

- Prove ticket status state machine rules are enforced server-side (assessment mandatory tier)
- Verify the backend rejects invalid input before persist (AC-09)
- Confirm API error responses are meaningful and status-appropriate (AC-10)
- Provide evidence for assessment criterion #11 (`dotnet test` passes)

### Environment and execution

| Aspect | Approach |
|--------|----------|
| Framework | xUnit |
| Host | WebApplicationFactory (SupportTicketManagement.Web) |
| Database | Isolated SQLite per test collection; same schema via migrations |
| Naming | `MethodName_Scenario_ExpectedResult` |
| Command | `dotnet test` from solution root |

---

## Unit / Component

**Core stance:** Dedicated unit and component tests are **deferred to Stretch**.

Rationale:

- The assessment mandatory tier requires **integration** tests for the status state machine
- API integration tests already exercise Domain rules, Application services, and Infrastructure through HTTP
- Isolating Domain transition helpers or validators in pure unit tests would duplicate coverage without satisfying criterion #11

If Stretch is pursued later, candidates for unit tests include:

- Status transition allowance matrix in Domain
- Request validation rules in isolation
- Mapping / DTO shaping without HTTP

---

## API / Integration

**Primary approach for Core.** Tests live in `SupportTicketManagement.Tests` and issue HTTP requests against the Web host via `WebApplicationFactory`. Assertions use HTTP status codes and response body shape — not implementation internals.

### State machine (mandatory)

Valid transitions (expect **200**):

| From | To |
|------|-----|
| Open | InProgress |
| Open | Cancelled |
| InProgress | Resolved |
| InProgress | Cancelled |
| Resolved | Closed |

Invalid transitions (expect **409**; status unchanged), including:

- Closed → Open, Resolved → InProgress, Open → Closed (skipping states)
- Any transition from Cancelled or Closed

Approach: create a ticket in a known status → `PATCH /api/tickets/{id}/status` → assert status code and persisted status on subsequent GET.

### Backend validation

| Scenario | Expected |
|----------|----------|
| Create ticket with empty title | 400 |
| Create ticket with missing priority or assignee | 400 |
| Create ticket with invalid user ID | 404 |
| Add comment with empty message | 400 |
| Invalid status enum value on PATCH | 400 |
| Ticket not found (GET / related ops) | 404 |

### Search and filter

Integration tests on `GET /api/tickets`:

| Scenario | Expected |
|----------|----------|
| No filters | All tickets returned |
| `search` keyword matching title or description | Matching subset |
| `status` filter | Only matching status |
| Combined `search` and `status` | Intersection |
| No matches | Empty items array (200) |
| Invalid status query | 400 |

### Error handling

| Scenario | Expected Status |
|----------|-----------------|
| Ticket not found | 404 |
| Invalid status transition | 409 |
| Validation failure | 400 (ErrorResponse with field messages) |
| No stack traces in response | AC-10 |

---

## Edge Cases

Covered within Core integration suites where they support acceptance criteria:

| Edge case | How covered |
|-----------|-------------|
| Skipping states (e.g. Open → Closed) | Invalid transition → 409 |
| Terminal statuses (Cancelled, Closed) | Further transitions rejected |
| Empty search / omitted search | No keyword constraint; all tickets (subject to other filters) |
| No-match search/filter | 200 with empty collection (not an error) |
| Invalid enum / malformed status | 400 |
| Missing required fields on create | 400; nothing persisted |
| Non-existent ticket or user reference | 404 |

Broader failure suites (concurrency, partial writes under crash, malformed JSON body variants, large payloads) are **not** in Core scope.

---

## Tests Not Covered and Why

| Not covered | Why |
|-------------|-----|
| Unit tests for Domain / Application in isolation | Stretch; mandatory evidence is integration-level state machine coverage |
| MVC / browser UI automation | Manual exploratory only; API tests cover server-side behaviour; UI automation not required for Core submission |
| Authentication and authorization tests | Stretch (Identity); Core has no login requirement |
| Pagination, performance, and load tests | Out of Core scope |
| Full matrix of every invalid transition pair | Representative invalid cases prove rejection; exhaustive combinatorial suite deferred |
| Stretch features (theme toggle, Identity flows) | Deferred until Core acceptance criteria are met |
| CI pipeline execution evidence | No CI in Core scope; local `dotnet test` recorded in `test-results.md` |

---

## Entry and Exit Criteria

**Entry:** State machine enforcement complete; validation and error handling hardened; API matches contract; `dotnet build` succeeds.

**Exit:** Mandatory state machine integration tests pass; supporting validation, search/filter, and error tests pass; `dotnet test` exits 0; results recorded in `test-results.md`; assessment criterion #11 satisfied.
