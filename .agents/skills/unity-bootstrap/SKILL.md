---
name: unity-bootstrap
description: Use for bootstrap scene setup, repeatable editor scaffolding, build settings generation, config asset creation, and validation tooling. Do not use for unrelated gameplay systems.
---

# unity-bootstrap

## Read first
- `docs/game-spec.md`
- `docs/repo-standards.md`
- the current sprint file in `docs/planning/`, if one exists
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
