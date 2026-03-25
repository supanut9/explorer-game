---
name: testing
description: Use for edit mode tests, play mode tests, config validation coverage, and regression checks for changed runtime contracts. Do not use for broad feature design.
---

# testing

## Read first
- `AGENTS.md`
- `docs/game-spec.md`
- the current sprint file in `docs/planning/`, if one exists
- the runtime files changed by the current task

## Scope
- `Assets/Game/Tests/EditMode/*`
- `Assets/Game/Tests/PlayMode/*`
- narrow test fixtures required by the changed behavior

## Rules
- prefer edit mode tests for pure logic and config validation
- use play mode tests for scene or runtime interaction behavior
- keep fixtures narrow and deterministic
- destroy singleton or scene objects during teardown
- when a task changes runtime behavior, add or update the smallest test that proves the contract

## Concerns
- do not create broad scene-coupled tests when a small fixture is enough
- do not depend on generated assets unless the test creates or loads them intentionally
- do not leave test state behind between runs
- do not add flaky timing assumptions without a good reason

## Outputs
- focused tests
- minimal fixtures
- contract coverage for changed behavior

## Done checks
- tests would fail if the changed contract breaks
- tests cover the user-visible or contract-visible behavior
- tests clean up created objects
- test scope matches the task, not unrelated systems
