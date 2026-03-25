# Sprint 09

## Sprint Goal
- Make the existing world feel larger and naturally bounded by reworking zone framing, edge treatment, and traversal anchor layout without adding new gameplay systems.

## Committed Items
- World scale and framing pass
- Boundary treatment pass
- Portal and spawn layout cleanup

## Stretch Items
- None. Keep Sprint 09 focused on layout readability and route cleanup only.

## Dependencies
- `docs/game-spec.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-04.md`
- `docs/planning/sprint-08.md`
- `docs/content-plan.md`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- `Assets/Game/World/ZonePortal.cs`
- `Assets/Game/World/WorldRuntimeController.cs`
- `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`
- `Assets/Scenes/VillageZone.unity`
- `Assets/Scenes/ForestZone.unity`
- `Assets/Scenes/MountainZone.unity`

## Risks
- World-scale work can drift into broad scene-decoration or final-art production instead of fixing layout readability.
- Making zones feel larger by adding empty travel distance can make the slice slower without making it clearer.
- Boundary work can regress stable traversal if blockers interfere with the current village, forest, or mountain routes.
- Portal cleanup can create a new readability problem if travel anchors become hidden instead of naturally framed.
- Test coverage can become brittle if it depends on exact scene positions rather than stable route outcomes.

## Workflow
- Use `sprint/09` as the integration branch for Sprint 09.
- Checkout `main`.
- Create or checkout `sprint/09`.
- Commit Sprint 09 work directly on `sprint/09`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the `MVP Foundation` milestone.
- Open a final PR from `sprint/09` into `main`.
- Merge the sprint PR with a merge commit so Sprint 09 history is preserved on `main`.

## Review Notes
- Confirm each zone feels intentionally routed, with a stronger foreground, midground, and landmark read from common player approaches.
- Confirm zone edges read as natural boundaries rather than exposed map ends or invisible-wall space.
- Confirm village, forest, and mountain transitions still feel obvious to find while no longer reading as dropped portal props.
- Confirm one spawned player and one active audio listener survive startup and every supported zone transition after layout changes.

## Sprint Tasks

### EG-69 World Scale And Framing Pass
- Story Order: `23.1`
- Status: done
- Owner: `Game.World`, `Content`
- Module: `Assets/Game/World`, `Assets/Scenes`
- Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: reshape the three existing zones so they read as larger places through clearer route hierarchy, sightlines, and landmark spacing.
- Acceptance:
  - each zone presents a clearer approach path, destination landmark, and mid-route framing so travel reads as intentional instead of compact or flat
  - landmark and prop spacing improve the sense of scale without introducing new zones, quests, or traversal mechanics
  - the pass keeps the current three-zone loop readable and does not depend on final-art assets
- Subtasks:
  - widen or reorder key sightlines so the player sees a stronger route progression before reaching zone edges
  - rebalance landmark spacing and supporting props so routes read as longer without becoming empty
  - keep the pass grounded in generated scaffolding ownership instead of hand-authored scene drift
- Dependencies:
  - `EG-66`
  - `EG-68`
- Notes:
  - treat readability as the goal, not raw world size
  - avoid adding extra interaction points during this pass

### EG-70 Boundary Treatment Pass
- Story Order: `23.2`
- Status: done
- Owner: `Content`, `Game.World`
- Module: `Assets/Scenes`, `Assets/Game/Editor`
- Files: `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: remove the exposed-edge feeling by giving every playable boundary a believable physical limit that fits the natural-world spec.
- Acceptance:
  - common player-facing edges are framed by cliffs, terrain, vegetation, fences, rocks, or similar blockers that feel intentional in context
  - the player no longer reads the current boundaries as obvious map ends or accidental falloff space during normal exploration
  - boundary treatment stays placeholder-first and does not expand into final-art polish or new traversal rules
- Subtasks:
  - identify the most visible edge breaks in village, forest, and mountain play space
  - add or reposition terrain and prop blockers to reinforce those edges without trapping the supported route
  - keep blockers readable from approach distance so the player understands the world limit before touching it
- Dependencies:
  - `EG-69`
- Notes:
  - prioritize the most immersion-breaking edges first
  - prefer diegetic blockers over invisible constraints

### EG-71 Portal And Spawn Layout Cleanup
- Story Order: `23.3`
- Status: in progress
- Owner: `Game.World`, `Game.Editor`
- Module: `Assets/Game/World`, `Assets/Game/Editor`, `Assets/Game/Tests/PlayMode`
- Files: `Assets/Game/World/ZonePortal.cs`, `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`, `Assets/Game/Tests/PlayMode/WorldPlayableBaselineTests.cs`
- Goal: embed traversal anchors and spawn points into route landmarks so zone changes feel like natural movement through the world instead of stepping onto isolated portal placements.
- Acceptance:
  - traversal anchors align with path ends, gates, trailheads, or comparable landmarks that make the transition readable before interaction
  - zone spawn points land the player in believable continuation space instead of exposing the underlying portal contract
  - the supported village, forest, and mountain traversal loop remains stable under automated or scripted verification
- Subtasks:
  - review current entry and return anchor placement in all supported zones
  - move or disguise traversal anchors so they fit route geometry and landmark framing
  - update verification only where the supported route contract changes, keeping assertions centered on outcomes
- Dependencies:
  - `EG-69`
  - `EG-70`
  - `EG-67`
- Notes:
  - do not hide portals so aggressively that route discovery becomes confusing
  - preserve the current one-player and one-audio-listener invariants

## Manual Smoke Path
- Open `Bootstrap`, move through `CharacterSelect`, and enter the world.
- Walk the main village route and confirm the starting space reads larger and more directed than the Sprint 08 baseline.
- Probe the most obvious former boundary edges in village, forest, and mountain to confirm they now read as natural limits before contact.
- Travel village -> forest -> village and village -> mountain -> village and confirm each transition anchor feels embedded in the environment.
- Confirm one active audio listener and one spawned player remain present after each supported transition.

## Validation Scope
- Expected automated target for Sprint 09:
  - `python3 scripts/validate_unity_project.py`
  - `unity-project-sanity` GitHub Actions job
  - `ExplorerGame.Tests.PlayMode.WorldPlayableBaselineTests`
- Still manual until Sprint 09 layout work lands:
  - comparative readability check against the current Sprint 08 world layout
  - edge-probing smoke pass for visible boundaries in all three zones
