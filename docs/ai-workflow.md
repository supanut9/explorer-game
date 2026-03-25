# AI Workflow

## Workflow
- Open `docs/index.md` before editing code or creating tasks.
- Read `AGENTS.md` for repo-level Codex routing, skill selection, subagent usage, and command policy.
- Prefer editing one module at a time.
- Change contracts in `Game.Core` before changing feature modules.
- Add tests when introducing new behavior or state transitions.
- If a request conflicts with `docs/game-spec.md` or `docs/repo-standards.md`, update the docs in the same change.
- If the docs do not answer the task, pause and identify the missing decision before implementation.

## Codex Metadata
- Repo skills live in `.agents/skills/<skill-name>/SKILL.md` and should use YAML frontmatter with `name` and `description`.
- Repo subagents live in `.codex/agents/*.toml` and should define `name`, `description`, and `developer_instructions`.
- Repo command approval policy lives in `.codex/rules/*.rules`.
- Keep `AGENTS.md` as the policy layer that tells Codex when to use the skills and subagents above.

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
