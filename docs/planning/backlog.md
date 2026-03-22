# Backlog

## Sprint 01: MVP Foundation
- Status: done
- Goal: make the repo ready for a first playable exploration loop with clear AI ownership.

## Sprint 02: First Playable Polish
- Status: planned
- Goal: turn the Sprint 01 foundation into a more believable first playable by adding content, scene verification, and stronger validation.

## Parallel Lanes
- Content: placeholder prefabs, ambient population, and zone dressing.
- World: verify scene wiring and traversal flow in the editor.
- Validation: expand CI and smoke checks beyond repo-only validation.
- Support: license and project-settings cleanup that should not block gameplay scope.

## Epic 1: Documentation Foundation
- Status: done
- Goal: standardize repo docs for contributors and AI
- Story 1.1: git workflow and planning doc standardization
  - Tracking ID: `EG-1`
  - Owner: `Docs`
  - Files: `AGENTS.md`, `CONTRIBUTING.md`, `README.md`, `docs/repo-standards.md`, `docs/planning/README.md`, `docs/planning/sprint-01.md`
  - Acceptance: branch workflow, commit format, PR metadata, and Sprint 01 planning structure stay consistent across repo docs.
  - Sprint detail: `docs/planning/sprint-01.md#eg-1-git-workflow-docs`

## Epic 2: Core Runtime Contracts
- Status: done
- Goal: define session state and shared runtime contracts
- Story 2.1: shared session state and scene constants
  - Tracking ID: `EG-2`
  - Owner: `Game.Core`
  - Files: `Assets/Game/Core/GameSession.cs`, `Assets/Game/Core/GameConstants.cs`
  - Acceptance: selected character and active zone persist across scene changes.
  - Sprint detail: `docs/planning/sprint-01.md#eg-2-core-session`
- Story 2.2: catalog validation rules
  - Tracking ID: `EG-3`
  - Owner: `Game.Editor`
  - Files: `Assets/Game/Player/CharacterCatalog.cs`, `Assets/Game/World/WorldCatalog.cs`, `Assets/Game/Tests/EditMode/CatalogValidationTests.cs`
  - Acceptance: generated catalogs validate required entries before runtime work continues.
  - Sprint detail: `docs/planning/sprint-01.md#eg-3-catalog-validation`

## Epic 3: Player Flow
- Status: done
- Goal: character select, movement, and camera
- Story 3.1: character select flow
  - Tracking ID: `EG-4`
  - Owner: `Game.UI`
  - Files: `Assets/Game/UI/CharacterSelectionView.cs`
  - Acceptance: choosing male or female stores the selection and moves into the world flow.
  - Sprint detail: `docs/planning/sprint-01.md#eg-4-character-select`
- Story 3.2: third-person movement and camera
  - Tracking ID: `EG-5`
  - Owner: `Game.Player`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`
  - Acceptance: the player can walk, sprint, look, and stay camera-followed in third person.
  - Sprint detail: `docs/planning/sprint-01.md#eg-5-movement-camera`

## Epic 4: World Flow
- Status: done
- Goal: zone loading and spawn flow
- Story 4.1: zone definitions and world catalog
  - Tracking ID: `EG-6`
  - Owner: `Game.World`
  - Files: `Assets/Game/World/ZoneDefinition.cs`, `Assets/Game/World/WorldCatalog.cs`, `Assets/Game/Tests/EditMode/WorldCatalogTests.cs`
  - Acceptance: village, forest, and mountain zones resolve by stable scene-name contract.
  - Sprint detail: `docs/planning/sprint-01.md#eg-6-zone-catalog`
- Story 4.2: zone loading and avatar spawn
  - Tracking ID: `EG-7`
  - Owner: `Game.World`
  - Files: `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/World/ZonePortal.cs`
  - Acceptance: the selected avatar spawns in the active zone and zone loading is additive/predictable.
  - Sprint detail: `docs/planning/sprint-01.md#eg-7-world-spawn`

## Epic 5: Light Interaction
- Status: done
- Goal: talk, inspect, and prompt behavior
- Story 5.1: interaction probe and prompt UI
  - Tracking ID: `EG-8`
  - Owner: `Game.Interaction`
  - Files: `Assets/Game/Interaction/InteractionProbe.cs`, `Assets/Game/UI/InteractionPromptLabel.cs`
  - Acceptance: the nearest valid interactable shows a prompt and can be triggered.
  - Sprint detail: `docs/planning/sprint-01.md#eg-8-interaction-probe`
- Story 5.2: placeholder NPC and inspectable behavior
  - Tracking ID: `EG-9`
  - Owner: `Game.Interaction`
  - Files: `Assets/Game/Interaction/DialogueNpc.cs`, `Assets/Game/Interaction/InspectableObject.cs`
  - Acceptance: NPCs talk and inspectables log placeholder behavior without adding extra systems.
  - Sprint detail: `docs/planning/sprint-01.md#eg-9-npc-inspectable`

## Epic 6: Editor Tooling
- Status: done
- Goal: repeatable project setup and validation
- Story 6.1: project scaffolding generator
  - Tracking ID: `EG-10`
  - Owner: `Game.Editor`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: one menu action generates starter scenes, config assets, and build settings.
  - Sprint detail: `docs/planning/sprint-01.md#eg-10-editor-scaffold`

## Epic 7: Tests
- Status: done
- Goal: edit mode and play mode coverage
- Story 7.1: session and selection tests
  - Tracking ID: `EG-11`
  - Owner: `Game.Tests.EditMode`
  - Files: `Assets/Game/Tests/EditMode/GameSessionTests.cs`
  - Acceptance: session persistence and character selection are covered by edit mode tests.
  - Sprint detail: `docs/planning/sprint-01.md#eg-11-session-tests`
- Story 7.2: interaction probe tests
  - Tracking ID: `EG-12`
  - Owner: `Game.Tests.PlayMode`
  - Files: `Assets/Game/Tests/PlayMode/InteractionProbeTests.cs`
  - Acceptance: nearest-target interaction is covered by play mode tests.
  - Sprint detail: `docs/planning/sprint-01.md#eg-12-interaction-tests`

## Epic 8: Stretch Backlog
- Status: done
- Goal: track optional sprint-adjacent work that already exists in the workspace
- Story 8.1: animal roaming placeholder behavior
  - Tracking ID: `EG-13`
  - Owner: `Game.Animals`
  - Files: `Assets/Game/Animals/AnimalRoamingAgent.cs`, `Assets/Game/Animals/Game.Animals.asmdef`
  - Acceptance: animals roam or idle naturally without adding hostile behavior.
  - Sprint detail: `docs/planning/sprint-01.md#eg-13-animal-roaming`
- Story 8.2: selected character HUD label
  - Tracking ID: `EG-14`
  - Owner: `Game.UI`
  - Files: `Assets/Game/UI/SelectedCharacterLabel.cs`
  - Acceptance: the current selected character is reflected in a simple on-screen label.
  - Sprint detail: `docs/planning/sprint-01.md#eg-14-selected-character-label`
- Story 8.3: repo codex workflow assets
  - Tracking ID: `EG-15`
  - Owner: `Docs`
  - Files: `.codex/instructions.md`, `.codex/agents/*.md`, `.codex/skills/*.md`
  - Acceptance: repo-owned Codex instructions, agent roles, and task skills are tracked with the project docs.
  - Sprint detail: `docs/planning/sprint-01.md#eg-15-codex-workflow-assets`

## Epic 9: CI Foundation
- Status: done
- Goal: add lightweight GitHub Actions validation before Unity CI is introduced
- Story 9.1: repo checks workflow
  - Tracking ID: `EG-16`
  - Owner: `CI`
  - Files: `.github/workflows/repo-checks.yml`, `scripts/validate_repo.py`
  - Acceptance: pull requests to `main` and pushes to sprint branches run a lightweight validation workflow without needing a Unity license.
  - Sprint detail: `docs/planning/sprint-01.md#eg-16-repo-checks-workflow`

## Epic 10: Repo Governance
- Status: ready
- Goal: formalize repo metadata and legal baseline after Sprint 01 merge
- Story 10.1: license baseline
  - Tracking ID: `EG-17`
  - Owner: `Docs`
  - Files: `LICENSE`, `README.md`, `CONTRIBUTING.md`
  - Acceptance: the repo includes a tracked license file and contributor-facing docs acknowledge the chosen license baseline.
  - Sprint detail: `docs/planning/sprint-02.md#eg-17-license-baseline`
- Story 10.2: shader graph settings baseline
  - Tracking ID: `EG-18`
  - Owner: `ProjectSettings`
  - Files: `ProjectSettings/ShaderGraphSettings.asset`
  - Acceptance: the Shader Graph project setting change is either adopted intentionally or reset deliberately, and the resulting project state is documented as a tracked repo decision.
  - Sprint detail: `docs/planning/sprint-02.md#eg-18-shadergraph-settings-baseline`

## Epic 11: CI Expansion
- Status: ready
- Goal: add Unity-aware validation after repo-checks is stable
- Story 11.1: unity validation workflow
  - Tracking ID: `EG-19`
  - Owner: `CI`
  - Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-02.md`
  - Acceptance: Sprint 02 defines a concrete path for Unity-aware validation, including workflow scope, environment constraints, and the first required checks to automate next.
  - Sprint detail: `docs/planning/sprint-02.md#eg-19-unity-validation-workflow`

## Epic 12: Content Pass 1
- Status: ready
- Goal: populate the first playable world with placeholder-first content that matches the game spec
- Story 12.1: placeholder character and npc prefab hookup
  - Tracking ID: `EG-20`
  - Owner: `Game.Player`
  - Files: `Assets/Resources/Configs/CharacterCatalog.asset`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/WorldPersistent.unity`
  - Acceptance: the player and placeholder NPC references are hooked up to real prefabs or stable placeholder prefabs in scene and catalog flow.
  - Sprint detail: `docs/planning/sprint-02.md#eg-20-placeholder-prefab-hookup`
- Story 12.2: zone dressing pass
  - Tracking ID: `EG-21`
  - Owner: `Content`
  - Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: each zone has a basic placeholder dressing pass with houses, terrain props, vegetation, and traversal landmarks.
  - Sprint detail: `docs/planning/sprint-02.md#eg-21-zone-dressing-pass`
- Story 12.3: npc, inspectable, and animal placement pass
  - Tracking ID: `EG-22`
  - Owner: `Content`
  - Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: the first playable world contains placed NPCs, inspectables, and passive animals that exercise the Sprint 01 systems.
  - Sprint detail: `docs/planning/sprint-02.md#eg-22-ambient-placement-pass`

## Epic 13: First Playable Verification
- Status: ready
- Goal: verify that the first playable loop works in-editor and in CI-supported paths
- Story 13.1: scene wiring and traversal verification
  - Tracking ID: `EG-23`
  - Owner: `Game.Editor`
  - Files: `Assets/Scenes/Bootstrap.unity`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/WorldPersistent.unity`, `docs/planning/sprint-02.md`
  - Acceptance: Sprint 02 defines and captures a repeatable verification pass from bootstrap through character select into the world traversal loop.
  - Sprint detail: `docs/planning/sprint-02.md#eg-23-scene-wiring-verification`
