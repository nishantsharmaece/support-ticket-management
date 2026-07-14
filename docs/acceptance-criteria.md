# Acceptance Criteria

**Scope:** Core only  
**Traces to:** [requirements-analysis.md](requirements-analysis.md), [project-context.md](../tool-specific/cursor-workflow/project-context.md)

Acceptance criteria define observable pass/fail outcomes for each Core feature. Each section is independently verifiable through manual or automated verification. Detailed test approach is documented separately in `test-strategy.md`.

## Traceability Matrix

| Feature | Requirement IDs | Assessment Criterion |
|---------|-----------------|-------------------|
| AC-01: Create Ticket | FR-01, FR-02, BR-04, BR-06 | #1 |
| AC-02: List Tickets | FR-03, BR-08 | #2 |
| AC-03: View Ticket Details | FR-04, FR-10 | #3 |
| AC-04: Update Ticket | FR-05, BR-04, BR-06 | #4 |
| AC-05: Change Ticket Status | FR-06, FR-07, FR-08, BR-01, BR-02, BR-03 | #6 |
| AC-06: Add Comments | FR-09, FR-10, BR-05 | #5 |
| AC-07: Search Tickets | FR-11, FR-13, BR-08 | #7 (partial) |
| AC-08: Filter by Status | FR-12, FR-13, BR-08 | #7 (partial) |
| AC-09: Input Validation | FR-17, BR-04, BR-05, BR-06, NFR-04 | #9 |
| AC-10: Error Handling | FR-08, FR-18, NFR-06 | — |
| AC-11: Data Persistence | FR-14, NFR-02, NFR-03 | #8 |

---

## AC-01: Create Ticket

**Traces to:** FR-01, FR-02, BR-04, BR-06

### Preconditions

- Application is running and database is available
- Seeded users exist and are selectable as assignee and creator
- User is on the create ticket form

### Success Criteria

- [ ] User can submit a ticket with title, description, priority, assignee, and creator
- [ ] New ticket is saved with status `Open`
- [ ] `createdAt` and `updatedAt` are set by the system
- [ ] Created ticket appears in the ticket list
- [ ] User receives confirmation or is redirected to the new ticket or list

### Failure Criteria

- Empty or whitespace-only title is rejected; no ticket is persisted
- Missing required fields (priority, assignee) are rejected with user-visible feedback
- Invalid assignee or creator reference is rejected; no partial record is saved
- User sees a clear validation message explaining what failed

---

## AC-02: List Tickets

**Traces to:** FR-03, BR-08

### Preconditions

- Application is running
- Database may contain zero or more tickets

### Success Criteria

- [ ] All persisted tickets are displayed in the list
- [ ] Each list entry shows key fields: title, status, priority, and assignee
- [ ] When no tickets exist, user sees a clear empty-state message
- [ ] List reflects data from persistent storage, not session-only data

### Failure Criteria

- List omits persisted tickets or shows data that was not saved
- Application error occurs with no user-facing feedback
- List displays stale data that does not match the database

---

## AC-03: View Ticket Details

**Traces to:** FR-04, FR-10

### Preconditions

- Ticket exists in the database

### Success Criteria

- [ ] User can open a ticket detail view from the list
- [ ] Detail shows all ticket fields: title, description, priority, status, assignee, creator, `createdAt`, `updatedAt`
- [ ] All comments for the ticket are displayed with message, creator, and `createdAt`
- [ ] Displayed data matches what is stored in the database

### Failure Criteria

- Request for a non-existent ticket ID shows not-found feedback
- One or more ticket fields are missing or incorrectly displayed
- Comments are missing, incomplete, or out of date

---

## AC-04: Update Ticket

**Traces to:** FR-05, BR-04, BR-06

### Preconditions

- Ticket exists in the database

### Success Criteria

- [ ] User can update title, description, priority, and assignee
- [ ] Changes are persisted and visible on the ticket detail view
- [ ] `updatedAt` reflects the time of the last successful update
- [ ] Updated ticket appears correctly in the ticket list

### Failure Criteria

- Invalid field values are rejected; previous values remain unchanged
- Update to a non-existent ticket fails with clear feedback
- Partial or silent update occurs without user awareness
- Invalid assignee reference is rejected

---

## AC-05: Change Ticket Status (State Machine)

**Traces to:** FR-06, FR-07, FR-08, BR-01, BR-02, BR-03

**Valid transitions:**

| From | To |
|------|-----|
| Open | In Progress |
| Open | Cancelled |
| In Progress | Resolved |
| In Progress | Cancelled |
| Resolved | Closed |

### Preconditions

- Ticket exists in the database
- For success scenarios: ticket is in the source state of a valid transition
- For failure scenarios: ticket is in a state where the attempted transition is invalid

### Success Criteria

- [ ] Open → In Progress succeeds and status is persisted
- [ ] Open → Cancelled succeeds and status is persisted
- [ ] In Progress → Resolved succeeds and status is persisted
- [ ] In Progress → Cancelled succeeds and status is persisted
- [ ] Resolved → Closed succeeds and status is persisted
- [ ] Updated status is visible on detail view and list after change

### Failure Criteria

- Invalid transitions are rejected by the backend (e.g., Closed → Open, Resolved → In Progress, Open → Closed, any transition from Cancelled or Closed)
- Ticket status remains unchanged after a rejected transition
- User receives clear, meaningful feedback explaining the transition is not allowed
- No status change is persisted when transition is invalid

---

## AC-06: Add Comments

**Traces to:** FR-09, FR-10, BR-05

### Preconditions

- Ticket exists in the database
- User is on the ticket detail view

### Success Criteria

- [ ] User can add a comment with a non-empty message
- [ ] Comment is saved with creator and `createdAt` timestamp
- [ ] New comment appears on the ticket detail view immediately
- [ ] Comment persists and is visible after page refresh or restart

### Failure Criteria

- Empty or whitespace-only message is rejected; no comment is saved
- Comment on a non-existent ticket is rejected with feedback
- User sees a clear validation message when submission fails

---

## AC-07: Search Tickets

**Traces to:** FR-11, FR-13, BR-08

### Preconditions

- Application is running
- Tickets exist in the database (zero-match scenarios are valid)

### Success Criteria

- [ ] Keyword search returns tickets whose title or description contains the search term
- [ ] Empty or whitespace keyword returns all tickets (no keyword constraint applied)
- [ ] Search results reflect only persisted ticket data
- [ ] When no tickets match, user sees an empty result state (not an error)

### Failure Criteria

- Search returns tickets that do not match the keyword
- Search operates on non-persisted or stale data
- Search failure produces no user feedback

---

## AC-08: Filter by Status

**Traces to:** FR-12, FR-13, BR-08

### Preconditions

- Application is running
- Tickets with multiple statuses exist in the database

### Success Criteria

- [ ] Filter by a selected status shows only tickets with that status
- [ ] Status filter can be combined with keyword search
- [ ] Combined filter returns tickets matching both criteria
- [ ] When no tickets match the filter, user sees an empty result state (not an error)

### Failure Criteria

- Filter returns tickets with a status other than the one selected
- Filter has no effect on the displayed list
- Combined search and filter returns tickets that fail either criterion

---

## AC-09: Input Validation

**Traces to:** FR-17, BR-04, BR-05, BR-06, NFR-04

### Preconditions

- User submits a create ticket, update ticket, or add comment form

### Success Criteria

- [ ] Backend rejects invalid input before any data is persisted
- [ ] Required ticket fields are enforced on create and update
- [ ] Comment requires a non-empty message and an existing ticket reference
- [ ] Assignee and creator must reference a seeded user that exists
- [ ] Server-side validation is authoritative regardless of client-side checks

### Failure Criteria

- Invalid data is saved to the database
- Validation exists only on the client with no server-side enforcement
- Invalid user references are accepted and persisted

---

## AC-10: Error Handling

**Traces to:** FR-08, FR-18, NFR-06

### Preconditions

- An operation triggers a validation failure, not-found condition, or invalid status transition

### Success Criteria

- [ ] User sees a meaningful, non-technical error message
- [ ] Application remains usable after an error (no broken state)
- [ ] Validation errors identify the problem in plain language
- [ ] Not-found conditions (missing ticket) show appropriate feedback
- [ ] No stack traces, connection strings, or internal details are exposed to the user

### Failure Criteria

- Operation fails silently with no user feedback
- Unhandled exception or stack trace is shown to the user
- Error message is missing, generic, or unhelpful
- Application enters an unusable state after an error

---

## AC-11: Data Persistence

**Traces to:** FR-14, NFR-02, NFR-03

### Preconditions

- Tickets and comments have been created in a prior session
- Application is restarted

### Success Criteria

- [ ] All previously created tickets are retrievable after application restart
- [ ] All previously created comments are retrievable after application restart
- [ ] Ticket field values and statuses match pre-restart state
- [ ] Seeded users are available on fresh application setup

### Failure Criteria

- Data created before restart is lost or incomplete
- Storage is in-memory only and does not survive restart
- Seed users are missing on fresh setup

---

## Submission Criteria

Additional assessment criteria not covered by feature sections above:

- [ ] No secrets committed to the repository (Assessment #10)
- [ ] State-machine integration tests pass — valid transitions succeed, invalid transitions are rejected (Assessment #11; verifiability requirement only; test definitions in `test-strategy.md`)
