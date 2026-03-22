# world-zone

## Use for
- zone definitions
- world catalogs
- scene loading
- travel between areas

## Read first
- `docs/game-spec.md`
- `docs/content-plan.md`
- `docs/planning/sprint-01.md`
- `Assets/Game/Core/GameSession.cs`
- `Assets/Game/World/WorldCatalog.cs`

## Scope
- `Assets/Game/World/*`
- shared contracts only when zone work truly needs them
- world tests tied to zone resolution or spawn flow

## Rules
- use stable scene-name contracts
- keep zone loading predictable
- store active zone in shared session state
- keep travel logic light and aligned with the current world flow

## Concerns
- do not create implicit scene-name dependencies outside shared constants or catalog data
- do not leave stale loaded zones active unless the task explicitly requires it
- do not mix large content systems into structural world flow work

## Outputs
- zone definitions
- world catalog entries
- loading and travel logic
- spawn flow support

## Done checks
- active zone resolves correctly
- spawn point and scene mapping are stable
- travel updates the active zone contract
- world config stays complete and valid
