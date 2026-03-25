# Sprint 08

## Sprint Goal
- Complete the v1 three-zone world by opening the mountain route with stable traversal, minimal placeholder content, and honest verification coverage.

## Committed Items
- Mountain route traversal exposure
- Mountain slice verification coverage

## Stretch Items
- Mountain placeholder content boundary pass

## Dependencies
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-04.md`
- `docs/planning/sprint-07.md`
- `Assets/Game/World/ZonePortal.cs`
- `Assets/Game/World/WorldRuntimeController.cs`
- `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- `Assets/Scenes/VillageZone.unity`
- `Assets/Scenes/MountainZone.unity`

## Risks
- Mountain-route work can sprawl into content polish or final-art cleanup instead of finishing the traversal contract.
- Extending connected-slice verification can become brittle if assertions depend on scene layout details instead of stable outcomes.
- Opening the mountain route can regress the already-supported village -> forest -> village path if portal ownership or spawn state is not kept narrow.

## Workflow
- Use `sprint/08` as the integration branch for Sprint 08.
- Checkout `main`.
- Create or checkout `sprint/08`.
- Commit Sprint 08 work directly on `sprint/08`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the `MVP Foundation` milestone.
- Open a final PR from `sprint/08` into `main`.
- Merge the sprint PR with a merge commit so Sprint 08 history is preserved on `main`.

## Review Notes
- Confirm the village -> mountain -> village loop becomes a supported path without regressing the existing village -> forest -> village loop.
- Confirm one player and one active audio listener survive startup and mountain traversal transitions.
- Confirm at least one mountain interaction target is reachable after traversal and again after returning to the village.
- Confirm mountain presentation stays placeholder-first and does not expand into new systems, quests, combat, or final-art expectations.

## Sprint Tasks

### EG-66 Mountain Route Traversal Exposure
- Story Order: `22.1`
- Status: done
- Owner: `Game.World`, `Game.Editor`
- Module: `Assets/Game/World`, `Assets/Game/Editor`
- Files: `Assets/Game/World/ZonePortal.cs`, `Assets/Game/World/WorldRuntimeController.cs`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: make `MountainZone` a supported connected destination in the v1 world flow instead of a signposted-but-closed stub.
- Acceptance:
  - the player can intentionally travel from `VillageZone` into `MountainZone` and return through visible, reachable traversal anchors
  - the route keeps startup and scene-transition invariants aligned with the hardened connected slice
  - the change does not regress the existing village -> forest -> village route
- Subtasks:
  - replace or rewire the current closed mountain-route marker into a real traversal contract
  - confirm `MountainZone` spawn and return anchors are stable after additive load or unload transitions
  - keep the portal or spawn changes narrow to world-flow ownership, not unrelated scene cleanup
- Dependencies:
  - `EG-45`
  - `EG-64`
- Notes:
  - preserve the existing village guidance flow where possible
  - do not add new traversal UI or quest systems in this sprint

### EG-67 Mountain Slice Verification Coverage
- Story Order: `22.2`
- Status: done
- Owner: `Game.Tests.PlayMode`, `Docs`
- Module: `Assets/Game/Tests/PlayMode`, `docs/planning`
- Files: `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`, `docs/planning/sprint-08.md`
- Goal: extend the connected-slice verification contract so the mountain route is covered with the same level of honesty used in Sprint 07.
- Acceptance:
  - the verification path covers startup, village interaction, village-to-mountain traversal, mountain interaction, mountain-to-village return, and post-return village stability
  - the path verifies one spawned player and one active audio listener at startup and after each mountain scene transition
  - the sprint doc names any remaining manual-only mountain smoke path explicitly if automation still falls short
- Subtasks:
  - extend the baseline PlayMode path or add a focused companion path for mountain traversal and return
  - keep assertions centered on stable route outcomes rather than incidental transform positions
  - update Sprint 08 validation notes to distinguish automated versus manual mountain coverage
- Dependencies:
  - `EG-66`
  - `EG-65`
- Notes:
  - keep the forest path stable while adding mountain coverage
  - do not claim automated mountain support until the runtime route is actually enabled
  - prefer bounded state polling for zone transitions and interaction targets instead of fixed frame-count waits

### EG-68 Mountain Placeholder Content Boundary Pass
- Story Order: `22.3`
- Status: done
- Owner: `Content`, `Game.Interaction`
- Module: `Assets/Scenes`, `docs`
- Files: `Assets/Scenes/MountainZone.unity`, `docs/content-plan.md`, `docs/planning/sprint-08.md`
- Goal: give `MountainZone` the minimum placeholder landmarks and interaction touchpoint needed to read as a real v1 destination without turning Sprint 08 into a broad content sprint.
- Acceptance:
  - the mountain zone contains a minimal readable set of placeholder landmarks and route framing that support the new traversal path
  - the zone exposes at least one lightweight interaction touchpoint, such as an inspectable or placeholder NPC, that fits the existing interaction rules
  - Sprint 08 keeps any further mountain content explicitly deferred instead of hiding it inside committed scope
- Subtasks:
  - define the minimum mountain content set required for v1 readability
  - add or confirm one interaction touchpoint that can support the mountain verification path
  - record which mountain content remains intentionally deferred after this sprint
- Dependencies:
  - `EG-66`
- Notes:
  - keep the mountain interaction surface to a single inspectable in this sprint
  - stay placeholder-first and avoid final-art polish

## Manual Smoke Path
- Open `Bootstrap`, move through `CharacterSelect`, and enter the world.
- Interact with the village guide NPC before taking the mountain route.
- Travel village -> mountain and confirm one active audio listener and one spawned player remain present after the transition.
- Trigger the mountain interaction touchpoint.
- Travel mountain -> village and confirm the village guide NPC still works after the return transition.
- Re-run the existing village -> forest -> village smoke path if Sprint 08 touches shared portal or spawn flow.

## Validation Scope
- Expected automated target for Sprint 08:
  - `python3 scripts/validate_unity_project.py`
  - `unity-project-sanity` GitHub Actions job
  - mountain PlayMode coverage when the route is implemented and Unity license secrets are configured
- Still manual until Sprint 08 work lands:
  - in-editor smoke pass of the village -> mountain -> village route
  - regression smoke pass of the existing village -> forest -> village route after mountain-route changes
