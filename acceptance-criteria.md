# Acceptance Criteria

Part C — Submission artifact for the .NET AI Capability Assessment.  
Adapted from [`docs/acceptance-criteria.md`](docs/acceptance-criteria.md). Scope: **Core only**.

Acceptance criteria define observable pass/fail outcomes for Core features. Checklists below group criteria for verification. Detailed feature-level criteria and test approach remain in `docs/acceptance-criteria.md` and `docs/test-strategy.md`.

**Traces to:** [`requirements-analysis.md`](requirements-analysis.md), [`docs/requirements-analysis.md`](docs/requirements-analysis.md)

---

## Core

Ticket lifecycle, listing, comments, search/filter, and persistence.

- [ ] User can create a ticket with title, description, priority, assignee, and creator
- [ ] New tickets are saved with status `Open`; `createdAt` and `updatedAt` are set by the system
- [ ] Created ticket appears in the ticket list; user receives confirmation or is redirected appropriately
- [ ] All persisted tickets are displayed in the list with key fields (title, status, priority, assignee)
- [ ] Empty ticket list shows a clear empty-state message
- [ ] User can open ticket detail showing all ticket fields, timestamps, and related comments
- [ ] User can update title, description, priority, and assignee; changes persist and `updatedAt` updates
- [ ] Valid status transitions succeed and are persisted:
  - Open → In Progress
  - Open → Cancelled
  - In Progress → Resolved
  - In Progress → Cancelled
  - Resolved → Closed
- [ ] Updated status is visible on detail view and list after change
- [ ] User can add a comment with a non-empty message; comment saves with creator and `createdAt`
- [ ] New comment appears on ticket detail and remains visible after refresh or restart
- [ ] Keyword search returns tickets whose title or description contains the search term
- [ ] Empty or whitespace keyword returns all tickets (no keyword constraint applied)
- [ ] Filter by status shows only tickets with the selected status
- [ ] Keyword search and status filter can be applied together
- [ ] No-match search/filter shows an empty result state (not an error)
- [ ] Tickets and comments survive application restart; seeded users are available on fresh setup

---

## Validation

Server-side validation is authoritative; invalid input is rejected before persistence.

- [ ] Backend rejects invalid or incomplete input before any data is persisted
- [ ] Empty or whitespace-only title is rejected on create; no ticket is saved
- [ ] Missing required fields (priority, assignee) are rejected with user-visible feedback
- [ ] Invalid assignee or creator reference is rejected; no partial record is saved
- [ ] Required ticket fields are enforced on create and update
- [ ] Comment requires a non-empty message and an existing ticket reference
- [ ] Empty or whitespace-only comment message is rejected; no comment is saved
- [ ] Assignee and creator must reference a seeded user that exists
- [ ] Invalid field values on update leave previous values unchanged
- [ ] Client-side checks (if any) are supplementary only — server-side enforcement remains authoritative

---

## Error Handling

Failed operations produce clear, user-facing feedback without exposing internals.

- [ ] Invalid status transitions are rejected by the backend (e.g., Closed → Open, Resolved → In Progress, Open → Closed, any transition from Cancelled or Closed)
- [ ] Ticket status remains unchanged after a rejected transition; no invalid change is persisted
- [ ] User receives clear, meaningful feedback when a status transition is not allowed
- [ ] Request for a non-existent ticket shows not-found feedback
- [ ] Comment on a non-existent ticket is rejected with feedback
- [ ] Update to a non-existent ticket fails with clear feedback
- [ ] User sees a meaningful, non-technical error message for validation, not-found, and transition failures
- [ ] Application remains usable after an error (no broken state)
- [ ] No stack traces, connection strings, or internal details are exposed to the user
- [ ] Operations do not fail silently

---

## Testing

Mandatory verification for Core, especially the status state machine.

- [ ] Integration tests prove valid status transitions succeed
- [ ] Integration tests prove invalid status transitions are rejected
- [ ] State-machine behaviour is verifiable via automated tests (`dotnet test`)
- [ ] Solution builds successfully (`dotnet build`)
- [ ] Core MVC flows for create, list, detail, update, comments, search, and filter are manually verifiable
- [ ] Persistence across restart is manually or automatically verifiable
- [ ] No secrets are committed to the repository

---

## Documentation

Submission and operability documentation required for assessment.

- [ ] README provides setup instructions that work on a clean machine (.NET 8 SDK, restore, build, run, test)
- [ ] Database setup / migrations and seed behaviour are documented
- [ ] Root `requirements-analysis.md` documents Core requirements (Part C template)
- [ ] Root `acceptance-criteria.md` documents Core acceptance checklists (this file)
- [ ] Detailed planning artifacts remain available under `docs/` (requirements, acceptance criteria, architecture, data model, API contract, UI flow, implementation plan, test strategy)
- [ ] Prompt history / lifecycle artifacts are present under `ai-prompts/`
- [ ] Project context and workflow notes are available under `tool-specific/cursor-workflow/`
