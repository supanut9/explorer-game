# Repository Guidelines

## Codex Workflow
Codex reads `AGENTS.md` before doing work, so this file is the repository policy layer for AI behavior.

- Read `docs/index.md` before changing code, docs, or planning files.
- Treat `docs/game-spec.md`, `docs/repo-standards.md`, and `docs/ai-workflow.md` as the source of truth.
- Use the matching repo skill from `.agents/skills/*/SKILL.md` before substantial work in that task family.
- Use the smallest matching skill. If a task crosses domains, choose one primary skill and keep any handoff explicit.
- Use repo subagents from `.codex/agents/*.toml` only for bounded delegated work. Keep one primary owner per task.

## Skill Routing
- Planning and "what next" requests: use `sprint-planning`.
- Backlog shaping, new `EG-*` work, or task splitting: use `backlog-refinement`.
- Repo, workflow, or planning doc updates: use `docs-maintenance`.
- Bootstrap, scene generation, config creation, or validation tooling: use `unity-bootstrap`.
- Player control, camera, selection, or input work: use `player-controller`.
- Zone, travel, scene loading, spawn, or world catalog work: use `world-zone`.
- Interaction prompt, inspectable, or NPC placeholder work: use `interaction`.
- Character select, HUD labels, or prompt presentation work: use `ui-flow`.
- Edit mode or play mode coverage work: use `testing`.

## Subagent Routing
- `runtime_implementer`: gameplay code in runtime modules.
- `editor_tooling`: editor utilities, validation, scaffold generation, and setup automation.
- `test_writer`: edit mode and play mode tests, regression coverage, and validation checks.
- `doc_maintainer`: repository docs, workflow notes, skills, and agent metadata.
- `game_designer`: structural design decisions and game-spec-level tradeoffs.
- `content_designer`: asset-level content lists, variants, and content-pass planning.

## Project Structure
This Unity `6000.4.0f1` project uses URP. Runtime code lives in `Assets/Game`, split by feature: `Core`, `Player`, `World`, `Interaction`, `Animals`, `UI`, and `Editor`. Tests live under `Assets/Game/Tests` in `EditMode` and `PlayMode`. Scenes belong in `Assets/Scenes`, and shared config assets belong in `Assets/Resources/Configs`.

## Build and Development
Use the Unity Editor for day-to-day work:
- `Tools/Explorer Game/Generate Project Scaffolding` creates the starter scenes, config assets, and build settings.
- `Tools/Explorer Game/Validate Config Assets` checks the generated character and world catalogs.
- `Tools/Explorer Game/Validate Generated Scenes` checks the generated bootstrap, world, and interaction scene wiring.
- Unity Test Runner runs `EditMode` and `PlayMode` tests from `Assets/Game/Tests`.

## Command Rules
- Safe repo inspection commands such as `git status`, `git diff`, `git branch`, and `git log` are expected parts of normal work.
- Prompt before remote or write operations such as `git push`, `git pull`, `git fetch`, `gh pr create`, or `gh pr edit`.
- Do not use destructive cleanup commands such as `git reset --hard`, `git checkout --`, `git clean -fd`, or `rm -rf`.

## Coding Style
Use C# with 4-space indentation and ASCII-only identifiers unless a file already uses otherwise. Keep one responsibility per `MonoBehaviour`, put shared contracts in `Game.Core`, and prefer ScriptableObject-backed configuration over hidden Inspector state. Use PascalCase for types, methods, and public members; use camelCase for private fields, with `[SerializeField]` when Inspector access is needed.

## Testing
Add tests when changing scene flow, spawning, input, or interaction behavior. Keep tests narrow and deterministic. Name tests after the behavior they validate, for example `GameSessionTests` or `InteractionProbeTests`. Prefer edit mode tests for pure logic and play mode tests for scene/runtime behavior.

## Commits and Pull Requests
Start sprint work from `main`, then create or checkout the sprint branch, such as `sprint/01`. Commit sprint work directly on that sprint branch unless a separate feature branch is explicitly needed. Finish the sprint by opening a pull request from the sprint branch into `main`.

Use Conventional Commits-style messages for every commit: `type(scope): [EG-id] subject`. Supported types are `feat`, `fix`, `refactor`, `docs`, `chore`, `test`, `style`, `perf`, `build`, and `ci`. Use the stable backlog tracking id in the bracket tag, such as `[EG-1]`, and keep story order like `2.1` only in planning docs. If a change does not yet have an `EG-*` id, add it to the backlog first. Keep the subject short, imperative, lowercase, and without a trailing period. Example commits: `docs(repo): [EG-1] standardize git workflow`, `feat(core): [EG-2] restore shared session state`, `test(tests): [EG-11] cover session persistence`.

Pull requests should include summary, linked `EG-*` tracking ids, linked project card, target branch, touched modules, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, risks or notes, and a merge readiness checklist. Set GitHub PR metadata for labels, project, milestone, and assignee before review or merge. Merge sprint PRs into `main` with a merge commit so the sprint commit history stays preserved, then delete the sprint branch after merge. Include screenshots or a short video for UI or gameplay changes.

## Working With This Repo
Start with `docs/index.md`, then follow `docs/game-spec.md`, `docs/repo-standards.md`, `docs/ai-workflow.md`, and `AGENTS.md`. If code and docs differ, update the docs in the same change. Keep edits small and avoid broad refactors unless they are required for the task. AI should treat the docs as the source of truth.
