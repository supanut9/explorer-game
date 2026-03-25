# Task Skills

## Purpose
This file maps reusable task families to the Codex skills used in this repo. The canonical skill bodies live in `.agents/skills/<skill-name>/SKILL.md`, and `AGENTS.md` routes tasks to them.

## Gameplay Skills
- `player-controller` - avatar selection, movement, camera, and input mapping.
- `world-zone` - zone definitions, travel, world catalogs, and scene loading.
- `interaction` - inspectables, dialogue targets, prompts, and range-based interaction.
- `ui-flow` - menu flow, selection screens, HUD labels, and prompt UI.
- `testing` - edit mode or play mode tests, plus validation paths for configs and state transitions.

## Setup Skills
- `unity-bootstrap` - scene flow, bootstrap state, build settings, and editor setup tools.

## Planning Skills
- `backlog-refinement` - turn rough ideas into Scrum-ready tasks, stories, and dependencies.
- `sprint-planning` - turn refined backlog items into a sprint goal and committed work.

## Docs Skills
- `docs-maintenance` - update repo docs, task conventions, and workflow notes.

## Selection Rule
- Pick the smallest skill that owns the change.
- If a task crosses skills, choose one primary skill and document the handoff.
- Do not use a generic skill when a more specific one exists.

## Skill Locations
- `backlog-refinement`: `.agents/skills/backlog-refinement/SKILL.md`
- `docs-maintenance`: `.agents/skills/docs-maintenance/SKILL.md`
- `interaction`: `.agents/skills/interaction/SKILL.md`
- `player-controller`: `.agents/skills/player-controller/SKILL.md`
- `sprint-planning`: `.agents/skills/sprint-planning/SKILL.md`
- `testing`: `.agents/skills/testing/SKILL.md`
- `ui-flow`: `.agents/skills/ui-flow/SKILL.md`
- `unity-bootstrap`: `.agents/skills/unity-bootstrap/SKILL.md`
- `world-zone`: `.agents/skills/world-zone/SKILL.md`
