# Sprint 07

## Sprint Goal
- Harden the current v1 exploration slice so the documented connected path is backed by stronger runtime verification and honest validation coverage.

## Committed Items
- Connected-slice runtime regression coverage

## Stretch Items
- Validation scope cleanup and CI truthfulness follow-up

## Dependencies
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/ai-workflow.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-04.md`
- `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- `scripts/validate_unity_project.py`

## Risks
- PlayMode coverage can become brittle if it over-specifies scene layout details instead of stable traversal outcomes.
- Validation updates can drift into claiming broader automation than the repo actually runs.
- Hardening work can sprawl into new gameplay scope unless the sprint stays centered on the existing village-forest loop.

## Workflow
- Use `sprint/07` as the integration branch for Sprint 07.
- Checkout `main`.
- Create or checkout `sprint/07`.
- Commit Sprint 07 work directly on `sprint/07`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the `MVP Foundation` milestone.
- Open a final PR from `sprint/07` into `main`.
- Merge the sprint PR with a merge commit so Sprint 07 history is preserved on `main`.

## Review Notes
- Confirm the village -> forest -> village traversal loop remains the supported connected slice.
- Confirm one player and one active audio listener survive startup and both scene transitions.
- Confirm reachable interaction still works in the village before traversal, in the forest after traversal, and again in the village after returning.
- Confirm repo validation checks the generated markers that the supported slice depends on, including the closed mountain-route signposting.

## Sprint Tasks

### EG-64 Connected-Slice Runtime Regression Coverage
- Story Order: `21.1`
- Status: done
- Owner: `Game.Tests.PlayMode`, `Game.Editor`
- Module: `Assets/Game/Tests/PlayMode`, `scripts`
- Files: `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`, `scripts/validate_unity_project.py`
- Goal: strengthen automated proof for the currently supported connected exploration slice without adding new gameplay behavior.
- Acceptance:
  - PlayMode coverage verifies startup, village interaction, village-to-forest traversal, forest interaction, forest-to-village return, and post-return village interaction
  - the regression path verifies one spawned player and one active audio listener at startup and after each scene transition
  - repo validation checks the generated scene markers that the connected slice depends on, including the village sign that marks the mountain route as closed in this slice
- Subtasks:
  - extend the baseline PlayMode traversal test path to cover return travel and post-return interaction
  - assert the stable startup and scene-transition invariants that the playable slice depends on
  - align the repo validation script with the reviewed scene markers for the current supported route
- Dependencies:
  - `EG-49`
  - `EG-50`
  - `EG-63`
- Notes:
  - keep assertions centered on stable outcomes, not incidental transform layout details
  - do not expand the supported automated path to the mountain route in this sprint

### EG-65 Validation Scope Cleanup And CI Truthfulness Follow-Up
- Story Order: `21.2`
- Status: done
- Owner: `Docs`, `CI`
- Module: `.github/workflows`, `docs/planning`
- Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-07.md`
- Goal: keep validation and workflow messaging honest about what is automated versus still manual in the current slice.
- Acceptance:
  - workflow and sprint docs describe the current validation scope without implying unsupported coverage
  - any manual-only smoke steps for the connected slice are named explicitly if they remain outside automation
  - the follow-up stays documentation or workflow-scope cleanup only
- Subtasks:
  - review current Unity validation workflow messaging against the actual automated checks
  - document any manual-only connected-slice smoke path that still matters for sprint closeout
  - keep workflow scope narrow unless runtime coverage gaps are already closed
- Dependencies:
  - `EG-64`
- Notes:
  - this remains stretch unless the runtime coverage work lands cleanly

## Manual Smoke Path
- Open `Bootstrap`, move through `CharacterSelect`, and enter the world.
- Confirm one active audio listener and one spawned player are present at world startup.
- Interact with the village guide NPC.
- Travel village -> forest and confirm the forest inspectable still works.
- Travel forest -> village and confirm the village guide NPC still works after the return transition.
- Treat mountain as signposted-but-manual-only in this sprint; do not claim automated traversal coverage for that route.

## Validation Scope
- Always automated:
  - `python3 scripts/validate_unity_project.py`
  - `unity-project-sanity` GitHub Actions job
- Conditionally automated when Unity license secrets are configured:
  - `unity-editmode-tests`
  - `unity-playmode-tests`
- Still manual for sprint closeout:
  - in-editor smoke pass of the supported village -> forest -> village route
