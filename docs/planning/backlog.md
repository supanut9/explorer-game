# Backlog

## Sprint 01: MVP Foundation
- Status: done
- Goal: make the repo ready for a first playable exploration loop with clear AI ownership.

## Sprint 02: First Playable Polish
- Status: done
- Goal: turn the Sprint 01 foundation into a more believable first playable by adding content, scene verification, and stronger validation.

## Sprint 03: Playable Stability And Control Polish
- Status: done
- Goal: stabilize the first playable after Sprint 02 by repairing generated-scene camera state, tightening input contracts, and adding regression coverage for the verified runtime loop.

## Sprint 04: Connected Exploration Slice
- Status: done
- Goal: turn the stable first playable into a readable multi-zone exploration slice with visible traversal, guidance, and verification.

## Sprint 05: Visual Identity And Presentation Pass
- Status: done
- Goal: give the connected exploration slice a coherent visual direction, stronger scene mood, and cleaner player-facing presentation without changing the core loop.

## Sprint 06: Codex Workflow Standardization
- Status: done
- Goal: align the repository's Codex workflow metadata with the current native skills, subagents, and command-rules model so future AI work follows the repo standard by default.

## Sprint 07: MVP Slice Hardening
- Status: in progress
- Goal: harden the current v1 exploration slice so the documented connected path is backed by stronger runtime verification and honest validation coverage.

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
  - Files: `AGENTS.md`, `.codex/agents/*.toml`, `.agents/skills/*/SKILL.md`, `.codex/rules/*.rules`
  - Acceptance: repo-owned Codex instructions, custom subagents, task skills, and command rules are tracked with the project docs.
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
- Status: done
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
- Story 14.6: sprint 03 asset sync and noise cleanup
  - Tracking ID: `EG-44`
  - Owner: `Game.Player`, `Game.Tests.PlayMode`, `ProjectSettings`
  - Files: `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Game/Tests/PlayMode/*.meta`, `ProjectSettings/SceneTemplateSettings.json`
  - Acceptance: serialized prefab and test assets are brought into line with the Sprint 03 runtime baseline, and editor-only scene-template noise is excluded from the sprint branch.
  - Sprint detail: `docs/planning/sprint-03.md#eg-44-sprint-03-asset-sync-and-noise-cleanup`

## Epic 15: Connected Exploration
- Status: done
- Goal: make the world traversal loop readable and worthwhile beyond the initial spawn area
- Story 15.1: zone traversal exposure
  - Tracking ID: `EG-45`
  - Owner: `Game.World`, `Game.Editor`
  - Files: `Assets/Game/World/ZonePortal.cs`, `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: the player can intentionally travel from `VillageZone` into at least one secondary zone and return through visible, reachable traversal anchors without breaking spawn stability.
  - Sprint detail: `docs/planning/sprint-04.md#eg-45-zone-traversal-exposure`
- Story 15.2: guide npc and signposting pass
  - Tracking ID: `EG-46`
  - Owner: `Content`, `Game.Interaction`
  - Files: `Assets/Scenes/VillageZone.unity`, `Assets/Game/Interaction/DialogueNpc.cs`
  - Acceptance: the starting village clearly points the player toward the forest and mountain routes using a guide NPC and simple world signage, without introducing a quest system.
  - Sprint detail: `docs/planning/sprint-04.md#eg-46-guide-npc-and-signposting-pass`
- Story 15.3: zone-specific interaction copy
  - Tracking ID: `EG-47`
  - Owner: `Game.Interaction`, `Content`
  - Files: `Assets/Game/Interaction/DialogueNpc.cs`, `Assets/Game/Interaction/InspectableObject.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: placed NPCs and inspectables present distinct placeholder text by zone so exploration surfaces different content instead of repeated generic prompts.
  - Sprint detail: `docs/planning/sprint-04.md#eg-47-zone-specific-interaction-copy`
- Story 15.4: landmark and traversal dressing pass
  - Tracking ID: `EG-48`
  - Owner: `Content`
  - Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: each zone has readable landmarks, paths, and traversal framing that help the player find transitions, NPCs, and inspectables without extra UI systems.
  - Sprint detail: `docs/planning/sprint-04.md#eg-48-landmark-and-traversal-dressing-pass`
- Story 15.5: multi-zone verification coverage
  - Tracking ID: `EG-49`
  - Owner: `Game.Tests.PlayMode`, `Game.Editor`
  - Files: `Assets/Game/Tests/PlayMode/*`, `docs/planning/sprint-04.md`
  - Acceptance: Sprint 04 captures a repeatable verification path for entering a secondary zone, returning, and confirming that prompts and traversal still work after scene transitions.
  - Sprint detail: `docs/planning/sprint-04.md#eg-49-multi-zone-verification-coverage`
- Story 15.6: animal roaming navmesh fallback
  - Tracking ID: `EG-51`
  - Owner: `Game.Animals`
  - Files: `Assets/Game/Animals/AnimalRoamingAgent.cs`, `docs/planning/sprint-04.md`
  - Acceptance: passive animals stop spamming NavMesh warnings when a zone has no baked NavMesh, and the connected exploration slice remains playable without requiring navigation bake setup.
  - Sprint detail: `docs/planning/sprint-04.md#eg-51-animal-roaming-navmesh-fallback`
- Story 15.7: sprint 04 scene and material sync
  - Tracking ID: `EG-52`
  - Owner: `Content`, `Game.Editor`, `ProjectSettings`
  - Files: `Assets/Scenes/*.unity`, `Assets/Resources/Prefabs/Materials/*.mat`, `ProjectSettings/SceneTemplateSettings.json`
  - Acceptance: the generated village, forest, and mountain scene assets plus their new material assets are synced to the Sprint 04 traversal baseline, and editor-only scene template noise is excluded from the sprint branch.
  - Sprint detail: `docs/planning/sprint-04.md#eg-52-sprint-04-scene-and-material-sync`

## Epic 16: Validation Expansion
- Status: done
- Goal: turn the Unity validation plan into real automated coverage after the connected exploration slice is stable
- Story 16.1: unity validation workflow implementation
  - Tracking ID: `EG-50`
  - Owner: `CI`
  - Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-04.md`
  - Acceptance: the repo runs at least one automated Unity-aware validation path in GitHub Actions with the environment and scope documented in the sprint plan.
  - Sprint detail: `docs/planning/sprint-04.md#eg-50-unity-validation-workflow-implementation`

## Epic 17: Visual Direction
- Status: done
- Goal: make the first playable look intentional instead of scaffolded while preserving the stable traversal loop
- Story 17.1: world palette and material cohesion
  - Tracking ID: `EG-53`
  - Owner: `Content`, `Game.Editor`
  - Files: `Assets/Resources/Prefabs/Materials/*.mat`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: village, forest, and mountain scenes share a deliberate material palette with clearer biome identity and fewer arbitrary placeholder colors.
  - Sprint detail: `docs/planning/sprint-05.md#eg-53-world-palette-and-material-cohesion`
- Story 17.2: lighting and atmosphere pass
  - Tracking ID: `EG-54`
  - Owner: `Content`, `Game.Editor`
  - Files: `Assets/Scenes/Bootstrap.unity`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: the key scenes have improved sky, camera framing, and lighting mood so the playable slice reads as one cohesive experience rather than raw editor default lighting.
  - Sprint detail: `docs/planning/sprint-05.md#eg-54-lighting-and-atmosphere-pass`
- Story 17.3: landmark silhouette pass
  - Tracking ID: `EG-55`
  - Owner: `Content`
  - Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
  - Acceptance: major landmarks and traversal anchors have stronger silhouette contrast and spacing so they are recognizable at a distance.
  - Sprint detail: `docs/planning/sprint-05.md#eg-55-landmark-silhouette-pass`

## Epic 18: Player-Facing Presentation
- Status: done
- Goal: improve the player-facing look of the selection and exploration moments without adding new systems
- Story 18.1: character select presentation refresh
  - Tracking ID: `EG-56`
  - Owner: `Game.UI`, `Game.Editor`
  - Files: `Assets/Game/UI/CharacterSelectionView.cs`, `Assets/Scenes/CharacterSelect.unity`
  - Acceptance: character select remains minimal but looks intentionally framed, readable, and consistent with the world presentation.
  - Sprint detail: `docs/planning/sprint-05.md#eg-56-character-select-presentation-refresh`
- Story 18.2: interaction prompt presentation polish
  - Tracking ID: `EG-57`
  - Owner: `Game.UI`, `Game.Interaction`
  - Files: `Assets/Game/UI/InteractionPromptLabel.cs`, `Assets/Scenes/WorldPersistent.unity`
  - Acceptance: the interaction prompt remains lightweight but is more legible and visually integrated with the new scene presentation.
  - Sprint detail: `docs/planning/sprint-05.md#eg-57-interaction-prompt-presentation-polish`
- Story 18.3: avatar and npc placeholder cleanup
  - Tracking ID: `EG-58`
  - Owner: `Content`, `Game.Player`, `Game.Interaction`
  - Files: `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Resources/Prefabs/NPCs/*.prefab`
  - Acceptance: placeholder characters and guide NPCs read more cleanly in silhouette, proportions, and materials without introducing final art dependencies.
  - Sprint detail: `docs/planning/sprint-05.md#eg-58-avatar-and-npc-placeholder-cleanup`

## Epic 19: Visual Verification
- Status: done
- Goal: make the presentation pass reviewable and stable
- Story 19.1: presentation review checklist
  - Tracking ID: `EG-59`
  - Owner: `Docs`, `Game.Editor`
  - Files: `docs/planning/sprint-05.md`
  - Acceptance: Sprint 05 defines a repeatable in-editor review path for checking palette, lighting, silhouettes, and UI readability across the connected exploration slice.
  - Sprint detail: `docs/planning/sprint-05.md#eg-59-presentation-review-checklist`
- Story 19.2: sprint 05 presentation asset sync
  - Tracking ID: `EG-60`
  - Owner: `Content`, `Game.Editor`
  - Files: `Assets/Scenes/*.unity`, `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Resources/Prefabs/Materials/*.mat`
  - Acceptance: the generated scenes, prefabs, and materials are regenerated from the Sprint 05 scaffold changes and checked in as the reviewed visual baseline.
  - Sprint detail: `docs/planning/sprint-05.md#eg-60-sprint-05-presentation-asset-sync`
- Story 19.3: repo ignore and starter asset cleanup
  - Tracking ID: `EG-61`
  - Owner: `Docs`, `ProjectSettings`
  - Files: `.gitignore`, `ProjectSettings/ProjectSettings.asset`, `Assets/Scenes/SampleScene.unity`, `Assets/Readme.asset`, `Assets/TutorialInfo/*`, `Assets/Settings/SampleSceneProfile.asset`
  - Acceptance: editor-local scene template noise is ignored, the default scene template points at the project bootstrap scene, and unused Unity starter assets are removed from the tracked repo.
  - Sprint detail: `docs/planning/sprint-05.md#eg-61-repo-ignore-and-starter-asset-cleanup`
- Story 19.4: sprint 05 material serialization sync
  - Tracking ID: `EG-62`
  - Owner: `Content`, `Game.Editor`
  - Files: `Assets/Resources/Prefabs/Materials/*.mat`
  - Acceptance: tracked presentation materials serialize the updated Sprint 05 palette consistently, including `_Color` normalization matching the reviewed baseline.
  - Sprint detail: `docs/planning/sprint-05.md#eg-62-sprint-05-material-serialization-sync`

## Epic 20: Codex Workflow Standardization
- Status: done
- Goal: align the repo's Codex metadata with the current platform-native skills, subagents, and command rules model
- Story 20.1: codex-native skills, subagents, and rules migration
  - Tracking ID: `EG-63`
  - Owner: `Docs`
  - Files: `AGENTS.md`, `.agents/skills/*/SKILL.md`, `.codex/agents/*.toml`, `.codex/rules/*.rules`, `docs/index.md`, `docs/ai-workflow.md`, `docs/task-skills.md`, `docs/agent-roles.md`, `docs/planning/README.md`, `docs/planning/sprint-06.md`
  - Acceptance: repo skills live under `.agents/skills/<skill>/SKILL.md` with YAML frontmatter, repo subagents live under `.codex/agents/*.toml`, repo command rules are versioned under `.codex/rules/*.rules`, and project docs route Codex to the new structure consistently.
  - Sprint detail: `docs/planning/sprint-06.md#eg-63-codex-native-skills-subagents-and-rules-migration`

## Epic 21: MVP Slice Hardening
- Status: in progress
- Goal: strengthen automated confidence in the current connected exploration slice without expanding v1 gameplay scope
- Story 21.1: connected-slice runtime regression coverage
  - Tracking ID: `EG-64`
  - Status: done
  - Owner: `Game.Tests.PlayMode`, `Game.Editor`
  - Files: `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`, `scripts/validate_unity_project.py`
  - Acceptance: PlayMode and repo validation cover startup, village interaction, village-to-forest traversal, forest interaction, forest-to-village return, post-return village interaction, and the scene markers that support the current route.
  - Sprint detail: `docs/planning/sprint-07.md#eg-64-connected-slice-runtime-regression-coverage`
- Story 21.2: validation scope cleanup and ci truthfulness follow-up
  - Tracking ID: `EG-65`
  - Status: ready
  - Owner: `Docs`, `CI`
  - Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-07.md`
  - Acceptance: workflow and sprint docs describe the current automated coverage honestly and name any remaining manual-only smoke path explicitly.
  - Sprint detail: `docs/planning/sprint-07.md#eg-65-validation-scope-cleanup-and-ci-truthfulness-follow-up`
