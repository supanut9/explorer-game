# Codex Instructions

## Goal
This repository is built for AI-assisted implementation of a small Unity exploration game. Work should stay modular, repeatable, and easy to validate.

## Source of truth
- Treat the checked-in docs as authoritative: `docs/game-spec.md`, `docs/repo-standards.md`, `docs/ai-workflow.md`, and `AGENTS.md`.
- Read `docs/index.md` before starting any task.
- Use `docs/decision-rules.md` when the docs do not fully answer a task.
- If code conflicts with the docs, change the docs deliberately rather than guessing.
- Do not implement behavior that is not described or implied by the docs without updating them in the same task.

## Operating rules
- Read the repo structure before editing.
- Prefer the smallest safe change that advances the task.
- Do not rewrite unrelated files.
- Do not depend on hidden scene state when a contract or config asset can own the behavior.
- Add docs when introducing a new workflow, rule, or runtime contract.
- Follow the repo docs before personal assumptions or generic defaults.
- Keep sprint implementation commits on the active sprint branch unless the docs explicitly require a separate feature branch.
- Do not open or merge a sprint PR without the required PR body metadata and GitHub PR metadata.

## Preferred workflow
1. Inspect current files and confirm the owning module.
2. Read the relevant docs and confirm the intended behavior.
3. Change the contract first if other modules depend on it.
4. Implement the feature in the nearest runtime module.
5. Add validation or tests.
6. Update the relevant docs if the task changed behavior, workflow, or ownership.
7. When preparing the sprint PR, include summary, linked `EG-*` ids, project card, target branch, scope, scenes or config assets touched, test evidence, sprint or milestone metadata, risks or notes, and a merge readiness checklist.
8. When preparing the sprint PR, set GitHub metadata for labels, project, milestone, and assignee before review or merge.

## Task ownership
- `Game.Core` for shared enums, constants, and session state.
- `Game.Player` for avatar selection, movement, and camera behavior.
- `Game.World` for zone loading and world-level runtime flow.
- `Game.Interaction` for prompts, inspectables, and dialogue targets.
- `Game.Animals` for ambient non-hostile behavior.
- `Game.UI` for menu and HUD wiring.
- `Game.Editor` for repeatable setup and validation tools.

## Safety rules
- Prefer explicit references over magic strings, except for stable scene-name contracts.
- Use editor tooling to generate or validate repeated setup.
- Keep AI-friendly boundaries: one feature, one owner, one validation path.
- If a task crosses modules, document the handoff in the task output.

## Default assumptions
- PC-first
- Third-person
- Placeholder-first
- Single-player
- Light interaction only
