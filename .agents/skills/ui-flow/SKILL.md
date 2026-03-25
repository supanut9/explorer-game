---
name: ui-flow
description: Use for character select, HUD labels, prompt UI, and menu-to-scene flow. Do not use for large UI frameworks or unrelated runtime systems.
---

# ui-flow

## Read first
- `docs/game-spec.md`
- the current sprint file in `docs/planning/`, if one exists
- `Assets/Game/Core/GameSession.cs`
- `Assets/Game/Core/GameConstants.cs`

## Scope
- `Assets/Game/UI/*`
- minimal supporting hooks in other modules when the task requires them

## Rules
- keep UI minimal and obvious
- read display state from runtime contracts instead of duplicating state in UI
- keep one responsibility per component
- avoid introducing larger UI systems before the core loop is stable

## Concerns
- do not hide game flow logic across many UI scripts
- do not make labels or prompts depend on editor-only setup when code can bind them safely
- do not bundle unrelated styling or layout work into a runtime UI task

## Outputs
- view components
- flow controllers
- state-driven labels or prompts

## Done checks
- UI reflects the correct runtime state
- buttons route to the intended flow
- labels update from the right source of truth
- the task stays lightweight and focused
