# Project Reflection

## 1. Project Summary

I selected and built Option 1 — Backend-Heavy Support Ticket Management — for the .NET AI Capability Assessment. The goal was not only a working application but a proper documented, architected, reviewable AI-assisted development lifecycle.

The developed solution is a .NET 8 application with ASP.NET Core MVC for the server-rendered UI, ASP.NET Core Web API for REST endpoints, EF Core with SQLite for database, and Bootstrap 5 for styling. Core features include ticket CRUD, comments, keyword search, status filtering, server-side validation, and an enforced ticket status state machine — the signature engineering judgment piece of the assessment. After Core was complete and verified, I added two Stretch features: ASP.NET Core Identity authentication (login/logout protecting MVC pages) and a light/dark theme toggle persisted in `localStorage`.

Core implementation followed a nine-phase plan across layered commits (solution setup → domain and database → application services → API → MVC → integration tests → documentation). Three pull requests merged feature branches into `dev` and then `main`. Post-core Stretch work continued on `feature/stretch-features`. The solution passes `dotnet build` and 22 integration tests (recorded in `tool-specific/cursor-workflow/test-results.md`).

## 2. AI-assisted Development Workflow

We used Cursor as the primary AI tool, deliberately separating planning from implementation. For significant work — requirements, architecture, API contract, implementation plan — I used Plan Mode: Cursor produced a plan, I reviewed it, then prompted implementation with explicit scope boundaries ("Implement only Phase X… stop after Phase X is complete"). For coding, I used Agent Mode with scoped prompts that referenced approved planning documents and `.cursor/rules/project-rules.md`.

Each session loaded `tool-specific/cursor-workflow/project-context.md` as the source of truth. Prompt history was exported to `ai-prompts/planning/` and `ai-prompts/implementation/` as lifecycle artifacts. I reviewed AI-generated code before accepting changes — adjusting validation behaviour, fixing naming clashes, and removing dead code rather than accepting output blindly. After every major phase I ran `dotnet build`; after Core features I ran `dotnet test` and performed manual browser verification of MVC flows.

```mermaid
flowchart LR
  planDocs[PlanDocs] --> corePhases[CorePhases]
  corePhases --> verifyBuild[TestAndBuild]
  verifyBuild --> stretch[StretchAuthTheme]
  stretch --> reflection[Reflection]
```

## 3. Planning and Documentation Approach

I designed the project architecture before writing application code. On 13 July I created, in sequence: project context, requirements analysis, acceptance criteria, architecture, data model, API contract, UI flow, implementation plan, task checklist, and test strategy. I also established `.cursor/rules/project-rules.md` and the assessment-required folder layout (`docs/`, `ai-prompts/`, `tool-specific/cursor-workflow/`, `database/`).

This document-first approach gave every implementation session a stable reference. I updated documentation continuously — README setup and API tables, acceptance-criteria status, test results, and prompt exports — rather than leaving it until the end. I avoided documenting features I had not built (for example, Swagger was listed as a Stretch option in planning but was not implemented).

## 4. Engineering Decisions and Manual Judgement

Several decisions required judgement beyond what AI could decide alone:

- **Technology stack.** I have reviewed the assessment requirements, and then I chose ASP.NET Core MVC over Razor Pages. MVC gave a clear controller/view structure alongside the Web API in a single Web project, which suited the backend-heavy scope and integration-test strategy.
- **Architecture.** I used lightweight Clean Architecture (Domain → Application → Infrastructure → Web) with the state machine rules in Domain and enforcement in Application, keeping controllers thin.
- **Folder structure.** I aligned the repository with the assessment layout and used feature folders within layers where it improved clarity.
- **Static assets.** I kept the default Bootstrap and jQuery files under `wwwroot/lib/` from the MVC template rather than switching to CDN references — simpler for local/offline development and consistent with the template baseline.
- **`.gitignore`.** During solution build and initialization I added a standard .NET `.gitignore` covering `bin/`, `obj/`, IDE folders, and SQLite database files so build artefacts and local DB state stay out of source control.
- **Code review.** I refined AI output manually: made `CreateTicketRequest.Priority` nullable so omitted priority returns 400 instead of silently defaulting; renamed the domain user DbSet to `TicketUsers` to resolve an Identity naming clash; fixed dark-theme CSS where Bootstrap's `.table-light` forced black text on a dark background.
- **Stretch prioritisation.** With limited time after Core, I implemented a simple authentication and theming (light and dark mode toggle) — high-value, demonstrable features and unit tests.

## 5. Challenges Faced

- **Environment setup.** We installed the .Net 8 SDK on local machine; I pinned the SDK version in `global.json` for reproducibility.
- **Integration test isolation.** Parallel test classes raced on a shared SQLite file on Windows. I fixed this with a serialized collection fixture and `SqliteConnection.ClearAllPools()` on cleanup.
- **Status transition and validation.** While implementing and verifying status changes, integration tests exposed that a missing `priority` field on ticket creation defaulted to `Low` instead of returning 400. I corrected the validation to match the test strategy. The state machine itself — five valid transitions, invalid transitions returning HTTP 409 with status unchanged — was verified through dedicated integration tests.
- **Identity integration.** Adding ASP.NET Core Identity required separating login users (`AspNetUsers`) from seeded ticket assignees (`TicketUsers`), which was not obvious from the initial data model.
- **Time balance.** The assessment weights lifecycle artifacts equally with the application. I had to resist expanding Core scope and Stretch features at the expense of testing evidence and this reflection.

## 6. What Went Well

I followed the phased implementation worked. Each git commit mapped to a logical layer or concern, and PRs (#1 project setup, #2 core implementation, #3 dev to main) kept changes reviewable. The state machine was implemented once in Domain and enforced consistently in both API and MVC, with 10 integration tests proving valid and invalid transitions. Planning documents made AI sessions reproducible — I could point Cursor at `docs/api-contract.md` or `docs/ui-flow.md` and get output aligned with prior decisions. Manual UI checks after MVC and Stretch implementation caught theme contrast issues that automated tests would not.

## 7. What I Would Improve With More Time

- Add Swagger/OpenAPI documentation (planned Stretch, not implemented).
- Add unit tests for `TicketStatusStateMachine` in isolation, complementing the integration suite.
- Apply authentication to the REST API, not only MVC pages.
- Add a CI pipeline, pagination, and other Stretch items from `project-context.md`.
- Merge `feature/stretch-features` through a pull request with a formal review, matching the Core workflow.

## 8. Key Learnings

AI tools are very effective for scaffolding, exploring different options, and generating boilerplate — but they do not replace engineering ownership. The most valuable pattern was small implementation phases with explicit stop boundaries, which prevented generating the entire application in one unreviewable batch. Integration tests exposed incorrect assumptions (priority defaulting) that looked fine in isolation. Documentation was not overhead; it was the mechanism that kept AI output aligned with requirements across multiple sessions. Frequent builds and manual verification caught issues early, especially around UI theming and authentication redirects.

## 9. Conclusion

This project demonstrates AI as an engineering assistant, not an autonomous developer. We chose the stack, designed the architecture, scoped Stretch work, reviewed every significant change, fixed defects manually, and validated the final solution against acceptance criteria AC-01 through AC-11. Cursor accelerated planning and implementation, but judgement — what to build, what to defer, what to reject, and what to verify — remained mine. That balance of AI speed and human accountability is what the assessment was designed to evaluate, and it is the approach I would use on a real team project.
