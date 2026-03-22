# Game Spec

## Game summary
A simple 3D exploration game where the player selects `Male` or `Female`, then explores a natural environment with trees, animals, mountains, houses, and NPCs.

## V1 scope
- Third-person controller on PC.
- Two avatar choices only: male and female.
- One persistent flow with multiple zones.
- Light interaction only: talk, inspect, observe.
- Placeholder assets are acceptable for the first playable pass.

## Out of scope for v1
- Combat
- Inventory
- Quest chains
- Combat stats or leveling
- Multiplayer
- Complex character customization

## World structure
- `VillageZone` is the starting area.
- `ForestZone` is a connected exploration area.
- `MountainZone` is a connected exploration area.
- The world should feel natural, not system-heavy.

## Interaction rules
- NPCs should present a simple talk prompt and dialogue placeholder.
- Objects can be inspectable with a short description.
- Animals should move or idle naturally, but remain non-hostile.

## UI rules
- Character select should be minimal and obvious.
- On-screen prompts should be lightweight.
- The game should not introduce extra UI systems until the core loop is stable.

## Technical assumptions
- Unity `6000.4.0f1`
- URP
- Input System
- Single-player only
- Code-driven bootstrap and repeatable editor tooling
