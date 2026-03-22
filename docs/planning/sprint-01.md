# Sprint 01

## Sprint Goal
- Make the project ready for the first playable exploration loop with clear AI ownership and repeatable setup.

## Committed Items
- Core runtime contracts
- Character select flow
- Third-person movement and camera
- World loading and spawn flow
- Light interaction prompt and placeholder NPCs
- Editor scaffolding and validation
- Baseline tests

## Stretch Items
- Additional placeholder content variants
- Extra world-zone polish
- Animal roaming placeholder behavior
- Selected character HUD label
- Repo Codex workflow assets

## Dependencies
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/decision-rules.md`
- `docs/content-plan.md`

## Risks
- Unity scene and prefab wiring may need manual verification in the editor.
- Content scope can expand if asset-level decisions are made too early.

## Workflow
- Use `sprint/01` as the integration branch for Sprint 01.
- Checkout `main`.
- Create or checkout `sprint/01`.
- Create one fresh `feature/<task-slug>` branch from `sprint/01`.
- Implement one task on that feature branch.
- Open a PR from the feature branch into `sprint/01`.
- Merge the PR into `sprint/01`.
- Stop using the merged feature branch.
- Create the next feature branch from the updated `sprint/01`.
- After all Sprint 01 tasks are merged, open a final PR from `sprint/01` into `main`.

## Review Notes
- Confirm the first playable loop is reachable from bootstrap to world.
- Confirm each module stayed inside its own file ownership.

## Sprint Tasks

### EG-1 Git Workflow Docs
- Story Order: `1.1`
- Status: done
- Owner: `Docs`
- Module: `Repository Docs`
- Files: `AGENTS.md`, `CONTRIBUTING.md`, `README.md`, `docs/repo-standards.md`, `docs/planning/README.md`, `docs/planning/sprint-01.md`
- Goal: document the sprint git workflow, `EG-*` tracking ids, and single-file Sprint 01 planning structure.
- Acceptance:
  - branch flow is consistent across repo docs
  - commit format uses stable `EG-*` ids
  - PR metadata is documented once and referenced consistently
  - Sprint 01 overview and task details live in one file
- Subtasks:
  - document sprint branch and feature branch flow
  - document commit and PR rules
  - assign stable `EG-*` ids in planning docs
  - consolidate Sprint 01 tasks into the sprint file
- Dependencies:
  - `docs/repo-standards.md`
- Notes:
  - docs-only workflow standardization task
### EG-2 Core Session
- Story Order: `2.1`
- Status: done
- Owner: `Game.Core`
- Module: `Assets/Game/Core`
- Files: `Assets/Game/Core/GameSession.cs`, `Assets/Game/Core/GameConstants.cs`
- Goal: persist the selected character and active zone across scene changes.
- Acceptance:
  - selected character persists after scene changes
  - active zone persists after scene changes
  - shared scene names remain stable
- Subtasks:
  - define stable session fields
  - wire scene persistence
  - verify state survives scene transitions
- Dependencies:
  - `docs/game-spec.md`
  - `docs/repo-standards.md`
- Notes:
  - shared contract for player and world flow

### EG-3 Catalog Validation
- Story Order: `2.2`
- Status: done
- Owner: `Game.Editor`
- Module: `Assets/Game/Editor`
- Files: `Assets/Game/Player/CharacterCatalog.cs`, `Assets/Game/World/WorldCatalog.cs`, `Assets/Game/Tests/EditMode/CatalogValidationTests.cs`
- Goal: validate generated catalogs before runtime work continues.
- Acceptance:
  - generated character catalog validates required entries
  - generated world catalog validates required zones
- Subtasks:
  - confirm required catalog fields
  - validate generated character entries
  - validate generated world zone entries
- Dependencies:
  - `docs/game-spec.md`
  - `docs/repo-standards.md`
- Notes:
  - keeps setup repeatable for AI and humans

### EG-4 Character Select
- Story Order: `3.1`
- Status: done
- Owner: `Game.UI`
- Module: `Assets/Game/UI`
- Files: `Assets/Game/UI/CharacterSelectionView.cs`, `Assets/Game/Tests/EditMode/CharacterSelectionViewTests.cs`
- Goal: let the player choose male or female and continue into the world flow.
- Acceptance:
  - male and female selection both persist to the session
  - selection transitions into the world flow
- Subtasks:
  - wire the selection buttons
  - store the selected character in session state
  - transition into the world scene flow
- Dependencies:
  - `docs/game-spec.md`
  - `docs/decision-rules.md`
  - `EG-2`
- Notes:
  - keep the UI minimal

### EG-5 Movement Camera
- Story Order: `3.2`
- Status: done
- Owner: `Game.Player`
- Module: `Assets/Game/Player`
- Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`
- Goal: provide third-person movement and camera follow behavior.
- Acceptance:
  - the player can walk, sprint, and look
  - the camera follows in third person
  - input is routed through the existing Input System asset
- Subtasks:
  - read move and look input
  - apply third-person movement
  - follow the player with the camera rig
- Dependencies:
  - `docs/game-spec.md`
  - `EG-2`
- Notes:
  - keep movement simple and PC-first

### EG-6 Zone Catalog
- Story Order: `4.1`
- Status: done
- Owner: `Game.World`
- Module: `Assets/Game/World`
- Files: `Assets/Game/World/ZoneDefinition.cs`, `Assets/Game/World/WorldCatalog.cs`, `Assets/Game/Tests/EditMode/WorldCatalogTests.cs`
- Goal: define the village, forest, and mountain zone contracts.
- Acceptance:
  - village, forest, and mountain zones resolve by stable scene name
  - zone definitions hold spawn and display metadata
- Subtasks:
  - define the zone asset fields
  - create the stable scene-name mapping
  - capture spawn metadata for each zone
- Dependencies:
  - `docs/game-spec.md`
  - `docs/content-decisions.md`
- Notes:
  - structural world decisions only

### EG-7 World Spawn
- Story Order: `4.2`
- Status: done
- Owner: `Game.World`
- Module: `Assets/Game/World`
- Files: `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/World/ZonePortal.cs`
- Goal: load the active zone and spawn the selected avatar there.
- Acceptance:
  - the selected avatar spawns in the active zone
  - zone loading is additive or otherwise predictable for the target flow
- Subtasks:
  - load the active zone
  - resolve the selected avatar prefab
  - spawn the avatar at the configured point
- Dependencies:
  - `EG-2`
  - `EG-6`
- Notes:
  - depends on stable scene-name contracts

### EG-8 Interaction Probe
- Story Order: `5.1`
- Status: done
- Owner: `Game.Interaction`
- Module: `Assets/Game/Interaction`
- Files: `Assets/Game/Interaction/InteractionProbe.cs`, `Assets/Game/UI/InteractionPromptLabel.cs`
- Goal: show prompts and trigger the nearest valid interactable.
- Acceptance:
  - the nearest valid interactable shows a prompt
  - the interactable can be triggered from the prompt flow
- Subtasks:
  - detect nearby interactables
  - select the nearest valid target
  - render the current prompt text
  - trigger the active target on input
- Dependencies:
  - `docs/game-spec.md`
  - `EG-2`
- Notes:
  - keep interactions light and non-hostile

### EG-9 NPC Inspectable
- Story Order: `5.2`
- Status: blocked
- Owner: `Game.Interaction`
- Module: `Assets/Game/Interaction`
- Files: `Assets/Game/Interaction/DialogueNpc.cs`, `Assets/Game/Interaction/InspectableObject.cs`
- Goal: provide placeholder NPC and inspectable behavior.
- Acceptance:
  - NPCs can show placeholder talk behavior
  - inspectables can show placeholder inspect behavior
- Subtasks:
  - define the NPC placeholder text
  - define the inspectable placeholder text
  - keep both behaviors non-system-heavy
- Dependencies:
  - `docs/game-spec.md`
  - `docs/content-decisions.md`
- Notes:
  - no quest or inventory systems
  - blocked until interaction runtime files are added to the repo

### EG-10 Editor Scaffold
- Story Order: `6.1`
- Status: done
- Owner: `Game.Editor`
- Module: `Assets/Game/Editor`
- Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- Goal: generate starter scenes, config assets, and build settings from one menu action.
- Acceptance:
  - one menu action generates the starter project scaffolding
  - generated assets and scenes validate cleanly
- Subtasks:
  - generate the starter scenes
  - generate or update config assets
  - set the build settings entries
  - run validation after generation
- Dependencies:
  - `docs/game-spec.md`
  - `docs/repo-standards.md`
- Notes:
  - reduces manual setup

### EG-11 Session Tests
- Story Order: `7.1`
- Status: done
- Owner: `Game.Tests.EditMode`
- Module: `Assets/Game/Tests/EditMode`
- Files: `Assets/Game/Tests/EditMode/GameSessionTests.cs`
- Goal: cover session persistence and character selection in edit mode tests.
- Acceptance:
  - session state persists as expected
  - character selection updates the stored selection
- Subtasks:
  - test default session creation
  - test character selection persistence
  - test reset behavior if needed
- Dependencies:
  - `EG-2`
- Notes:
  - keep the tests deterministic

### EG-12 Interaction Tests
- Story Order: `7.2`
- Status: blocked
- Owner: `Game.Tests.PlayMode`
- Module: `Assets/Game/Tests/PlayMode`
- Files: `Assets/Game/Tests/PlayMode/InteractionProbeTests.cs`
- Goal: cover nearest-target interaction in play mode tests.
- Acceptance:
  - the nearest interactable is detected
  - interactables can be triggered in play mode
- Subtasks:
  - build a simple interactable test fixture
  - verify nearest-target selection
  - verify the trigger path
- Dependencies:
  - `EG-8`
- Notes:
  - keep the test setup minimal
  - blocked until play mode interaction test files are added to the repo

### EG-13 Animal Roaming
- Story Order: `8.1`
- Status: done
- Owner: `Game.Animals`
- Module: `Assets/Game/Animals`
- Files: `Assets/Game/Animals/AnimalRoamingAgent.cs`, `Assets/Game/Animals/Game.Animals.asmdef`
- Goal: provide simple non-hostile roaming or idling behavior for animals.
- Acceptance:
  - animals roam or idle naturally
  - the behavior stays non-hostile
- Subtasks:
  - configure the roaming agent
  - keep movement localized around an origin area
  - avoid adding combat or quest systems
- Dependencies:
  - `docs/game-spec.md`
- Notes:
  - currently present in the workspace but not yet tracked

### EG-14 Selected Character Label
- Story Order: `8.2`
- Status: done
- Owner: `Game.UI`
- Module: `Assets/Game/UI`
- Files: `Assets/Game/UI/SelectedCharacterLabel.cs`
- Goal: reflect the active selected character in a simple on-screen label.
- Acceptance:
  - the current selected character is shown in a lightweight UI label
  - the label reads from session state without adding a larger UI system
- Subtasks:
  - bind a text label to the selected character
  - keep the display lightweight
- Dependencies:
  - `EG-2`
- Notes:
  - currently present in the workspace but not yet tracked

### EG-15 Codex Workflow Assets
- Story Order: `8.3`
- Status: done
- Owner: `Docs`
- Module: `.codex`
- Files: `.codex/instructions.md`, `.codex/agents/*.md`, `.codex/skills/*.md`
- Goal: keep repo-owned Codex instructions, role prompts, and task skills versioned with the project docs.
- Acceptance:
  - the repo-owned `.codex` instructions are committed
  - agent role prompts are committed for project reuse
  - skill docs are committed for sprint and task workflows
- Subtasks:
  - track repo-owned `.codex` instruction files
  - track role prompts under `.codex/agents`
  - track skill docs under `.codex/skills`
- Dependencies:
  - `EG-1`
- Notes:
  - commit only project-owned Codex assets, not personal machine config
