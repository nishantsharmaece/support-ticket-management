# Prompt History — Testing

Activity index for test strategy, mandatory integration tests, and recorded results.

**Honesty note:** Original **test strategy** was authored in the planning chat; **test implementation and green suite** happened inside the Core implementation chat. Dedicated root `test-strategy.md` / `test-results.md` files were added in a **structure-gate remediation** session (documentation only; optional re-run of `dotnet test`). Export that remediation to [`testing/04-root-test-strategy-and-results.md`](testing/04-root-test-strategy-and-results.md) if not already present.

---

## Session 1 — Test strategy (planning)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-13 |
| **Export** | [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) (final segment: create `docs/test-strategy.md`) |
| **Prompt summary** | Create a concise Core test strategy only: mandatory state-machine integration tier, validation and search/filter coverage, out-of-scope unit/UI automation. No application code. |
| **AI response summary** | Documented in-scope vs out-of-scope, environment (WebApplicationFactory + SQLite), and case outline including missing priority → 400. |
| **Accepted** | Integration-first Core approach; mandatory valid/invalid transition cases; unit tests deferred as Stretch. |
| **Changed** | Human kept strategy aligned to assessment criterion #11. |
| **Rejected** | Claiming UI automation or full unit suite as Core deliverables. |

---

## Session 2 — Mandatory integration tests (implementation)

| Field | Summary |
|-------|---------|
| **When** | 2026-07-15 |
| **Export** | [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) (segment: “Implement the mandatory integration tests”) |
| **Prompt summary** | Implement valid/invalid state transitions, backend validation, search/filter tests; run all tests; fix failures; update `test-results.md`; stop. |
| **AI response summary** | Added `StateMachineIntegrationTests`, `ValidationIntegrationTests`, `SearchFilterIntegrationTests`, factory/fixture helpers. Fixed parallel SQLite races on Windows and nullable priority validation so suite reaches **22/22**. |
| **Accepted** | 22 integration tests via WebApplicationFactory; serialized collection; results recorded under cursor-workflow. |
| **Changed** | Test fixture isolation; `Priority?` on create DTO to match strategy. |
| **Rejected** | Leaving silent priority default; parallel shared-DB fixture that failed on Windows. |

Evidence of results: [`../test-results.md`](../test-results.md), [`../tool-specific/cursor-workflow/test-results.md`](../tool-specific/cursor-workflow/test-results.md).

---

## Session 3 — Remediation: root test strategy & results

| Field | Summary |
|-------|---------|
| **When** | 2026-07-26 (structure-gate remediation) |
| **Export** | [`testing/04-root-test-strategy-and-results.md`](testing/04-root-test-strategy-and-results.md) *(export this remediation chat if missing)* |
| **Prompt summary** | Guide requires root `test-strategy.md` and `test-results.md` from `docs/test-strategy.md` and `tool-specific/cursor-workflow/test-results.md`. Do not change application code (optional `dotnet test` for counts). |
| **AI response summary** | Created root Part C test artifacts with current pass counts; left detailed sources in place. |
| **Accepted** | Exact root filenames; mirrored strategy/results for assessor gate. |
| **Changed** | Formatting/headings; refreshed counts if tests were re-run. |
| **Rejected** | Expanding test scope or inventing new suites during remediation. |

---

## Accepted vs rejected (testing overall)

| Decision | Outcome | Why |
|----------|---------|-----|
| Mandatory state-machine integration tier | Accepted | Assessment criterion #11 |
| Missing priority → 400 | Accepted (after fix) | Matches strategy / AC |
| Pure Domain unit tests for state machine | Deferred | Explicitly out of Core scope |
| Browser automation as Core | Rejected | Manual exploratory only |

---

## Detailed exports

| Path | Role |
|------|------|
| [`planning/project-planning-chat-01.md`](planning/project-planning-chat-01.md) | Original test strategy prompt |
| [`implementation/project-implementation-chat-01.md`](implementation/project-implementation-chat-01.md) | Test implementation + fixes |
| [`testing/04-root-test-strategy-and-results.md`](testing/04-root-test-strategy-and-results.md) | Remediation export (root test files) |
