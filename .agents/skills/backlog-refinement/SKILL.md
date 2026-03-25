---
name: backlog-refinement
description: Use when turning rough ideas into tracked backlog items, splitting oversized work, or adding a new EG-* item before implementation starts. Do not use for direct runtime implementation.
---

# backlog-refinement

## Read first
- `docs/index.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- the current sprint file in `docs/planning/`, if one exists

## Inputs
- a feature idea, bug, gap, or untracked file
- current repo modules and file layout
- known blockers or dependencies

## Scope
- planning docs only
- backlog and sprint task structure
- acceptance criteria and ownership boundaries

## Rules
- add a backlog item before implementing real work that has no `EG-*` id
- split by outcome, not by person
- keep each task tied to one primary module or file family
- separate docs work from runtime work unless the doc update is part of the same task contract
- use stable `EG-*` ids for commits and references

## Concerns
- do not create tasks that touch many unrelated modules
- do not leave dependencies implicit
- do not create vague acceptance criteria like "works" or "improved"
- do not implement untracked work first and plan it later

## Outputs
- backlog entry with `EG-*`
- sprint task entry or sprint status update
- clear acceptance criteria
- ownership and dependency notes

## Done checks
- each task can be reviewed as one logical unit
- acceptance criteria are observable and testable
- dependencies are explicit
- overlapping file ownership is minimized
- planning docs match the current branch state
