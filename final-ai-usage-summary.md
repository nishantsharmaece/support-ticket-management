# Final AI Usage Summary

Part C — Submission artifact for the .NET AI Capability Assessment.  
Summarizes how Cursor was used across the full lifecycle, what was accepted vs rejected, and where prompt history is stored.

**Traces to:** [`reflection.md`](reflection.md), [`pr-description.md`](pr-description.md), [`tool-workflow.md`](tool-workflow.md), [`code-review-notes.md`](code-review-notes.md), [`debugging-notes.md`](debugging-notes.md), [`docs/reflection.md`](docs/reflection.md)

**Structure-gate note:** This root filename is required by the official Participant Guide. Remediation places ownership summaries at repository root while leaving detailed narrative under `docs/reflection.md` and prompt exports under `ai-prompts/`.

---

## Primary tool and modes

| Item | Value |
|------|-------|
| Primary AI tool | Cursor |
| Planning / design | Plan Mode — produce a plan, human review, then scoped implement prompts |
| Implementation | Agent Mode — phase-bounded prompts with explicit stop conditions |
| Persistent guidance | `.cursor/rules/project-rules.md`, `tool-specific/cursor-workflow/project-context.md` |
| Verification | Human-run `dotnet build`, `dotnet test`, manual MVC/UI checks |

AI was treated as an engineering assistant, not an autonomous developer. Stack choice, architecture, Stretch prioritisation, and acceptance/rejection of suggestions remained human-owned.

---

## AI usage across the lifecycle

| Phase | How AI was used | Human ownership |
|-------|-----------------|-----------------|
| **Planning** | Drafted requirements, acceptance criteria, architecture, data model, API contract, UI flow, implementation plan, test strategy | Chose Option 1, MVC over Razor Pages, Clean Architecture boundaries, Core vs Stretch scope |
| **Design** | Consolidated layer diagrams, DTO/API shapes, state-machine rules into docs | Kept business rules in Domain/Application; rejected pushing rules into Infrastructure |
| **Implementation** | Scaffolded solution, entities, EF configs, services, API/MVC, phased commits | Phase stop boundaries; reviewed every significant diff before accept |
| **Testing** | Generated WebApplicationFactory harness and mandatory integration cases | Fixed Windows SQLite isolation; ensured tests match test strategy (e.g. missing priority → 400) |
| **Debugging** | Located DTO/Identity/CSS root causes under constrained prompts | Confirmed with build, full test suite, and browser checks |
| **Code review** | Cross-checked Core vs acceptance criteria and layering | Recorded observations; deferred or rejected out-of-scope changes |
| **Documentation / ownership** | Helped author Part C root artifacts and align filenames to the guide | Structure-gate remediation: exact root names; leave `docs/` sources in place |

---

## Accepted vs rejected AI output

### Accepted (with human validation)

| Output | Why accepted | How validated |
|--------|--------------|---------------|
| Layered solution scaffolding and feature folders | Matched approved architecture | `dotnet build`; project references inward |
| Planning document drafts | Accelerated document-first workflow | Edited against assessment Core scope; no undocumented Stretch claims |
| State machine + Application enforcement pattern | Single source of truth for BR-01–BR-03 | 10 dedicated state-machine integration tests |
| Integration test suite structure | Covers mandatory assessment tier | 22/22 `dotnet test` |
| Serialized SQLite test fixture + pool clear | Fixed real Windows file-lock races | Full suite green after fix |
| Nullable `Priority` on create DTO | Matches validation strategy (omit → 400) | Validation integration test |
| `TicketUsers` rename for Identity Stretch | Separates login accounts from assignees | Build + login redirect checks |
| Dark-theme CSS overrides for `.table-light` | Fixed real contrast bug | Manual UI review in dark mode |
| Dead-code / parser cleanup at submission review | Hygiene without behaviour change | Build + 22/22 tests |

### Rejected or corrected

| Output / suggestion | Decision | Why |
|---------------------|----------|-----|
| Silent default of omitted `priority` to `Low` | Corrected | Contradicted test strategy and validation AC |
| Large unscoped “build the whole app” batches | Rejected pattern | Unreviewable; used phase stop boundaries instead |
| Documenting Swagger as implemented | Rejected | Stretch planned but not built |
| Domain events / status audit trail | Rejected | Out of Core scope |
| Replace `ServiceResult<T>` with FluentResults or exceptions | Rejected | Unnecessary dependency; current mapping is clear |
| Enforce transitions in EF `SaveChanges` interceptor | Rejected | Would push business rules into Infrastructure |
| Optimistic concurrency (rowversion) | Rejected | No Core concurrent-edit requirement |
| Client-side JS as authoritative transition enforcement | Rejected | Server-side enforcement is authoritative |
| Premature Stretch/Swagger/CI expansion before Core evidence | Deferred | Lifecycle artifacts and Core AC take priority |

Further detail: [`code-review-notes.md`](code-review-notes.md) (Suggestions Rejected), [`review-fixes.md`](review-fixes.md), [`debugging-notes.md`](debugging-notes.md).

---

## Where prompt history lives (`ai-prompts/`)

Prompt exports are organized by activity. Existing files at authorship of this summary:

| Path | Activity |
|------|----------|
| `ai-prompts/planning/project-structure-chat-01.md` | Initial repository structure and project rules |
| `ai-prompts/planning/project-planning-chat-01.md` | Requirements through implementation plan |
| `ai-prompts/implementation/project-implementation-chat-01.md` | Core phased implementation, tests, submission hygiene |
| `ai-prompts/implementation/authentication_and_dark_light_theme_implement.md` | Stretch Identity + theme |

Additional activity exports (design, testing, debugging, code-review, documentation) belong under matching folders such as:

- `ai-prompts/design/`
- `ai-prompts/testing/`
- `ai-prompts/debugging/`
- `ai-prompts/code-review/`
- `ai-prompts/documentation/`

**This ownership session** should be exported to:

`ai-prompts/documentation/07-ownership-artifacts.md`

Root companions produced in the same remediation stream: `reflection.md`, `pr-description.md`, `final-ai-usage-summary.md` (this file). Detailed narrative reflection remains in `docs/reflection.md`.

---

## Outcome

Cursor accelerated scaffolding, documentation, and constrained fixes. Human judgment defined scope, corrected incorrect defaults, rejected layering and dependency suggestions that did not serve Core acceptance criteria, and verified the solution with build, 22 integration tests, and manual UI checks. Structure-gate remediation ensures required ownership filenames exist at repository root so evaluation can proceed past layout checks.
