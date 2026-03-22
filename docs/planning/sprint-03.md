# Sprint 03

## Sprint Goal
- Stabilize the first playable after Sprint 02 by repairing generated-scene camera state, tightening runtime input contracts, and adding regression coverage for the verified world loop.

## Committed Items
- Generated world camera and listener cleanup
- Input action configuration contract
- Placeholder player collision baseline
- First-playable regression coverage

## Stretch Items
- Player jump baseline
- Additional scene-authoring cleanup if new generated duplication appears outside `WorldPersistent`
- Unity validation expansion once the generated-scene baseline stops drifting

## Dependencies
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-02.md`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- `Assets/Game/World/WorldRuntimeController.cs`
- `Assets/Game/Player/ThirdPersonCameraRig.cs`

## Risks
- Generated scene repair can accidentally delete intended authored content if the cleanup rules are too broad.
- Input fixes can drift into undocumented control-policy changes if the supported configuration contract is not written down first.
- Play mode coverage may remain brittle if the tests depend on editor-generated assets without a stable setup path.
- Jump work should not destabilize the now-working move/look/interact baseline.

## Workflow
- Use `sprint/03` as the integration branch for Sprint 03.
- Checkout `main`.
- Create or checkout `sprint/03`.
- Commit Sprint 03 work directly on `sprint/03`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the Sprint 03 milestone once it exists.
- Open a final PR from `sprint/03` into `main`.
- Merge the sprint PR with a merge commit so Sprint 03 history is preserved on `main`.

## Review Notes
- Confirm the generated world scene is deterministic after multiple scaffold reruns.
- Confirm the rendered camera view and the movement reference stay aligned in Play Mode.
- Confirm any input-policy change is explicit in docs and in runtime behavior.
- Confirm new tests prove the repaired baseline instead of only mirroring manual steps.

## Outcome
- Status: done
- Verified in Unity after scaffold rerun:
  - one active world camera and one active audio listener
  - rendered view stays aligned with third-person movement reference
  - player spawn remains single-instance
  - reachable NPC interaction works in the generated village baseline
  - grounded jump works without regressing the repaired control loop

## Sprint Tasks

### EG-39 Generated World Camera And Listener Cleanup
- Story Order: `14.1`
- Status: done
- Owner: `Game.Editor`, `Game.World`, `Game.Player`
- Module: `Assets/Game/Editor`, `Assets/Game/World`, `Assets/Game/Player`
- Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`
- Goal: make the generated world scene converge to one camera rig and one active audio listener so the rendered view matches the control reference during play.
- Acceptance:
  - rerunning project scaffolding removes duplicated `World Root` and `ThirdPersonCameraRig` objects from `WorldPersistent`
  - the world path enters play with exactly one active `AudioListener`
  - the visible camera view rotates with the same camera reference used for third-person movement
  - no new compiler warnings are introduced by the scene cleanup path
- Subtasks:
  - repair root lookup so scaffold reruns update `World Root` instead of recreating it
  - dedupe world camera rigs and listeners both in generated scene repair and at runtime startup
  - retest the playable loop from `Bootstrap` after rerunning scaffolding
- Dependencies:
  - `EG-23`
  - `EG-32`
  - `EG-37`
- Notes:
  - the current uncommitted runtime/editor fixes on `sprint/03` belong to this task
  - the regression appeared only after Sprint 02 merged because the generated scene carried duplicated serialized camera state
  - verification confirmed that rerunning project scaffolding collapses the duplicated world roots and camera rigs back to one active runtime camera and one audio listener

### EG-40 Input Action Configuration Contract
- Story Order: `14.2`
- Status: done
- Owner: `Game.Player`, `Game.Interaction`, `Docs`
- Module: `Assets/Game/Player`, `Assets/Game/Interaction`, `docs`
- Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Player/ThirdPersonCameraRig.cs`, `Assets/Game/Interaction/InteractionProbe.cs`, `docs/repo-standards.md`
- Goal: define and implement the supported runtime contract for `InputActionProperty` configuration.
- Acceptance:
  - the repo explicitly documents whether inline `InputActionProperty.action` values are supported
  - runtime input code matches that documented policy
  - fallback desktop controls still work when no configured action asset is present
- Subtasks:
  - decide whether inline actions remain supported or are intentionally forbidden
  - update the three runtime input paths to match that decision
  - document the policy in repo standards
- Dependencies:
  - `EG-35`
- Notes:
  - this task comes directly from the Sprint 02 PR review finding
  - the runtime now accepts both valid inline `InputActionProperty.action` bindings and referenced action assets, while still falling back when serialized actions are empty

### EG-41 Placeholder Player Collision Baseline
- Story Order: `14.3`
- Status: done
- Owner: `Game.Editor`, `Game.Player`
- Module: `Assets/Game/Editor`, `Assets/Resources/Prefabs/Characters`
- Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Resources/Prefabs/Characters/MaleExplorer.prefab`, `Assets/Resources/Prefabs/Characters/FemaleExplorer.prefab`
- Goal: remove the accidental double-collision setup from generated placeholder player prefabs.
- Acceptance:
  - generated character prefabs keep one intended player collision shape
  - the visual child does not retain an extra primitive collider
  - moving, spawning, and interaction still work after prefab regeneration
- Subtasks:
  - inspect the generated placeholder character prefab structure
  - remove the visual-child primitive collider during scaffold generation
  - rerun scaffolding and verify the updated prefab baseline
- Dependencies:
  - `EG-20`
  - `EG-36`
- Notes:
  - this task also comes from the Sprint 02 PR review finding
  - generated placeholder character prefabs now converge to a single root `CharacterController` without a second collider on the visual child

### EG-42 First-Playable Regression Coverage
- Story Order: `14.4`
- Status: done
- Owner: `Game.Tests.PlayMode`, `Game.Editor`
- Module: `Assets/Game/Tests/PlayMode`, `Assets/Game/Editor`
- Files: `Assets/Game/Tests/PlayMode/*`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- Goal: add narrow regression coverage for the repaired first-playable path so camera/listener duplication and spawn regressions are caught earlier.
- Acceptance:
  - at least one test covers the one-player world entry invariant
  - at least one test covers the one-camera or one-audio-listener world startup invariant
  - at least one test covers placeholder interaction reachability or prompt activation
- Subtasks:
  - identify the smallest stable play-mode setup for generated world validation
  - add coverage for player spawn uniqueness and runtime camera ownership
  - add coverage for one reachable interaction in the generated world path
- Dependencies:
  - `EG-39`
  - `EG-40`
  - `EG-41`
- Notes:
  - prefer stable generated setup over scene-by-scene manual fixture authoring
  - coverage now targets the generated world path directly for one-player startup, one-audio-listener startup, and reachable NPC interaction

### EG-43 Player Jump Baseline
- Story Order: `14.5`
- Status: done
- Owner: `Game.Player`
- Module: `Assets/Game/Player`, `Assets/Game/Tests/PlayMode`
- Files: `Assets/Game/Player/ThirdPersonExplorerController.cs`, `Assets/Game/Tests/PlayMode/*`
- Goal: add a simple grounded jump so the controller baseline covers the next obvious missing player action.
- Acceptance:
  - the player can jump from grounded state with predictable height and landing
  - jump does not break sprint, camera-relative movement, or spawn stability
  - the behavior is covered by at least one targeted test or verification path
- Subtasks:
  - add jump input and grounded-jump velocity to the controller
  - retune gravity or grounding if needed for stable landing
  - add a focused validation path
- Dependencies:
  - `EG-39`
  - `EG-42`
- Notes:
  - this remains stretch until the camera/listener regression is closed
  - the controller now supports a grounded keyboard jump and includes a focused play mode test for lift-off from the grounded baseline

### EG-44 Sprint 03 Asset Sync And Noise Cleanup
- Story Order: `14.6`
- Status: done
- Owner: `Game.Player`, `Game.Tests.PlayMode`, `ProjectSettings`
- Module: `Assets/Resources/Prefabs/Characters`, `Assets/Game/Tests/PlayMode`, `ProjectSettings`
- Files: `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Game/Tests/PlayMode/*.meta`, `ProjectSettings/SceneTemplateSettings.json`
- Goal: close Sprint 03 with serialized prefab and test assets aligned to the runtime baseline while keeping editor-only scene-template noise out of the branch.
- Acceptance:
  - placeholder character prefabs serialize the jump baseline introduced by `EG-43`
  - new play mode tests have tracked Unity `.meta` files
  - editor-only `SceneTemplateSettings.json` is not carried into the sprint branch
- Subtasks:
  - stage the serialized prefab updates for the jump-enabled placeholder characters
  - track the new play mode test `.meta` files
  - remove the untracked scene-template settings file from the sprint branch
- Dependencies:
  - `EG-41`
  - `EG-42`
  - `EG-43`
- Notes:
  - this is a sprint closeout task, not a new runtime feature
