---
name: sprint-planning
description: Use when building or updating a sprint plan, answering "what should we do next", choosing committed versus stretch work, or tracking sprint status. Do not use for direct feature implementation.
---

# sprint-planning

## Read first
- `docs/index.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- the current sprint file in `docs/planning/`, if one exists

## Scope
- sprint goal
- committed items
- stretch items
- status, blockers, dependencies, and risks

## Rules
- keep the sprint goal to one sentence
- commit only realistic work to the sprint
- place dependency-heavy work after the tasks they depend on
- add new work to the sprint before implementing it if it affects current sprint scope
- update sprint status when tasks move from ready to in progress, blocked, or done

## Concerns
- do not leave blocked tasks marked ready
- do not hide stretch work inside committed scope
- do not add work that has no owner, acceptance, or dependency notes
- do not let the sprint file diverge from the backlog

## Outputs
- sprint goal
- committed and stretch items
- dependency list
- risks and blockers
- up-to-date task status

## Done checks
- sprint goal is clear
- every tracked item has an owner and acceptance criteria
- blockers and dependencies are visible
- sprint and backlog docs agree on task identity and status
