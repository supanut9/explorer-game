# Repository Guidelines

## Project Structure
This Unity `6000.4.0f1` project uses URP. Runtime code lives in `Assets/Game`, split by feature: `Core`, `Player`, `World`, `Interaction`, `Animals`, `UI`, and `Editor`. Tests live under `Assets/Game/Tests` in `EditMode` and `PlayMode`. Scenes belong in `Assets/Scenes`, and shared config assets belong in `Assets/Resources/Configs`.

## Build and Development
Use the Unity Editor for day-to-day work:
- `Tools/Explorer Game/Generate Project Scaffolding` creates the starter scenes, config assets, and build settings.
- `Tools/Explorer Game/Validate Config Assets` checks the generated character and world catalogs.
- Unity Test Runner runs `EditMode` and `PlayMode` tests from `Assets/Game/Tests`.

## Coding Style
Use C# with 4-space indentation and ASCII-only identifiers unless a file already uses otherwise. Keep one responsibility per `MonoBehaviour`, put shared contracts in `Game.Core`, and prefer ScriptableObject-backed configuration over hidden Inspector state. Use PascalCase for types, methods, and public members; use camelCase for private fields, with `[SerializeField]` when Inspector access is needed.

## Testing
Add tests when changing scene flow, spawning, input, or interaction behavior. Keep tests narrow and deterministic. Name tests after the behavior they validate, for example `GameSessionTests` or `InteractionProbeTests`. Prefer edit mode tests for pure logic and play mode tests for scene/runtime behavior.

## Commits and Pull Requests
Start sprint work from `main`, then create or checkout the sprint branch, such as `sprint/01`. Create one fresh `feature/<task-slug>` branch at a time from the current sprint branch, open a pull request back into the sprint branch, then stop using that feature branch after merge. Finish the sprint by opening a pull request from the sprint branch into `main`.

Use Conventional Commits-style messages for every commit: `type(scope): [EG-id] subject`. Supported types are `feat`, `fix`, `refactor`, `docs`, `chore`, `test`, `style`, `perf`, `build`, and `ci`. Use the stable backlog tracking id in the bracket tag, such as `[EG-1]`, and keep story order like `2.1` only in planning docs. If a change does not yet have an `EG-*` id, add it to the backlog first. Keep the subject short, imperative, lowercase, and without a trailing period. Example commits: `docs(repo): [EG-1] standardize git workflow`, `feat(core): [EG-2] restore shared session state`, `test(tests): [EG-11] cover session persistence`.

Pull requests should include summary, linked `EG-*` tracking id, linked project card, target branch, touched modules, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, and a merge readiness checklist. Include screenshots or a short video for UI or gameplay changes.

## Working With This Repo
Start with `docs/index.md`, then follow `docs/game-spec.md`, `docs/repo-standards.md`, `docs/ai-workflow.md`, and `AGENTS.md`. If code and docs differ, update the docs in the same change. Keep edits small and avoid broad refactors unless they are required for the task. AI should treat the docs as the source of truth.
