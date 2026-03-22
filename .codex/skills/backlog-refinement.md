# backlog-refinement

## Use for
- turning rough ideas into tracked backlog items
- splitting oversized tasks before implementation starts
- defining acceptance criteria, dependencies, and ownership
- creating a new `EG-*` item before untracked work is committed

## Read first
- `docs/index.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-01.md`

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
