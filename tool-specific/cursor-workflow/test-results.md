# Test Results

**Date:** 2026-07-14  
**Command:** `dotnet test SupportTicketManagement.sln`  
**Outcome:** Passed (exit code 0)

## Summary

| Metric | Value |
|--------|-------|
| Total tests | 22 |
| Passed | 22 |
| Failed | 0 |
| Skipped | 0 |
| Duration | ~5.3 seconds |

## Coverage by mandatory tier

### State machine (`StateMachineIntegrationTests`)

| Test | Result |
|------|--------|
| Valid: Open → InProgress | Passed |
| Valid: Open → Cancelled | Passed |
| Valid: InProgress → Resolved | Passed |
| Valid: InProgress → Cancelled | Passed |
| Valid: Resolved → Closed | Passed |
| Invalid: Closed → Open (409, status unchanged) | Passed |
| Invalid: Cancelled → InProgress (409, status unchanged) | Passed |
| Invalid: Open → Closed (409, status unchanged) | Passed |
| Invalid: Resolved → InProgress (409, status unchanged) | Passed |
| Invalid: Closed → Resolved (409, status unchanged) | Passed |

### Backend validation (`ValidationIntegrationTests`)

| Test | Result |
|------|--------|
| Create ticket — empty title (400) | Passed |
| Create ticket — missing priority (400) | Passed |
| Create ticket — invalid user ID (404) | Passed |
| Add comment — empty message (400) | Passed |
| Change status — invalid enum (400) | Passed |
| Get ticket — not found (404) | Passed |

### Search and filter (`SearchFilterIntegrationTests`)

| Test | Result |
|------|--------|
| List — no filters | Passed |
| List — search keyword | Passed |
| List — status filter | Passed |
| List — combined search and status | Passed |
| List — no matches (empty array) | Passed |
| List — invalid status query (400) | Passed |

## Console output (excerpt)

```
Test Run Successful.
Total tests: 22
     Passed: 22
 Total time: 5.2611 Seconds
```

## Notes

- Integration tests use `WebApplicationFactory<Program>` with an isolated temp SQLite database per test collection.
- `CreateTicketRequest.Priority` was made nullable so omitted priority is rejected with 400 per test strategy (previously defaulted to `Low`).

## Assessment criterion

Criterion **#11** (`dotnet test` passes with mandatory integration coverage) is satisfied.
