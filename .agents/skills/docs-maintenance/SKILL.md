---
name: docs-maintenance
description: Use when updating repo workflow docs, planning docs, skills, agent metadata, or any documentation that must stay aligned with the current code and process. Do not use for unrelated runtime changes.
---

# docs-maintenance

## Read first
- `docs/index.md`
- `AGENTS.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- the current sprint file in `docs/planning/`, if one exists

## Scope
- markdown docs in the repo
- planning status and tracking ids
- workflow and contribution guidance
- Codex repo metadata such as skills, agents, and rules docs

## Rules
- treat docs as the source of truth before implementation
- if code changes a workflow or contract, update the docs in the same task
- keep one sprint file as the sprint source of truth
- use the documented commit format: `type(scope): [EG-id] subject`
- update status fields when work starts, blocks, or finishes

## Concerns
- do not duplicate the same rule in many files unless one is the canonical source and others are short references
- do not leave stale planning status after code is committed
- do not add long prose when a checklist is enough
- do not invent alternate workflows that conflict with repo standards

## Outputs
- concise doc updates
- aligned backlog and sprint entries
- corrected workflow references

## Done checks
- docs match the current code and git workflow
- links and task references are still valid
- examples use the same branch, commit, and PR conventions
- the changed doc is the right source for that rule
