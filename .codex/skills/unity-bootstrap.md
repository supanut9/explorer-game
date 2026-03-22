# unity-bootstrap

## Use for
- bootstrap scene setup
- build settings generation
- repeatable editor scaffolding
- config asset creation and validation

## Read first
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/planning/sprint-01.md`
- `Assets/Game/Core/GameConstants.cs`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`

## Scope
- `Assets/Game/Editor/*`
- generated scenes and config assets
- validation commands tied to scaffolding

## Rules
- keep project setup code-driven and repeatable
- prefer one menu entry per clear setup or validation action
- generated assets must match the runtime contracts already documented
- build settings must include the required startup and world scenes

## Concerns
- do not leave manual-only setup steps undocumented
- do not generate assets that fail validation immediately
- do not bake task-specific content into global scaffolding unless the sprint requires it

## Outputs
- bootstrap or scaffold tooling
- validation entry points
- generated scene/config expectations

## Done checks
- bootstrap path is repeatable
- build settings include required scenes
- generated assets validate
- docs mention the tooling if the workflow changed
