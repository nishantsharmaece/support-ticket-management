# Implementation Tasks

**Scope:** Core only  
**Traces to:** [implementation-plan.md](../../docs/implementation-plan.md), [project-context.md](project-context.md)

Execution roadmap and checklist for Core implementation. Complete tasks in order within each phase; respect dependencies across phases. Stretch features are out of scope until all Core acceptance criteria pass.

---

## Phase 1: Solution Setup

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-01 | Create solution and projects | Add Domain, Application, Infrastructure, Web, and Tests projects per architecture | — |
| T-02 | Configure project references | Enforce inward dependency rule across all projects | T-01 |
| T-03 | Scaffold folder structure and configuration | Feature folders, SQLite connection placeholder, README stub | T-01 |

---

## Phase 2: Domain Model

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-04 | Implement entities and enums | User, Ticket, Comment entities; TicketStatus and Priority enums per data-model | T-02 |
| T-05 | Define state transition rules | Domain rules for valid and invalid status transitions (BR-01–BR-03) | T-04 |

---

## Phase 3: Database

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-06 | Configure DbContext and mappings | Entity configurations, foreign keys, and indexes per data-model | T-04 |
| T-07 | Create migration and seed data | Initial migration; idempotent user seed; optional sample tickets and comments | T-06 |

---

## Phase 4: Infrastructure

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-08 | Implement repositories | Ticket, Comment, and User repository implementations | T-06, T-07 |
| T-09 | Register Infrastructure in DI | DbContext, repositories, migration apply, and seed on startup | T-08 |

---

## Phase 5: Web API

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-10 | Define Application DTOs and service interfaces | Request and response shapes per api-contract | T-04 |
| T-11 | Implement Application services | Create, list, get, update ticket; add comment use cases | T-08, T-10 |
| T-12 | Implement ticket API endpoints | POST, GET, PUT tickets per api-contract | T-11 |
| T-13 | Implement comment API endpoint | POST comment on ticket per api-contract | T-11 |

---

## Phase 6: MVC UI

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-14 | Create layout and ticket list view | List with empty state per ui-flow UI-01 | T-11 |
| T-15 | Implement create and edit ticket views | Forms with user and priority dropdowns per ui-flow UI-02, UI-04 | T-14 |
| T-16 | Implement ticket details view | Full ticket display and comments list per ui-flow UI-03 | T-14 |
| T-17 | Implement add comment on details | Comment form on details per ui-flow UI-06 | T-16 |

---

## Phase 7: State Machine

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-18 | Implement status change in Application and API | PATCH status endpoint; 409 on invalid transition (AC-05) | T-05, T-12 |
| T-19 | Implement status change in MVC | Status section on details; valid options only per ui-flow UI-05 | T-16, T-18 |

---

## Phase 8: Search and Filtering

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-20 | Add search and filter to API list | `search` and `status` query parameters per api-contract API-07, API-08 | T-12 |
| T-21 | Add search and filter to ticket list UI | Search input, status filter, combined apply per ui-flow | T-14, T-20 |

---

## Phase 9: Validation and Error Handling

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-22 | Harden API validation and ErrorResponse | All inputs per api-contract validation table (AC-09) | T-12, T-13, T-18 |
| T-23 | Harden MVC validation and error display | Field errors, summary, not-found handling; no stack traces (AC-10) | T-15, T-16, T-17, T-19 |

---

## Phase 10: Integration Testing

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-24 | Write state machine integration tests | Valid transitions succeed; invalid transitions return 409 (mandatory tier) | T-18, T-22 |
| T-25 | Record test results | Document `dotnet test` output in test-results.md | T-24 |

---

## Phase 11: Documentation Updates

| ID | Task | Objective | Dependencies |
|----|------|-----------|--------------|
| T-26 | Update README and database setup notes | Clean-machine setup, run, and test instructions | T-25 |
| T-27 | Verify planning docs against implementation | Confirm docs match behavior; note remaining lifecycle artifact gaps | T-26 |

---

## Phase Summary

| Phase | Tasks | Aligns With |
|-------|-------|-------------|
| 1. Solution Setup | T-01 – T-03 | implementation-plan Phase 1 |
| 2. Domain Model | T-04 – T-05 | implementation-plan Phase 2 |
| 3. Database | T-06 – T-07 | implementation-plan Phase 2 |
| 4. Infrastructure | T-08 – T-09 | implementation-plan Phase 2–3 |
| 5. Web API | T-10 – T-13 | implementation-plan Phase 3 |
| 6. MVC UI | T-14 – T-17 | implementation-plan Phase 4 |
| 7. State Machine | T-18 – T-19 | implementation-plan Phase 5 |
| 8. Search and Filtering | T-20 – T-21 | implementation-plan Phase 6 |
| 9. Validation and Error Handling | T-22 – T-23 | implementation-plan Phase 7 |
| 10. Integration Testing | T-24 – T-25 | implementation-plan Phase 8 |
| 11. Documentation Updates | T-26 – T-27 | implementation-plan Phase 9 |

---

## Completion Status (2026-07-14)

All Core tasks T-01 through T-25 are complete. T-26 (README, setup notes) and T-27 (doc verification) completed during final submission review.

| Phase | Status |
|-------|--------|
| 1–4 Solution, Domain, Database, Infrastructure | Complete |
| 5 Web API | Complete |
| 6 MVC UI | Complete |
| 7 State Machine | Complete |
| 8 Search and Filtering | Complete |
| 9 Validation and Error Handling | Complete |
| 10 Integration Testing | Complete (22/22) |
| 11 Documentation Updates | Complete |
