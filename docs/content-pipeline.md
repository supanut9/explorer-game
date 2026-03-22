# Content Pipeline

## Placeholder-First Policy
- Use placeholder meshes, materials, and animations until the gameplay loop is stable.
- Keep placeholder content grouped by function: player, NPC, animals, props, environment.
- Replace content by updating catalog assets or prefabs, not by rewriting feature code.

## Required Content Groups
- Male avatar prefab
- Female avatar prefab
- NPC placeholder prefab
- Animal placeholder prefabs
- Trees, rocks, houses, terrain materials

## Replacement Rule
- Preserve prefab names and config references when swapping assets where possible.
- If a replacement changes capabilities, update the owning module docs and tests in the same change.
