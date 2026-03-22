# Architecture

## Runtime flow
- `GameSession` is the only persistent runtime state holder.
- `BootstrapFlowController` ensures the session exists and routes the player into `CharacterSelect`.
- `CharacterSelectionView` stores the selected character and loads `WorldPersistent`.
- Zone scenes are identified by stable scene-name contracts in `GameConstants`.

## Design rules
- Shared identifiers live in `Game.Core`.
- Systems read content from catalogs instead of embedding scene-specific assumptions.
- Scene assembly should be reproducible through editor tooling in `Game.Editor`.
- Runtime modules should not reference editor-only APIs.

## World shape
- `VillageZone` is the initial playable area.
- `ForestZone` and `MountainZone` are connected follow-on zones.
- Interaction is intentionally shallow in v1: inspect, talk, observe.

## AI editing guardrails
- Add new systems in the nearest module instead of growing a generic utils folder.
- Do not couple UI logic directly to scene objects when a config or runtime state object can own the decision.
- Keep scene names and content IDs stable once referenced by code.
