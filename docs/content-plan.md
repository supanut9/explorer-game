# Content Plan

Use this file for concrete content lists once a content pass is active.

## Pass
- Name: V1 Exploration Content Pass
- Goal: create a placeholder-first natural world for the first playable exploration build
- Milestone: MVP Foundation

## Content Areas
- Animals: deer, birds, rabbits
- Trees / Plants: broadleaf trees, pine trees, shrubs, grass
- Houses: village houses, small cabins
- Rocks / Terrain: rocks, cliffs, terrain patches, paths
- NPCs: villagers, simple guide NPCs
- Props: fences, benches, lamps, signs, crates, barrels

## Priority Order
- 1. Player and NPC placeholder prefabs
- 2. Village houses and paths
- 3. Trees, rocks, and terrain dressing
- 4. Passive animals
- 5. Extra props and decoration
- 6. Variant pass for visual polish

## Asset Notes
- Keep all assets placeholder-first until the gameplay loop is stable.
- Preserve prefab names and catalog references when swapping in final art.
- Use grouped content sets so runtime logic does not depend on individual species names.

## Dependencies
- `docs/game-spec.md`
- `docs/content-decisions.md`
- `docs/content-pipeline.md`
- `docs/planning/backlog.md`

## Status
- baseline established in Sprint 02
- readability and connected-exploration follow-up planned for Sprint 04
- visual identity and presentation follow-up planned for Sprint 05

## MountainZone Minimum Set
- Keep `MountainGround`, `MountainPath`, and `LookoutPath` as the readable traversal line.
- Keep `CliffA`, `CliffB`, `LookoutStone`, `MountainBeacon`, and `MountainBeaconTop` as the minimal landmark frame for the zone.
- Keep one lightweight interaction touchpoint only: `MountainMarker` as the lookout inspectable.
- Use simple route-framing props only where needed to clarify the return anchor or lookout edge.

## MountainZone Deferred
- Animals or any NavMesh-dependent ambient behavior
- Cabins, houses, or settlement dressing
- Additional mountain NPCs or extra dialogue beats
- Extra portal routes beyond the village entry and village return anchors
- Decorative prop sweeps or final-art polish beyond readability
