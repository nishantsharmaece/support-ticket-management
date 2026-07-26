# Test Results

Part C — Submission artifact for the .NET AI Capability Assessment.  
Adapted from [`tool-specific/cursor-workflow/test-results.md`](tool-specific/cursor-workflow/test-results.md).

**Date:** 2026-07-26  
**Command:** `dotnet test SupportTicketManagement.sln`  
**Outcome:** Passed (exit code 0)

## Summary

| Metric | Value |
|--------|-------|
| Total tests | 22 |
| Passed | 22 |
| Failed | 0 |
| Skipped | 0 |
| Duration | ~10.4 seconds |

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
 Total time: 10.4402 Seconds
```

## Notes

- Integration tests use `WebApplicationFactory<Program>` with an isolated temp SQLite database per test collection.
- Re-run on 2026-07-26 confirmed the same pass counts as the earlier 2026-07-14 run (22/22); duration varies by machine load.
- Nested historical record remains at `tool-specific/cursor-workflow/test-results.md`.

## Assessment criterion

Criterion **#11** (`dotnet test` passes with mandatory integration coverage) is satisfied.
