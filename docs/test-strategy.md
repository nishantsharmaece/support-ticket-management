# Test Strategy

**Scope:** Core only  
**Traces to:** [acceptance-criteria.md](acceptance-criteria.md), [api-contract.md](api-contract.md), [implementation-plan.md](implementation-plan.md)

Test strategy for the Core implementation. Results are recorded in `test-results.md` after execution.

---

## Testing Objectives

- Prove ticket status state machine rules are enforced server-side (assessment mandatory tier)
- Verify the backend rejects invalid input before persist (AC-09)
- Confirm API error responses are meaningful and status-appropriate (AC-10)
- Provide evidence for submission criterion #11 (`dotnet test` passes)
- Support confidence in Core acceptance criteria without Stretch test coverage

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

---

## Test Approach

- **Primary:** API integration tests via xUnit and WebApplicationFactory against the Web project
- **Rationale:** Exercises the full stack (API, Application, Infrastructure, SQLite); aligns with the backend-heavy assessment and mandatory state-machine tier
- **Secondary:** Manual exploratory testing of the MVC UI for usability (success messages, form errors) — not formally automated in Core
- **Naming:** `MethodName_Scenario_ExpectedResult` per project-rules
- **Execution:** `dotnet test` from solution root; output captured in `test-results.md`

---

## Test Environment

| Aspect | Approach |
|--------|----------|
| Framework | xUnit |
| Host | WebApplicationFactory (SupportTicketManagement.Web) |
| Database | Separate SQLite file or in-memory SQLite per test run; same schema via migrations |
| Configuration | Test-specific connection string; no production secrets |
| Isolation | Fresh database state per test class or collection; seeded users available |
| CI/local | Runs on developer machine; no CI pipeline in Core scope |

---

## Test Data Strategy

- **Users:** Application seed data (3–5 seeded users) or explicit fixture setup per test class
- **Tickets:** Created via API within tests to control initial status for state machine scenarios
- **Comments:** Created via API when needed as preconditions
- **Cleanup:** Database reset between tests or per-class fixture to avoid cross-test contamination
- **No secrets:** Test configuration uses local or test connection strings only

---

## Integration Testing Strategy

- Tests live in SupportTicketManagement.Tests under an Integration area
- Each test issues HTTP requests to API endpoints per api-contract
- Assert on HTTP status codes and response body shape — not implementation internals
- Cover vertical slices: create ticket, get detail, update, change status, add comment
- List endpoint used for search and filter verification

---

## State Machine Testing Strategy (Mandatory)

**Assessment requirement:** Integration tests proving valid transitions succeed and invalid transitions are rejected.

### Valid Transitions (expect 200)

| From | To |
|------|-----|
| Open | InProgress |
| Open | Cancelled |
| InProgress | Resolved |
| InProgress | Cancelled |
| Resolved | Closed |

### Invalid Transitions (expect 409 per api-contract)

- Examples: Closed to Open, Resolved to InProgress, Open to Closed (skipping states), any transition from Cancelled or Closed
- Assert ticket status is unchanged after a rejected transition

### Approach

1. Create a ticket in a known status via API setup
2. PATCH `/api/tickets/{id}/status` with the target status
3. Assert HTTP status code and persisted status on subsequent GET

This tier is **required for Core submission** and must be documented in `test-results.md`.

---

## Backend Validation Testing

Verify server-side rejection (expect 400 or 404) without persisting invalid data:

| Scenario | Expected |
|----------|----------|
| Create ticket with empty title | 400 |
| Create ticket with missing priority or assignee | 400 |
| Create ticket with invalid user ID | 404 |
| Update ticket with invalid fields | 400 |
| Add comment with empty message | 400 |
| Invalid status enum value on PATCH | 400 |

---

## Search and Filter Testing

Integration tests on `GET /api/tickets`:

| Scenario | Expected |
|----------|----------|
| No filters | All tickets returned |
| `search` keyword matching title or description | Matching subset returned |
| `status` filter | Only tickets with matching status returned |
| Combined `search` and `status` | Intersection returned |
| No matches | Empty items array (200, not an error) |
| Empty or omitted `search` | No keyword constraint applied |

---

## Error Handling Testing

| Scenario | Expected Status | Notes |
|----------|-----------------|-------|
| Ticket not found | 404 | GET, PUT, PATCH, POST comment |
| Invalid status transition | 409 | State machine |
| Validation failure | 400 | ErrorResponse shape with field messages |
| No stack traces in response | — | AC-10 |

---

## Entry and Exit Criteria

### Entry Criteria (begin Phase 8 testing)

- State machine enforcement complete (implementation-plan Phase 5)
- Validation and error handling hardened (Phase 7)
- API endpoints match api-contract
- `dotnet build` succeeds

### Exit Criteria (Core testing complete)

- All mandatory state machine integration tests pass
- Supporting integration tests for validation, search/filter, and errors pass
- `dotnet test` exits with code 0
- Results recorded in `test-results.md`
- Assessment criterion #11 satisfied

---

## Risks and Assumptions

| Risk / Assumption | Mitigation |
|-------------------|------------|
| In-memory SQLite behavior differs from file-based | Use same EF migrations; verify persistence in at least one scenario if needed |
| Test order dependency | Isolate database per test class |
| MVC UI errors not automated | Manual spot-check during development; API tests cover server-side behavior |
| Scope creep into unit test suite | Defer unit tests to Stretch; keep Core focused on mandatory integration tier |
| API integration tests sufficient for state machine evidence | Assessment guide explicitly requires integration tests for state machine rules |

---

## Traceability

| Strategy Section | Acceptance Criteria / Requirements |
|------------------|-----------------------------------|
| State machine | AC-05, BR-01–BR-03, Assessment #11 |
| Backend validation | AC-09, FR-17 |
| Error handling | AC-10, FR-18 |
| Search and filter | AC-07, AC-08, FR-11–FR-13 |
