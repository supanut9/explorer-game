# Explorer Game

Simple Unity 3D exploration game built for AI-assisted implementation.

## Overview
This project is a third-person, PC-first exploration game where the player selects `Male` or `Female` and explores natural zones with light interaction.

## Getting Started
1. Open the project in Unity `6000.4.0f1`.
2. Run `Tools/Explorer Game/Generate Project Scaffolding`.
3. Run `Tools/Explorer Game/Validate Config Assets`.
4. Replace placeholder prefabs and wire the starter scenes incrementally.

## Project Structure
- `Assets/Game/Core` - shared state, enums, and constants
- `Assets/Game/Player` - avatar selection, movement, and camera
- `Assets/Game/World` - zone catalogs and scene loading
- `Assets/Game/Interaction` - prompts, inspectables, and dialogue targets
- `Assets/Game/Animals` - simple roaming behavior
- `Assets/Game/UI` - menus and HUD binders
- `Assets/Game/Editor` - repeatable setup and validation tools
- `Assets/Game/Tests` - edit mode and play mode tests
- `Assets/Scenes` - Unity scene assets
- `Assets/Resources/Configs` - shared config assets

## Documentation
Use the docs as the source of truth for implementation decisions:
- [docs/index.md](docs/index.md)
- [CONTRIBUTING.md](CONTRIBUTING.md)
- [AGENTS.md](AGENTS.md)
- [LICENSE](LICENSE)
- [docs/game-spec.md](docs/game-spec.md)
- [docs/repo-standards.md](docs/repo-standards.md)
- [docs/ai-workflow.md](docs/ai-workflow.md)

## Notes
- Unity editor: `6000.4.0f1`
- Render pipeline: URP
- Input stack: Unity Input System
- Scope: single-player, third-person, placeholder-first
