# backlog-refinement

## Use for
- Turning rough ideas into Scrum-ready work
- Splitting large tasks into parallel lanes
- Defining acceptance criteria
- Assigning module or file ownership

## Inputs
- A feature idea, bug, or planning note
- Current repo docs and module layout
- Known dependencies or constraints

## Outputs
- Epic and story breakdown
- Clear task titles
- Testable acceptance criteria
- Ownership and dependency notes

## Splitting Rules
- Split by outcome, not by person.
- Keep each task to one primary module or file family.
- Move shared contracts into `Game.Core` before feature work.
- Separate docs work from runtime work when both are needed.

## Validation
- Each task can be completed and reviewed on its own.
- Dependencies are explicit.
- Acceptance criteria are observable and testable.
- Parallel work does not overlap on the same files.
