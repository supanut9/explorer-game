# Sprint 04

## Sprint Goal
- Turn the stable first playable into a readable multi-zone exploration slice by exposing traversal, improving world guidance, and verifying that exploration holds together after scene transitions.

## Committed Items
- Zone traversal exposure
- Guide NPC and signposting pass
- Zone-specific interaction copy
- Landmark and traversal dressing pass
- Multi-zone verification coverage

## Stretch Items
- Unity validation workflow implementation
- Additional content polish if traversal and verification finish early

## Dependencies
- `docs/game-spec.md`
- `docs/content-plan.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-03.md`
- `Assets/Game/World/WorldRuntimeController.cs`
- `Assets/Game/World/ZonePortal.cs`
- `Assets/Game/Interaction/DialogueNpc.cs`
- `Assets/Game/Interaction/InspectableObject.cs`

## Risks
- Traversal can become system-heavy if route guidance turns into an implicit quest framework.
- Scene dressing can hide traversal anchors or create collision issues if landmarks are added without play verification.
- Zone-specific interaction text can drift into content sprawl if each object is hand-authored without a small, repeatable content rule.
- Multi-zone verification may stay manual unless the traversal path is deterministic enough for focused play mode coverage.

## Workflow
- Use `sprint/04` as the integration branch for Sprint 04.
- Checkout `main`.
- Create or checkout `sprint/04`.
- Commit Sprint 04 work directly on `sprint/04`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the Sprint 04 milestone once it exists.
- Open a final PR from `sprint/04` into `main`.
- Merge the sprint PR with a merge commit so Sprint 04 history is preserved on `main`.

## Review Notes
- Confirm zone transitions are visible and intentional, not hidden behind editor-only knowledge.
- Confirm village guidance makes the next destination obvious without adding extra UI systems.
- Confirm different zones expose different placeholder interaction text.
- Confirm multi-zone traversal does not regress the stable camera, spawn, and interaction baseline from Sprint 03.

## Sprint Tasks

### EG-45 Zone Traversal Exposure
- Story Order: `15.1`
- Status: in progress
- Owner: `Game.World`, `Game.Editor`
- Module: `Assets/Game/World`, `Assets/Game/Editor`, `Assets/Scenes`
- Files: `Assets/Game/World/ZonePortal.cs`, `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: make at least one secondary zone intentionally reachable from the starting village and provide a stable way back.
- Acceptance:
  - the player can walk from `VillageZone` into at least one secondary zone using a visible traversal anchor
  - the arrival spawn in the destination zone is valid and readable
  - the player can return without breaking the current world session
- Subtasks:
  - audit the current zone portal flow against actual reachable scene content
  - place or repair visible traversal anchors in the generated scenes
  - verify arrival and return spawns after traversal
- Dependencies:
  - `EG-7`
  - `EG-39`
- Notes:
  - prioritize one polished secondary route over exposing every zone poorly
  - implementation is currently focused on a readable village-to-forest route with a stable return path

### EG-46 Guide NPC And Signposting Pass
- Story Order: `15.2`
- Status: in progress
- Owner: `Content`, `Game.Interaction`
- Module: `Assets/Scenes`, `Assets/Game/Interaction`
- Files: `Assets/Scenes/VillageZone.unity`, `Assets/Game/Interaction/DialogueNpc.cs`
- Goal: make the next exploration choice obvious in the village through light world guidance only.
- Acceptance:
  - the player can find a guide NPC near the start
  - the guide NPC text points toward available exploration routes
  - simple signs or landmarks reinforce the route choice without adding quest UI
- Subtasks:
  - place or tune the guide NPC so it is encountered early
  - add route-specific placeholder dialogue
  - add lightweight signposting in the village scene
- Dependencies:
  - `EG-45`
- Notes:
  - keep the guidance diegetic and lightweight
  - the current implementation targets a start-area guide NPC plus forest and mountain route signs in the village

### EG-47 Zone-Specific Interaction Copy
- Story Order: `15.3`
- Status: in progress
- Owner: `Game.Interaction`, `Content`
- Module: `Assets/Game/Interaction`, `Assets/Scenes`
- Files: `Assets/Game/Interaction/DialogueNpc.cs`, `Assets/Game/Interaction/InspectableObject.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: give each zone distinct placeholder interaction content so exploration exposes different observations rather than repeated generic output.
- Acceptance:
  - village, forest, and mountain interactions each have distinct placeholder text
  - NPC talk and inspectable text remain short and readable
  - no new dialogue framework or content pipeline is introduced
- Subtasks:
  - define a small placeholder copy set for each zone
  - wire NPCs and inspectables to the correct text
  - verify the content still works after traversal between zones
- Dependencies:
  - `EG-8`
  - `EG-9`
  - `EG-45`
- Notes:
  - prefer a few good lines per zone over broad content volume
  - current scope targets the existing village guide NPC plus the forest and mountain inspectable markers

### EG-48 Landmark And Traversal Dressing Pass
- Story Order: `15.4`
- Status: in progress
- Owner: `Content`
- Module: `Assets/Scenes`
- Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: improve navigation readability so players can identify paths, landmarks, and interesting points without extra HUD systems.
- Acceptance:
  - each zone has at least one strong landmark visible from normal traversal
  - major routes toward traversal anchors or interaction points are visually legible
  - added dressing does not block movement or interactables
- Subtasks:
  - identify weak navigation areas in the current scenes
  - add or reposition paths, rocks, trees, signs, or props to clarify routes
  - retest traversal and NPC access after dressing changes
- Dependencies:
  - `EG-21`
  - `EG-22`
  - `EG-45`
- Notes:
  - this is a readability pass, not a fidelity pass
  - the current slice focuses on stronger forest and mountain landmarks so traversal anchors and lookout paths are easier to spot from normal movement

### EG-49 Multi-Zone Verification Coverage
- Story Order: `15.5`
- Status: in progress
- Owner: `Game.Tests.PlayMode`, `Game.Editor`
- Module: `Assets/Game/Tests/PlayMode`, `docs/planning`
- Files: `Assets/Game/Tests/PlayMode/*`, `docs/planning/sprint-04.md`
- Goal: capture a repeatable validation path for the connected exploration slice.
- Acceptance:
  - the sprint docs define a traversal verification path from village to a secondary zone and back
  - at least one focused automated or manual check covers multi-zone traversal without regressing the current listener/camera baseline
  - the verification path includes interaction confirmation in both the starting and destination zones
- Subtasks:
  - define the smallest stable traversal verification path
  - add focused coverage where automation is reliable
  - document remaining manual checks honestly where automation is still weak
- Dependencies:
  - `EG-39`
  - `EG-42`
  - `EG-45`
  - `EG-47`
- Notes:
  - prioritize confidence in the connected slice over broad but brittle test coverage
  - the current verification path targets village startup, forest traversal, and destination-zone interaction after the scene transition

### EG-51 Animal Roaming NavMesh Fallback
- Story Order: `15.6`
- Status: in progress
- Owner: `Game.Animals`
- Module: `Assets/Game/Animals`
- Files: `Assets/Game/Animals/AnimalRoamingAgent.cs`
- Goal: keep passive animal behavior quiet and safe in zones that do not yet have a baked NavMesh.
- Acceptance:
  - entering a zone with roaming animals does not spam NavMeshAgent warnings
  - animals still idle safely when no valid NavMesh is available
  - connected exploration verification is not blocked on navigation bake setup
- Subtasks:
  - guard the roaming update loop against inactive or off-NavMesh agents
  - skip destination changes cleanly when sampling fails or no NavMesh is available
  - retest the forest traversal loop for warning-free runtime behavior
- Dependencies:
  - `EG-13`
  - `EG-45`
- Notes:
  - this is a stability fallback, not a full ambient-navigation polish pass

### EG-52 Sprint 04 Scene And Material Sync
- Story Order: `15.7`
- Status: in progress
- Owner: `Content`, `Game.Editor`, `ProjectSettings`
- Module: `Assets/Scenes`, `Assets/Resources/Prefabs/Materials`, `ProjectSettings`
- Files: `Assets/Scenes/*.unity`, `Assets/Resources/Prefabs/Materials/*.mat`, `ProjectSettings/SceneTemplateSettings.json`
- Goal: sync the generated scene and material outputs from the Sprint 04 traversal and readability work without carrying editor-only noise.
- Acceptance:
  - generated village, forest, and mountain scenes reflect the new traversal anchors, signs, and landmarks
  - newly created material assets for the Sprint 04 props are tracked
  - `SceneTemplateSettings.json` is not carried into the sprint branch
- Subtasks:
  - stage the scaffold-generated scene updates
  - track the new material assets created for traversal props and landmarks
  - remove the editor-only scene template settings file from the sprint branch
- Dependencies:
  - `EG-45`
  - `EG-46`
  - `EG-47`
  - `EG-48`
- Notes:
  - this is a serialized-asset sync task, not a new gameplay system

### EG-50 Unity Validation Workflow Implementation
- Story Order: `16.1`
- Status: planned
- Owner: `CI`
- Module: `.github/workflows`, `docs/planning`
- Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-04.md`
- Goal: convert the Sprint 02 validation plan into a real automated Unity-aware workflow once the connected exploration slice is stable.
- Acceptance:
  - GitHub Actions runs at least one Unity-aware validation path
  - the workflow scope and environment assumptions are documented
  - the workflow avoids claiming broader test coverage than it actually provides
- Subtasks:
  - define the smallest Unity-aware check worth automating first
  - implement the workflow and supporting documentation
  - connect the workflow to the sprint verification story
- Dependencies:
  - `EG-19`
  - `EG-49`
- Notes:
  - treat this as stretch unless the multi-zone slice stabilizes early
