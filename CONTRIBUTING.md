# Contributing

## Before You Start
Read these files first:
- [README.md](README.md)
- [AGENTS.md](AGENTS.md)
- [docs/index.md](docs/index.md)
- [LICENSE](LICENSE)

## Project Rules
- Keep gameplay code in `Assets/Game`.
- Prefer small changes in the module that owns the behavior.
- Update docs when adding a new workflow, contract, or scene flow.
- Treat `docs/game-spec.md` and `docs/repo-standards.md` as the source of truth.

## Workflow
1. Inspect the relevant docs and existing module.
2. Make the smallest change that solves the task.
3. Add or update tests when behavior changes.
4. Run the Unity Editor checks when the task touches scenes, input, or config assets.

## GitHub Tracking
- Track active work in the GitHub Project board for this repo.
- Use milestones for release or sprint outcomes such as `MVP Foundation` or `Playable Exploration`.
- Keep one task card per clear deliverable and avoid putting subtask detail into milestones.

## Branches
- Start from `main`.
- Create or checkout the sprint branch for the active sprint, such as `sprint/01`.
- Create one fresh feature branch at a time from the current sprint branch, such as `feature/core-session` or `feature/character-select`.
- Open a PR from the feature branch into the sprint branch before merging.
- After the PR is merged, delete the feature branch or stop using it.
- Create the next feature branch from the updated sprint branch.
- Do not continue work on an already-merged feature branch.
- Open a final PR from the sprint branch into `main` when the sprint is complete.
- Keep branch scope narrow and aligned with the owning module.

## Commit Messages
- Use Conventional Commits-style messages for every commit: `type(scope): [EG-id] subject`.
- Supported types are `feat`, `fix`, `refactor`, `docs`, `chore`, `test`, `style`, `perf`, `build`, and `ci`.
- Use the stable backlog tracking id in the bracket tag, such as `[EG-1]` or `[EG-8]`.
- Keep story order such as `2.1` or `5.2` in planning docs only.
- If a change does not yet have an `EG-*` id, add it to the backlog first.
- Keep the subject short, imperative, lowercase, and without a trailing period.
- Keep each commit focused on one logical change.
- Example commits: `docs(repo): [EG-1] standardize git workflow`, `feat(core): [EG-2] restore shared session state`, `feat(world): [EG-6] add stable zone catalog`, `test(tests): [EG-11] cover session persistence`.

## Pull Requests
- Open feature PRs into the active sprint branch.
- Open sprint completion PRs from the sprint branch into `main`.
- Use this PR checklist for every merge:
  - summary
  - linked `EG-*` tracking id
  - linked project card
  - target branch
  - touched modules or scope
  - touched scenes or config assets when applicable
  - test evidence
  - sprint or milestone metadata
  - merge readiness checklist
- Include screenshots or a short video for UI or gameplay changes.
- Mention any Unity editor steps required to verify the change.
- Keep PRs narrow enough to review in one pass.
