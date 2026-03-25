# Agent Roles

## Purpose
This file defines execution roles for bounded work. The canonical subagent configs live in `.codex/agents/*.toml`, and `AGENTS.md` describes when to use them. Roles describe what an agent can touch, not the whole workflow.

## Runtime Roles
- `runtime-implementer` - writes gameplay code in runtime modules.
  - Can touch `Game.Core`, `Game.Player`, `Game.World`, `Game.Interaction`, `Game.Animals`, and `Game.UI`.
  - Should not modify docs unless a runtime contract changes.

## Editor Roles
- `editor-tooling` - writes editor utilities, bootstrap scripts, validation menus, and setup automation.
  - Can touch `Game.Editor` and related config scaffolding.
  - Should not add gameplay logic unless required for bootstrap.

## Testing Roles
- `test-writer` - adds or updates edit mode and play mode tests.
  - Focuses on behavior checks, validation, and regression coverage.
  - Should not broaden scope into feature work unless needed to make a test meaningful.

## Docs Roles
- `doc-maintainer` - updates repository docs, agent guidance, and workflow notes.
  - Keeps instructions short, explicit, and aligned with current code.
  - Should not make unrelated code changes.

## Design Roles
- `game-designer` - defines object, zone, and interaction design at the structure level.
  - Owns `docs/game-spec.md`, `docs/content-decisions.md`, and content-related planning notes.
  - Decides what kinds of objects exist, what they do, and what must be decided now versus later.
  - Should not implement runtime logic or create art assets directly.

- `content-designer` - defines asset-level content lists and object variants.
  - Owns `docs/content-decisions.md`, `docs/content-pipeline.md`, and content planning notes.
  - Decides which animals, trees, houses, props, and NPC variants should exist for a content pass.
  - Should not implement runtime logic or produce final art assets.

## Role Rules
- Assign one primary role per task.
- Use a secondary role only when the handoff is explicit.
- Keep file ownership narrow so tasks stay safe for parallel work.

## Subagent Locations
- `runtime_implementer`: `.codex/agents/runtime-implementer.toml`
- `editor_tooling`: `.codex/agents/editor-tooling.toml`
- `test_writer`: `.codex/agents/test-writer.toml`
- `doc_maintainer`: `.codex/agents/doc-maintainer.toml`
- `game_designer`: `.codex/agents/game-designer.toml`
- `content_designer`: `.codex/agents/content-designer.toml`
