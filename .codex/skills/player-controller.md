# player-controller

## Use for
- avatar selection flow
- third-person movement
- third-person camera follow
- player input mapping

## Read first
- `docs/game-spec.md`
- `docs/decision-rules.md`
- `docs/planning/sprint-01.md`
- `Assets/Game/Core/GameSession.cs`
- `Assets/Game/Core/GameConstants.cs`

## Scope
- `Assets/Game/Player/*`
- player-facing UI hooks if needed for the same task
- movement and camera tests when logic is extracted

## Rules
- keep movement simple and PC-first
- use the Input System
- keep camera and movement loosely coupled
- read selected character and spawn contracts from shared runtime state, not hidden scene state
- prefer small helper classes for movement math when it improves testability

## Concerns
- do not hardcode scene flow into the movement controller
- do not add combat, inventory, or animation systems to finish basic control
- do not depend on untracked prefabs for core logic
- do not let input assumptions leak into unrelated modules

## Outputs
- player runtime controller
- camera rig
- input bindings or fallbacks
- selection flow hooks when the task covers them

## Done checks
- selected avatar or session state is respected
- walk, sprint, and look behavior work through the intended input path
- camera follow is predictable
- new code stays inside player or shared-core ownership unless a task explicitly includes UI
