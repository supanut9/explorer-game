# Backlog

## Sprint 01: MVP Foundation
- Status: done
- Goal: make the repo ready for a first playable exploration loop with clear AI ownership.

## Sprint 02: First Playable Polish
- Status: done
- Goal: turn the Sprint 01 foundation into a more believable first playable by adding content, scene verification, and stronger validation.

## Sprint 03: Playable Stability And Control Polish
- Status: in progress
- Goal: stabilize the first playable after Sprint 02 by repairing generated-scene camera state, tightening input contracts, and adding regression coverage for the verified runtime loop.

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
- Status: done
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
- Status: done
- Goal: add Unity-aware validation after repo-checks is stable
- Story 11.1: unity validation workflow
  - Tracking ID: `EG-19`
  - Owner: `CI`
  - Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-02.md`
  - Acceptance: Sprint 02 defines a concrete path for Unity-aware validation, including workflow scope, environment constraints, and the first required checks to automate next.
  - Sprint detail: `docs/planning/sprint-02.md#eg-19-unity-validation-workflow`

## Epic 12: Content Pass 1
- Status: done
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
- Status: done
- Goal: verify that the first playable loop works in-editor and in CI-supported paths
- Story 13.1: scene wiring and traversal verification
  - Tracking ID: `EG-23`
  - Owner: `Game.Editor`
  - Files: `Assets/Scenes/Bootstrap.unity`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/WorldPersistent.unity`, `docs/planning/sprint-02.md`
  - Acceptance: Sprint 02 defines and captures a repeatable verification pass from bootstrap through character select into the world traversal loop.
  - Sprint detail: `docs/planning/sprint-02.md#eg-23-scene-wiring-verification`
- Story 13.2: input system assembly-definition baseline
  - Tracking ID: `EG-24`
  - Owner: `Game.Player`, `Game.Interaction`
  - Files: `Assets/Game/Player/Game.Player.asmdef`, `Assets/Game/Interaction/Game.Interaction.asmdef`
  - Acceptance: assemblies that use `UnityEngine.InputSystem` compile cleanly because their asmdefs reference `Unity.InputSystem`.
  - Sprint detail: `docs/planning/sprint-02.md#eg-24-input-system-assembly-baseline`
- Story 13.3: verification compile blocker cleanup
  - Tracking ID: `EG-25`
  - Owner: `Game.Interaction`, `Game.World`
  - Files: `Assets/Game/Interaction/InteractionProbe.cs`, `Assets/Game/World/WorldRuntimeController.cs`
  - Acceptance: the remaining verification compile error is removed and obsolete lookup warnings are cleared where they are part of the same blocker path.
  - Sprint detail: `docs/planning/sprint-02.md#eg-25-verification-compile-blocker-cleanup`
- Story 13.4: editor bootstrapper compile baseline
  - Tracking ID: `EG-26`
  - Owner: `Game.Editor`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: the scaffold generator compiles cleanly after placeholder-prefab work and no longer fails on ambiguous `Object` references.
  - Sprint detail: `docs/planning/sprint-02.md#eg-26-editor-bootstrapper-compile-baseline`
- Story 13.5: playable bootstrap presentation baseline
  - Tracking ID: `EG-27`
  - Owner: `Game.UI`, `Game.Editor`
  - Files: `Assets/Game/UI/CharacterSelectionView.cs`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: generated bootstrap and character-select scenes render through a camera and the player can make a character choice without hand-building UI.
  - Sprint detail: `docs/planning/sprint-02.md#eg-27-playable-bootstrap-presentation-baseline`
- Story 13.6: scaffold spawn safety baseline
  - Tracking ID: `EG-28`
  - Owner: `Game.World`, `Game.Editor`
  - Files: `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: generated zone spawn points match the placeholder scene layout and entering the world does not immediately drop the player below the playable ground.
  - Sprint detail: `docs/planning/sprint-02.md#eg-28-scaffold-spawn-safety-baseline`
- Story 13.7: world camera snap baseline
  - Tracking ID: `EG-29`
  - Owner: `Game.Player`, `Game.World`
  - Files: `Assets/Game/Player/ThirdPersonCameraRig.cs`, `Assets/Game/World/WorldRuntimeController.cs`
  - Acceptance: when the world scene loads, the camera snaps to a readable third-person framing on the spawned player instead of starting too close or inside the character.
  - Sprint detail: `docs/planning/sprint-02.md#eg-29-world-camera-snap-baseline`
- Story 13.8: input fallback binding baseline
  - Tracking ID: `EG-30`
  - Owner: `Game.Player`, `Game.Interaction`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`, `Assets/Game/Interaction/InteractionProbe.cs`
  - Acceptance: generated placeholder prefabs move, look, and interact with default bindings even when serialized `InputActionProperty` fields exist but contain no bindings.
  - Sprint detail: `docs/planning/sprint-02.md#eg-30-input-fallback-binding-baseline`
- Story 13.9: camera-relative movement baseline
  - Tracking ID: `EG-31`
  - Owner: `Game.Player`, `Game.Editor`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Game/Player/ThirdPersonExplorerController.cs`
  - Acceptance: generated world flow uses the active third-person camera as the movement reference so `WASD` follows the current view direction.
  - Sprint detail: `docs/planning/sprint-02.md#eg-31-camera-relative-movement-baseline`
- Story 13.10: world camera repair on scaffold rerun
  - Tracking ID: `EG-32`
  - Owner: `Game.Editor`, `Game.Player`, `Game.World`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/World/WorldRuntimeController.cs`
  - Acceptance: rerunning project scaffolding repairs existing `WorldPersistent` camera setup and world movement remains camera-relative without manual Inspector edits.
  - Sprint detail: `docs/planning/sprint-02.md#eg-32-world-camera-repair-on-scaffold-rerun`
- Story 13.11: village traversal layout baseline
  - Tracking ID: `EG-33`
  - Owner: `Game.Editor`, `Content`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: the scaffolded village floor and NPC placement support walking up to the villager without leaving the playable ground, and rerunning scaffolding updates existing placeholder layout instead of duplicating it.
  - Sprint detail: `docs/planning/sprint-02.md#eg-33-village-traversal-layout-baseline`
- Story 13.12: movement input drift baseline
  - Tracking ID: `EG-34`
  - Owner: `Game.Player`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`
  - Acceptance: the generated controller ignores small stray input values so the player does not move or rotate continuously without deliberate input.
  - Sprint detail: `docs/planning/sprint-02.md#eg-34-movement-input-drift-baseline`
- Story 13.13: pc-first fallback input baseline
  - Tracking ID: `EG-35`
  - Owner: `Game.Player`, `Game.Interaction`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`, `Assets/Game/Interaction/InteractionProbe.cs`
  - Acceptance: the generated fallback controls are keyboard-and-mouse first and do not drift because of connected gamepads or stick noise.
  - Sprint detail: `docs/planning/sprint-02.md#eg-35-pc-first-fallback-input-baseline`
- Story 13.14: single-player spawn and probe baseline
  - Tracking ID: `EG-36`
  - Owner: `Game.World`, `Game.Editor`
  - Files: `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/World/Game.World.asmdef`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: world entry keeps a single player instance active, the spawned player can interact without manual prefab repair, and the world assembly graph supports that runtime path.
  - Sprint detail: `docs/planning/sprint-02.md#eg-36-single-player-spawn-and-probe-baseline`
- Story 13.15: generated playable baseline assets
  - Tracking ID: `EG-37`
  - Owner: `Content`, `Game.Editor`
  - Files: `Assets/Scenes/*.unity`, `Assets/Resources/Configs/*`, `Assets/Resources/Prefabs/*`, `ProjectSettings/EditorBuildSettings.asset`
  - Acceptance: the generated first-playable scenes, config assets, prefabs, materials, and build settings are tracked as the Sprint 02 baseline after manual verification.
  - Sprint detail: `docs/planning/sprint-02.md#eg-37-generated-playable-baseline-assets`
- Story 13.16: package and editor noise cleanup
  - Tracking ID: `EG-38`
  - Owner: `Repo`
  - Files: `Packages/manifest.json`, `Packages/packages-lock.json`, `ProjectSettings/Packages/*`, `ProjectSettings/SceneTemplateSettings.json`
  - Acceptance: Unity AI assistant package churn and editor-local scene template metadata are removed from the sprint branch so Sprint 02 closes with only intentional gameplay and planning changes.
  - Sprint detail: `docs/planning/sprint-02.md#eg-38-package-and-editor-noise-cleanup`

## Epic 14: Post-Playable Stability
- Status: in progress
- Goal: remove the remaining runtime regressions and tighten the generated first-playable baseline after Sprint 02 merge
- Story 14.1: generated world camera and listener cleanup
  - Tracking ID: `EG-39`
  - Owner: `Game.Editor`, `Game.World`, `Game.Player`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`
  - Acceptance: rerunning project scaffolding collapses duplicate `World Root` and `ThirdPersonCameraRig` objects, the world scene contains exactly one active audio listener, and the rendered camera view matches the control reference during play.
  - Sprint detail: `docs/planning/sprint-03.md#eg-39-generated-world-camera-and-listener-cleanup`
- Story 14.2: input action configuration contract
  - Tracking ID: `EG-40`
  - Owner: `Game.Player`, `Game.Interaction`, `Docs`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`, `Assets/Game/Interaction/InteractionProbe.cs`, `docs/repo-standards.md`, `docs/planning/sprint-03.md`
  - Acceptance: the repo defines whether inline `InputActionProperty` values are supported, and the runtime either honors valid inline actions or clearly rejects them with documented intent.
  - Sprint detail: `docs/planning/sprint-03.md#eg-40-input-action-configuration-contract`
- Story 14.3: placeholder player collision baseline
  - Tracking ID: `EG-41`
  - Owner: `Game.Editor`, `Game.Player`
  - Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Resources/Prefabs/Characters/*.prefab`
  - Acceptance: generated placeholder player prefabs use a single intended collision setup and no longer ship with an extra primitive collider on the visual child.
  - Sprint detail: `docs/planning/sprint-03.md#eg-41-placeholder-player-collision-baseline`
- Story 14.4: first-playable regression coverage
  - Tracking ID: `EG-42`
  - Owner: `Game.Tests.PlayMode`, `Game.Editor`
  - Files: `Assets/Game/Tests/PlayMode/*`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
  - Acceptance: the generated first-playable path has regression coverage for one-camera world startup, player spawn uniqueness, and basic interaction reachability.
  - Sprint detail: `docs/planning/sprint-03.md#eg-42-first-playable-regression-coverage`
- Story 14.5: player jump baseline
  - Tracking ID: `EG-43`
  - Owner: `Game.Player`
  - Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Tests/PlayMode/*`
  - Acceptance: the first-playable controller supports a basic grounded jump with predictable behavior and no regression to the current move/look baseline.
  - Sprint detail: `docs/planning/sprint-03.md#eg-43-player-jump-baseline`
