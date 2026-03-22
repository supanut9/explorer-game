# AI Workflow

## Workflow
- Open `docs/index.md` before editing code or creating tasks.
- Prefer editing one module at a time.
- Change contracts in `Game.Core` before changing feature modules.
- Add tests when introducing new behavior or state transitions.
- If a request conflicts with `docs/game-spec.md` or `docs/repo-standards.md`, update the docs in the same change.
- If the docs do not answer the task, pause and identify the missing decision before implementation.

## Common Task Patterns
- New player feature: update `Game.Player`, then expose only minimal shared contracts in `Game.Core`.
- New environment interaction: implement `IInteractable` or a specific derived pattern in `Game.Interaction`.
- New zone: add a `ZoneDefinition`, scene, and build settings entry through editor tooling.

## Avoid
- Hidden scene wiring with undocumented object name dependencies
- Large monolithic managers that mix input, UI, world state, and spawning
- Asset references embedded in many scene objects when one catalog can own them

## Documentation
- If an AI change introduces a new runtime contract or workflow, update `README.md` or one file in `docs/` in the same task.
- Do not leave code paths undocumented if they are intended for future AI editing.
- Keep `docs/index.md` current when adding or moving documentation.
