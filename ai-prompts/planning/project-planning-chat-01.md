# Project context document outline
_Exported on 7/13/2026 at 16:11:04 GMT+5:30 from Cursor (3.10.20)_

---

**User**

@c:\Users\Nishant Sharma\Downloads\Net_AI_Assessment_ProjectGuide.docx.pdf 

I am building Option 1 – Support Ticket Management System for the .NET AI Capability Assessment.

The uploaded assessment guide is the source of truth for this project.

My technology stack is:

- ASP.NET Core MVC (.NET 8)
- ASP.NET Core Web API
- EF Core
- SQLite
- Bootstrap 5
- xUnit

Architecture:

- Lightweight Clean Architecture

Create the following document only:

tool-specific/cursor-workflow/project-context.md

The document should become the persistent project context for this repository and all future AI conversations.

Include:

- Project overview
- Business context
- Assessment objective
- Core scope
- Stretch scope (clearly marked as out of scope until Core is complete)
- Functional summary
- High-level architecture
- Technology stack
- Repository structure
- Development workflow
- Documentation strategy
- Testing strategy
- AI collaboration workflow
- Assumptions
- Constraints
- Success criteria

Do not create any other documents.

Do not generate implementation details.

Do not write any application code.

Keep the document concise (2–3 pages) and suitable as the primary project context for future planning and implementation.

---

**Cursor**

I'll read the assessment guide and explore the repository so the plan for `project-context.md` matches the guide and your stack.


Checking existing project files to see what's already in place and what the plan should cover.


I have the assessment guide and repo context. Creating a plan for `project-context.md`.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

Implementing `project-context.md` per the plan. Starting with the first todo.


Verifying the document against the plan and assessment guide.


Verification complete: all sections align with the assessment guide. Marking todos complete.


`project-context.md` is in place at [`support-ticket-management/tool-specific/cursor-workflow/project-context.md`](support-ticket-management/tool-specific/cursor-workflow/project-context.md). No other files were changed.

### What changed

The placeholder was replaced with a 16-section persistent context document covering:

| Section | Content |
|---------|---------|
| Project Overview | Option 1 backend-heavy assessment project; Cursor as primary AI tool |
| Business Context | Internal ticket management; state machine as the signature judgment piece |
| Assessment Objective | Lifecycle focus, effort split (8–12h Core), Part A/B/C weights |
| Core Scope | Entities, features, state machine (with mermaid diagram), mandatory integration tests |
| Stretch Scope | Clearly marked **out of scope until Core is complete** |
| Functional Summary | Six high-level user flows |
| High-Level Architecture | Lightweight Clean Architecture with mermaid diagram |
| Technology Stack | .NET 8, MVC, Web API, EF Core, SQLite, Bootstrap 5, xUnit |
| Repository Structure | Assessment-required layout with exists/planned status |
| Development Workflow | Seven-phase approach from planning through Stretch |
| Documentation Strategy | Role of each artifact; cross-references `project-rules.md` |
| Testing Strategy | Mandatory state-machine integration tests; Stretch deferred |
| AI Collaboration Workflow | How Cursor should be used in this repo |
| Assumptions & Constraints | Single-tenant, no auth in Core, no secrets, artifact priority |
| Success Criteria | All 11 Core acceptance criteria + submission readiness checklist |

### Verification

- All Option 1 Core entities, features, state transitions, and acceptance criteria match the assessment guide
- No implementation details (no class names, endpoints, or migration commands)
- Stretch is explicitly blocked until Core acceptance criteria pass
- Conventions are referenced in `.cursor/rules/project-rules.md` rather than duplicated

This document is ready to use as the primary context for future planning and implementation sessions.

---

**User**

Based on the uploaded assessment guide and the approved project-context.md, create only the following document:

docs/requirements-analysis.md

The document should capture the functional and non-functional requirements for the Core implementation only.

Include:

- Functional requirements
- Non-functional requirements
- Business rules
- Assumptions
- Constraints
- Edge cases
- Out-of-scope items (Stretch features)

Do not include:

- Database design
- API design
- UI design
- Implementation details
- Test cases
- Acceptance criteria (these will be documented separately)

Keep the document concise (2–3 pages), avoid duplicating project-context.md, and use the assessment guide as the source of truth.

Stop after creating this document only.

---

**Cursor**

Reviewing `project-context.md` and the repo layout so the requirements-analysis plan stays aligned and avoids duplication.


I have enough context from `project-context.md` and the assessment guide. Creating the plan for `docs/requirements-analysis.md`.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

Verifying the document against the plan and assessment guide.


`docs/requirements-analysis.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/requirements-analysis.md`](support-ticket-management/docs/requirements-analysis.md) — Core-scoped requirements analysis with:

| Section | Content |
|---------|---------|
| **My Understanding** | Internal ticket app, seeded users, enforced state machine |
| **Functional Requirements** | 18 requirements (`FR-01`–`FR-18`) across ticket management, status lifecycle, comments, search/filter, data/users, validation |
| **Non-Functional Requirements** | 9 requirements (`NFR-01`–`NFR-09`) covering architecture, persistence, security, scope |
| **Business Rules** | 9 enforceable rules (`BR-01`–`BR-09`) with state machine mermaid diagram |
| **Assumptions** | 9 requirements-specific assumptions (priority values deferred to data model) |
| **Constraints** | Core-only scope; no auth, no schema/API/UI in this doc |
| **Edge Cases** | Status transitions, validation, search/filter, data integrity, persistence |
| **Out of Scope** | All Stretch items from the assessment guide |

### Verification

- Cross-references `project-context.md` without duplicating workflow, architecture, or success criteria
- Excludes database design, API design, UI design, implementation details, test cases, and acceptance criteria
- All Option 1 Core entities (User, Ticket, Comment), features, state transitions, and validation requirements are covered
- Traceable IDs (`FR-`, `NFR-`, `BR-`) ready for `spec.md` and `acceptance-criteria.md`

---

**User**

Based on the uploaded assessment guide, the approved project-context.md, and requirements-analysis.md, create only the following document:

docs/acceptance-criteria.md

Create clear acceptance criteria for each Core feature.

Include:

- Create Ticket
- List Tickets
- View Ticket Details
- Update Ticket
- Change Ticket Status (State Machine)
- Add Comments
- Search Tickets
- Filter by Status
- Input Validation
- Error Handling
- Data Persistence

For each feature, define:
- Preconditions (if applicable)
- Success criteria
- Failure criteria

Do not include:
- Test cases
- Implementation details
- API design
- Database design

Keep the document concise (2–3 pages) and ensure every acceptance criterion is traceable to the assessment requirements.

Stop after creating this document only.

---

**Cursor**

Reviewing the approved context and requirements documents to draft the acceptance-criteria plan.


I have the source documents. Creating the plan for `docs/acceptance-criteria.md`.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/acceptance-criteria.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/acceptance-criteria.md`](support-ticket-management/docs/acceptance-criteria.md) — Core acceptance criteria with:

| Section | Coverage |
|---------|----------|
| **Traceability matrix** | Maps AC-01–AC-11 to FR/BR IDs and assessment criteria #1–#11 |
| **AC-01: Create Ticket** | Preconditions, 5 success checkboxes, 4 failure criteria |
| **AC-02: List Tickets** | Empty state, persisted data display |
| **AC-03: View Ticket Details** | All fields + comments |
| **AC-04: Update Ticket** | Field updates, `updatedAt`, validation |
| **AC-05: Change Ticket Status** | All 5 valid transitions + invalid transition failures |
| **AC-06: Add Comments** | Append-only, persistence |
| **AC-07: Search Tickets** | Keyword match, empty keyword behavior |
| **AC-08: Filter by Status** | Status filter + combined search |
| **AC-09: Input Validation** | Server-side enforcement |
| **AC-10: Error Handling** | User-facing messages, no stack traces |
| **AC-11: Data Persistence** | Survives restart, seed users |
| **Submission Criteria** | No secrets (#10), state-machine tests pass (#11) |

### Verification

- All 11 requested feature areas covered with preconditions, success, and failure criteria
- Every section traces to `FR-`/`BR-` IDs from `requirements-analysis.md`
- All 11 assessment Core acceptance criteria mapped
- Excludes test cases, implementation details, API design, and database design
- ~230 lines — within the 2–3 page target

---

**User**

Based on the uploaded assessment guide, the approved project-context.md, requirements-analysis.md, and acceptance-criteria.md, create only the following document:

docs/architecture.md

Document the proposed solution architecture for the Core implementation.

Include:

- High-level architecture overview
- Solution structure
- Project responsibilities
- Layer responsibilities (Presentation, Application, Domain, Infrastructure)
- Dependency flow
- Folder structure
- Design decisions and rationale
- Key design principles

Do not include:

- Database schema
- API endpoints
- UI screens
- Implementation code
- Test cases

Keep the document concise (2–3 pages) and focus on architectural decisions only.

Stop after creating this document only.

---

**Cursor**

Reviewing approved docs and project rules to draft the architecture plan.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/architecture.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/architecture.md`](support-ticket-management/docs/architecture.md) — Core solution architecture with:

| Section | Content |
|---------|---------|
| **High-Level Overview** | Single deployable app (MVC + API + SQLite); runtime flow mermaid diagram |
| **Solution Structure** | Solution layout and project reference table |
| **Project Responsibilities** | Domain, Application, Infrastructure, Web, Tests |
| **Layer Responsibilities** | Owns / must-not-contain table for all four layers |
| **Dependency Flow** | Inward dependency rule; project dependency mermaid; DI composition root |
| **Folder Structure** | Feature-grouped folders per project |
| **Design Decisions** | 8 decisions with rationale (Clean Architecture, single Web host, direct service calls, state machine placement, repositories, DTOs, SQLite, feature folders) |
| **Key Design Principles** | 10 architectural principles |

### Verification

- Aligns with `project-rules.md` project names, layer boundaries, and dependency rule
- Covers Core scope from `requirements-analysis.md` (state machine, no auth, server-side validation)
- Excludes database schema, API endpoints, UI screens, implementation code, and test cases
- Cross-references upstream docs without duplicating requirements or acceptance criteria
- ~200 lines — within the 2–3 page target

---

**User**

Based on the uploaded assessment guide and the approved planning documents (project-context.md, requirements-analysis.md, acceptance-criteria.md, and architecture.md), create only the following document:

docs/data-model.md

Design the data model for the Core implementation.

Include:

- Entities and their purpose
- Entity relationships
- Entity attributes with data types
- Primary and foreign keys
- Entity relationship diagram (Mermaid ER diagram preferred)
- Index recommendations (only where justified)
- Seed data strategy
- Migration strategy
- Design decisions and assumptions

Do not include:

- EF Core implementation
- SQL scripts
- Repository code
- API endpoints
- Business logic

Keep the document concise (2–3 pages) and ensure it supports all Core requirements.

Stop after creating this document only.

---

**Cursor**

Reviewing approved planning documents to draft the data-model plan.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/data-model.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/data-model.md`](support-ticket-management/docs/data-model.md) — Core logical data model with:

| Section | Content |
|---------|---------|
| **Entities and Purpose** | User, Ticket, Comment |
| **Entity Attributes** | Full attribute tables with data types, required flags, and notes |
| **Enum Values** | TicketStatus (0–4) and Priority (0–2) lookup tables |
| **Entity Relationships** | 1:N relationships, FKs, delete behaviors (Restrict/Cascade) |
| **ER Diagram** | Mermaid `erDiagram` with all entities and keys |
| **Index Recommendations** | `IX_Ticket_Status`, `UQ_User_Email`; rationale for omitted indexes |
| **Seed Data Strategy** | Required users; optional sample tickets/comments; idempotent approach |
| **Migration Strategy** | Initial migration, location, startup apply, version control, test DB |
| **Design Decisions** | 11 decisions with rationale |
| **Traceability** | Maps data model to FR/BR requirements |

### Verification

- All assessment guide entity fields covered (User, Ticket, Comment)
- Supports search (Title, Description), filter (Status), FKs, and persistence
- Excludes EF Core code, SQL scripts, repositories, API endpoints, and business logic
- ~210 lines — within the 2–3 page target

---

**User**

Based on the uploaded assessment guide and the approved planning documents, create only:

docs/api-contract.md

Design the REST API contract for the Core implementation.

Include:

- Endpoint summary
- HTTP method
- Route
- Request payload
- Response payload
- Status codes
- Validation rules
- Error responses

Cover only the Core features:

- Create Ticket
- List Tickets
- Get Ticket Details
- Update Ticket
- Change Ticket Status
- Add Comment
- Search Tickets
- Filter Tickets by Status

Do not include:

- Controller implementation
- Service implementation
- Database details
- Authentication
- Swagger configuration

Keep the document concise (2–3 pages).

Stop after creating this document only.

---

**Cursor**

Reviewing approved planning documents to draft the API contract plan.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/api-contract.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/api-contract.md`](support-ticket-management/docs/api-contract.md) — Core REST API contract with:

| Section | Content |
|---------|---------|
| **Endpoint Summary** | 8 features across 6 routes (list/search/filter share `GET /api/tickets`) |
| **Shared Types** | Enums, UserSummary, TicketSummary, TicketDetail, CommentDto, ErrorResponse |
| **API-01–08** | Method, route, request/response payloads, validation, error responses per endpoint |
| **Status Codes** | 200, 201, 400, 404, 409, 500 |
| **Validation Rules Summary** | Field-level rules traceable to BR-04–BR-06 |
| **Error Responses** | Standard ErrorResponse shape with example messages |
| **Traceability** | Maps each API to AC and FR IDs |

### Design highlights

- `PATCH /api/tickets/{id}/status` isolates state machine changes from field updates
- Invalid transitions return **409 Conflict**
- Search and status filter combine via query params (`?search=login&status=Open`)
- String enums in JSON (`Open`, `High`) for readability

### Verification

- All 8 Core features covered with full request/response contracts
- Excludes controller/service code, database details, auth, and Swagger
- Aligned with `project-rules.md` REST conventions (`/api/tickets`, plural resources)

---

**User**

Based on the uploaded assessment guide and the approved planning documents, create only:

docs/ui-flow.md

Design the user interface flow for the Core implementation using ASP.NET Core MVC.

Include:

- Screen overview
- Navigation flow
- User interactions
- Form validation
- Error handling
- Success messages
- Search and filter workflow
- State transition workflow

Describe the expected behavior for the following screens:

- Dashboard / Ticket List
- Create Ticket
- Ticket Details
- Edit Ticket
- Change Ticket Status
- Add Comment

Do not include:

- Razor code
- HTML/CSS
- Bootstrap implementation
- JavaScript
- Controller or API implementation

Keep the document concise (2–3 pages).

Stop after creating this document only.

---

**Cursor**

Reviewing approved planning documents to draft the UI flow plan.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/ui-flow.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/ui-flow.md`](support-ticket-management/docs/ui-flow.md) — Core MVC UI flow with:

| Section | Content |
|---------|---------|
| **Screen Overview** | 6 screens; status change and comments as sections on Ticket Details |
| **Navigation Flow** | Mermaid diagram; no login; default landing is ticket list |
| **Cross-Cutting** | Form validation, error handling, success messages |
| **Search & Filter** | 8-step workflow on ticket list |
| **State Transition** | Valid next-status options per current status |
| **UI-01–UI-06** | Per-screen displays, interactions, validation, errors, success |
| **Traceability** | Maps screens to AC-01 through AC-10 |

### Verification

- All six requested screens documented with expected behavior
- State machine UI shows only valid transitions; terminal states disable the selector
- Excludes Razor, HTML, CSS, Bootstrap, JavaScript, and controller/API implementation
- Aligned with acceptance criteria and requirements (search, filter, validation, error states)
- ~230 lines — within the 2–3 page target

---

**User**

Based on the uploaded assessment guide and all approved planning documents, create only:

docs/implementation-plan.md

Create a practical implementation plan for the Core scope only.

Break the implementation into small, logical phases.

For each phase include:

- Objective
- Deliverables
- Dependencies
- Expected outcome

The phases should be implementation-ready and minimize rework.

Suggested phases:

1. Solution and project setup
2. Domain model and database
3. Backend API
4. MVC UI
5. State machine
6. Search and filtering
7. Validation and error handling
8. Integration testing
9. Documentation review

Do not include implementation code or detailed task breakdown.

Keep the document concise (2–3 pages).

Stop after creating this document only.

---

**Cursor**

Reviewing approved planning documents to align the implementation plan with the architecture and scope.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/implementation-plan.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/implementation-plan.md`](support-ticket-management/docs/implementation-plan.md) — Core implementation plan with nine phases:

| Phase | Focus | Key Outcome |
|-------|--------|-------------|
| **1** | Solution and project setup | Clean `dotnet build`, correct layer structure |
| **2** | Domain model and database | Entities, migrations, seed data, persistence |
| **3** | Backend API | CRUD + comments via REST (status PATCH stubbed) |
| **4** | MVC UI | List, create, detail, edit, add comment in browser |
| **5** | State machine | Full enforcement in API + MVC (signature piece) |
| **6** | Search and filtering | Combined `search` + `status` on list |
| **7** | Validation and error handling | Server-side validation, user-facing errors |
| **8** | Integration testing | Mandatory state-machine tests + `test-results.md` |
| **9** | Documentation review | README, setup notes, submission readiness |

Each phase includes **Objective**, **Deliverables**, **Dependencies**, and **Expected Outcome**, plus a dependency summary table and mermaid sequencing diagram.

### Sequencing notes

- State machine **rules** defined in Phase 2; **enforcement** completed in Phase 5 (avoids rework)
- Search/filter extends existing list in Phase 6 after API and UI exist
- Validation hardened in Phase 7 after features are in place
- Integration tests in Phase 8 after state machine and validation are complete

---

**User**

Based on the uploaded assessment guide and all approved planning documents, create only:

tool-specific/cursor-workflow/tasks.md

Create a concise implementation task list for the Core scope only.

The task list should act as the execution roadmap for the project.

Group the tasks into logical implementation phases.

For each task include:

- Task ID
- Task name
- Objective
- Dependencies (if any)

Cover only:

- Solution setup
- Domain model
- Database
- Infrastructure
- Web API
- MVC UI
- State machine
- Search and filtering
- Validation and error handling
- Integration testing
- Documentation updates

Do not include:

- Stretch features
- Detailed implementation steps
- Code snippets
- Time estimates

Keep the document concise (1–2 pages) so it can be used as a checklist during implementation.

Stop after creating this document only.

---

**Cursor**

Aligning the task list with `implementation-plan.md` and the requested coverage areas.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`tool-specific/cursor-workflow/tasks.md` is created. No other files were changed.

### What was created

[`support-ticket-management/tool-specific/cursor-workflow/tasks.md`](support-ticket-management/tool-specific/cursor-workflow/tasks.md) — Core implementation checklist with **27 tasks** in **11 phases**:

| Phase | Tasks | Coverage |
|-------|-------|----------|
| 1. Solution Setup | T-01 – T-03 | Solution setup |
| 2. Domain Model | T-04 – T-05 | Domain model |
| 3. Database | T-06 – T-07 | Database |
| 4. Infrastructure | T-08 – T-09 | Infrastructure |
| 5. Web API | T-10 – T-13 | Web API |
| 6. MVC UI | T-14 – T-17 | MVC UI |
| 7. State Machine | T-18 – T-19 | State machine |
| 8. Search and Filtering | T-20 – T-21 | Search and filtering |
| 9. Validation and Error Handling | T-22 – T-23 | Validation and error handling |
| 10. Integration Testing | T-24 – T-25 | Integration testing |
| 11. Documentation Updates | T-26 – T-27 | Documentation updates |

Each task includes **ID**, **name**, **objective**, and **dependencies**. A phase summary table maps tasks to [implementation-plan.md](support-ticket-management/docs/implementation-plan.md) phases.

### Verification

- All 11 requested coverage areas included
- No Stretch tasks, code snippets, time estimates, or detailed implementation steps
- Aligned with `implementation-plan.md` sequencing
- ~115 lines — suitable as a 1–2 page implementation checklist

---

**User**

Based on the uploaded assessment guide and all approved planning documents, create only:

docs/test-strategy.md

Create a concise test strategy for the Core implementation.

Include:

- Testing objectives
- Test scope
- Test approach
- Test environment
- Test data strategy
- Integration testing strategy
- State machine testing strategy (mandatory)
- Backend validation testing
- Search and filter testing
- Error handling testing
- Entry and exit criteria
- Risks and assumptions

Do not include:

- Test cases
- xUnit code
- Implementation details
- Stretch features

Keep the document concise (1–2 pages) and aligned with the assessment's mandatory Core testing requirements.

Stop after creating this document only.

---

**Cursor**

Reviewing approved planning documents for mandatory testing requirements.


---

**User**

Implement the plan as specified, it is attached for your reference. Do NOT edit the plan file itself.

To-do's from the plan have already been created. Do not create them again. Mark them as in_progress as you work, starting with the first one. Don't stop until you have completed all the to-dos.

---

**Cursor**

`docs/test-strategy.md` is created. No other files were changed.

### What was created

[`support-ticket-management/docs/test-strategy.md`](support-ticket-management/docs/test-strategy.md) — Core test strategy with:

| Section | Content |
|---------|---------|
| **Testing Objectives** | State machine proof, validation, errors, submission evidence |
| **Test Scope** | In/out scope tables; Stretch and UI automation excluded |
| **Test Approach** | xUnit + WebApplicationFactory primary; manual MVC exploratory secondary |
| **Test Environment** | SQLite isolation, test config, no secrets |
| **Test Data Strategy** | Seeded users, API-created tickets, per-class cleanup |
| **Integration Testing** | Full-stack API tests via api-contract |
| **State Machine (Mandatory)** | All 5 valid transitions (200); invalid examples (409); 3-step approach |
| **Backend Validation** | 400/404 scenarios at strategy level |
| **Search and Filter** | List endpoint scenarios per api-contract |
| **Error Handling** | 404, 409, 400, no stack traces |
| **Entry/Exit Criteria** | Phase 5+7 complete to enter; mandatory tests pass + test-results.md to exit |
| **Risks and Assumptions** | SQLite isolation, scope control, API sufficiency |
| **Traceability** | Maps to AC-05, AC-07–AC-10, FR-11–FR-18, Assessment #11 |

### Verification

- Mandatory state machine integration tier clearly marked as required for submission
- No test cases, xUnit code, or implementation details
- Stretch testing explicitly out of scope
- ~155 lines — within the 1–2 page target

