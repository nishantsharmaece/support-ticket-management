# API Contract

Part C — Submission artifact for the .NET AI Capability Assessment.  
Adapted from [`docs/api-contract.md`](docs/api-contract.md). Scope: **Core only**.

**Traces to:** [`requirements-analysis.md`](requirements-analysis.md), [`data-model.md`](data-model.md), [`acceptance-criteria.md`](acceptance-criteria.md)

REST API contract for Core features. All request and response bodies use JSON with camelCase property names. Enum values are represented as strings in JSON.

---

## Endpoint Summary

| # | Feature | Method | Route |
|---|---------|--------|-------|
| 1 | Create Ticket | POST | `/api/tickets` |
| 2 | List Tickets | GET | `/api/tickets` |
| 3 | Get Ticket Details | GET | `/api/tickets/{id}` |
| 4 | Update Ticket | PUT | `/api/tickets/{id}` |
| 5 | Change Ticket Status | PATCH | `/api/tickets/{id}/status` |
| 6 | Add Comment | POST | `/api/tickets/{id}/comments` |
| 7 | Search Tickets | GET | `/api/tickets?search={keyword}` |
| 8 | Filter by Status | GET | `/api/tickets?status={status}` |

List, search, and filter share `GET /api/tickets`. Search and status filter are combinable: `GET /api/tickets?search=login&status=Open`.

---

## Shared Types

### Enums (string values in JSON)

| Enum | Values |
|------|--------|
| priority | `Low`, `Medium`, `High` |
| status | `Open`, `InProgress`, `Resolved`, `Closed`, `Cancelled` |

### UserSummary

Embedded in ticket and comment responses.

```json
{
  "id": 1,
  "name": "Jane Agent",
  "email": "jane@example.com",
  "role": "Agent"
}
```

### TicketSummary

List item shape.

| Property | Type | Notes |
|----------|------|-------|
| id | int | |
| title | string | |
| priority | string | Enum |
| status | string | Enum |
| assignedTo | UserSummary | |
| createdAt | string (ISO 8601 UTC) | |
| updatedAt | string (ISO 8601 UTC) | |

### TicketDetail

Single ticket shape. Includes all TicketSummary properties plus:

| Property | Type | Notes |
|----------|------|-------|
| description | string | |
| createdBy | UserSummary | |
| comments | CommentDto[] | Ordered by createdAt ascending |

### CommentDto

| Property | Type | Notes |
|----------|------|-------|
| id | int | |
| message | string | |
| createdBy | UserSummary | |
| createdAt | string (ISO 8601 UTC) | |

### ErrorResponse

Standard error body for all failure responses.

```json
{
  "title": "Validation failed",
  "status": 400,
  "errors": [
    { "field": "title", "message": "Title is required." }
  ]
}
```

---

## API-01: Create Ticket

**Traces to:** AC-01, FR-01, FR-02

| | |
|---|---|
| **Method** | POST |
| **Route** | `/api/tickets` |

### Request

```json
{
  "title": "Cannot login",
  "description": "User reports login failure",
  "priority": "High",
  "assignedToId": 2,
  "createdById": 1
}
```

### Response

| Status | Body |
|--------|------|
| 201 Created | TicketDetail (status = `Open`) |

`Location` header: `/api/tickets/{id}`

### Validation Rules

| Field | Rules |
|-------|-------|
| title | Required; non-empty; max 200 characters |
| description | Optional; max 4000 characters |
| priority | Required; valid priority enum |
| assignedToId | Required; must reference an existing user |
| createdById | Required; must reference an existing user |

### Error Responses

| Status | Condition |
|--------|-----------|
| 400 | Validation failure |
| 404 | Referenced user not found |

---

## API-02: List Tickets

**Traces to:** AC-02, FR-03

| | |
|---|---|
| **Method** | GET |
| **Route** | `/api/tickets` |

### Request

No query parameters. Returns all tickets.

### Response

| Status | Body |
|--------|------|
| 200 OK | `{ "items": [ TicketSummary, ... ] }` |

Empty array when no tickets exist.

### Error Responses

| Status | Condition |
|--------|-----------|
| 500 | Unhandled server error |

---

## API-03: Get Ticket Details

**Traces to:** AC-03, FR-04, FR-10

| | |
|---|---|
| **Method** | GET |
| **Route** | `/api/tickets/{id}` |

### Request

Path parameter: `id` (ticket identifier)

### Response

| Status | Body |
|--------|------|
| 200 OK | TicketDetail including comments array |

### Error Responses

| Status | Condition |
|--------|-----------|
| 404 | Ticket not found |

---

## API-04: Update Ticket

**Traces to:** AC-04, FR-05

| | |
|---|---|
| **Method** | PUT |
| **Route** | `/api/tickets/{id}` |

### Request

```json
{
  "title": "Updated title",
  "description": "Updated description",
  "priority": "Medium",
  "assignedToId": 3
}
```

Status is not accepted on this endpoint.

### Response

| Status | Body |
|--------|------|
| 200 OK | Updated TicketDetail (status unchanged) |

### Validation Rules

| Field | Rules |
|-------|-------|
| title | Required; non-empty; max 200 characters |
| description | Optional; max 4000 characters |
| priority | Required; valid priority enum |
| assignedToId | Required; must reference an existing user |

### Error Responses

| Status | Condition |
|--------|-----------|
| 400 | Validation failure |
| 404 | Ticket or referenced user not found |

---

## API-05: Change Ticket Status

**Traces to:** AC-05, FR-06, FR-07, FR-08, BR-01, BR-02, BR-03

| | |
|---|---|
| **Method** | PATCH |
| **Route** | `/api/tickets/{id}/status` |

### Request

```json
{
  "status": "InProgress"
}
```

### Valid Transitions

| From | To |
|------|-----|
| Open | InProgress, Cancelled |
| InProgress | Resolved, Cancelled |
| Resolved | Closed |

All other transitions are invalid.

### Response

| Status | Body |
|--------|------|
| 200 OK | Updated TicketDetail with new status |

### Validation Rules

| Field | Rules |
|-------|-------|
| status | Required; valid status enum; transition must be allowed from current ticket status |

### Error Responses

| Status | Condition |
|--------|-----------|
| 404 | Ticket not found |
| 409 | Invalid status transition (e.g., `"Cannot transition from Closed to Open."`) |

---

## API-06: Add Comment

**Traces to:** AC-06, FR-09, FR-10

| | |
|---|---|
| **Method** | POST |
| **Route** | `/api/tickets/{id}/comments` |

### Request

```json
{
  "message": "Investigating the issue",
  "createdById": 1
}
```

### Response

| Status | Body |
|--------|------|
| 201 Created | CommentDto |

`Location` header: `/api/tickets/{id}/comments/{commentId}`

### Validation Rules

| Field | Rules |
|-------|-------|
| message | Required; non-empty; max 4000 characters |
| createdById | Required; must reference an existing user |

### Error Responses

| Status | Condition |
|--------|-----------|
| 400 | Validation failure |
| 404 | Ticket or referenced user not found |

---

## API-07: Search Tickets

**Traces to:** AC-07, FR-11, FR-13

| | |
|---|---|
| **Method** | GET |
| **Route** | `/api/tickets?search={keyword}` |

### Request

| Query Parameter | Rules |
|-----------------|-------|
| search | Optional; matches title or description (case-insensitive); empty or omitted = no keyword filter |

### Response

| Status | Body |
|--------|------|
| 200 OK | `{ "items": [ TicketSummary, ... ] }` |

Empty array when no tickets match.

### Error Responses

None expected.

---

## API-08: Filter Tickets by Status

**Traces to:** AC-08, FR-12, FR-13

| | |
|---|---|
| **Method** | GET |
| **Route** | `/api/tickets?status={status}` |

### Request

| Query Parameter | Rules |
|-----------------|-------|
| status | Optional; valid status enum; combinable with `search` |

**Combined example:** `GET /api/tickets?search=login&status=Open`

### Response

| Status | Body |
|--------|------|
| 200 OK | `{ "items": [ TicketSummary, ... ] }` |

Empty array when no tickets match.

### Error Responses

| Status | Condition |
|--------|-----------|
| 400 | Invalid status query value |

---

## Status Codes Summary

| Code | Usage |
|------|-------|
| 200 | Successful GET, PUT, PATCH |
| 201 | Successful POST (create ticket, add comment) |
| 400 | Validation failure, invalid query parameter |
| 404 | Ticket or referenced user not found |
| 409 | Invalid status transition |
| 500 | Unhandled server error (no internal details exposed) |

---

## Validation Rules Summary

| Field | Rules |
|-------|-------|
| title | Required on create and update; non-empty; max 200 |
| description | Optional; max 4000 |
| priority | Required on create and update; valid enum |
| assignedToId | Required on create and update; existing user |
| createdById | Required on create and comment; existing user |
| status (PATCH body) | Required; valid enum; allowed transition |
| message | Required on comment; non-empty; max 4000 |
| search (query) | Optional; empty treated as no filter |
| status (query) | Optional; valid enum if provided |

---

## Error Responses

| Scenario | Status | Example Message |
|----------|--------|-----------------|
| Missing required field | 400 | `"Title is required."` |
| Invalid enum value | 400 | `"Invalid priority value."` |
| Invalid status query parameter | 400 | `"Invalid status value."` |
| User not found | 404 | `"User with id 99 not found."` |
| Ticket not found | 404 | `"Ticket with id 99 not found."` |
| Invalid status transition | 409 | `"Cannot transition from Closed to Open."` |
| Empty comment message | 400 | `"Message is required."` |

All error responses use the `ErrorResponse` shape. No stack traces or internal details are exposed (NFR-06, AC-10).

---

## Traceability

| API | Acceptance Criteria | Requirements |
|-----|---------------------|--------------|
| API-01 | AC-01 | FR-01, FR-02 |
| API-02 | AC-02 | FR-03 |
| API-03 | AC-03 | FR-04, FR-10 |
| API-04 | AC-04 | FR-05 |
| API-05 | AC-05 | FR-06, FR-07, FR-08 |
| API-06 | AC-06 | FR-09, FR-10 |
| API-07 | AC-07 | FR-11, FR-13 |
| API-08 | AC-08 | FR-12, FR-13 |

Source detail remains in [`docs/api-contract.md`](docs/api-contract.md).
