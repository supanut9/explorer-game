# Repo Standards

## Folder Layout
- Put runtime gameplay code in `Assets/Game`.
- Keep editor-only tooling in `Assets/Game/Editor`.
- Keep tests under `Assets/Game/Tests`.
- Keep scene assets in `Assets/Scenes`.
- Keep shared config assets in `Assets/Resources/Configs`.

## Code Style
- One MonoBehaviour should have one responsibility.
- Prefer small, explicit contracts over broad utility classes.
- Put shared enums, constants, and runtime state in `Game.Core`.
- Put feature logic in the nearest module instead of a generic `Common` folder.
- Keep public surface area minimal unless another module needs the contract.

## Scene Rules
- Bootstrap should own session creation and route into character select.
- Character select should only choose the avatar and move into the world flow.
- World scenes should load predictably from scene-name contracts.
- Use catalogs and editor tooling to reduce manual scene wiring.

## Content Rules
- Placeholder-first is the default.
- Replace art by swapping prefabs or catalog entries, not by rewriting systems.
- Preserve stable names for scenes, assets, and content IDs when possible.

## AI Editing
- Change the smallest module that can own the behavior.
- Update docs in the same task when adding a new workflow or contract.
- Add validation before adding cleverness.
- Prefer repeatable editor tooling over hidden manual setup.

## Branch Workflow
- Start from `main` before beginning sprint work.
- Use one sprint branch per sprint, such as `sprint/01`.
- Create the sprint branch from `main`, or check it out if it already exists.
- Create one fresh feature branch at a time from the current sprint branch, such as `feature/core-session` or `feature/character-select`.
- Open a pull request from the feature branch into the sprint branch before merging.
- After a feature branch is merged, delete it or stop using it.
- Create the next feature branch from the updated sprint branch, not from an older feature branch.
- Do not revive an older feature branch after the sprint branch has moved.
- Open a final pull request from the sprint branch into `main` when the sprint is complete.
- Keep branch scope narrow and aligned with the owning module.

## Commit Messages
- Use Conventional Commits-style messages for every commit: `type(scope): [EG-id] subject`.
- Supported types are `feat`, `fix`, `refactor`, `docs`, `chore`, `test`, `style`, `perf`, `build`, and `ci`.
- Use a scope when the change belongs to a clear module, such as `repo`, `docs`, `core`, `editor`, `player`, `world`, `interaction`, or `tests`.
- The bracket tag should use the stable backlog tracking id, such as `[EG-1]` or `[EG-8]`.
- Story order such as `2.1` or `5.2` should remain in planning docs, but commits and PR references should use the `EG-*` id.
- If a change does not yet have an `EG-*` id, add it to the backlog first before committing.
- Keep the subject short, imperative, lowercase, and without a trailing period.
- Example formats: `docs(repo): [EG-1] standardize git workflow`, `feat(core): [EG-2] restore shared session state`, `feat(world): [EG-6] add stable zone catalog`, `test(tests): [EG-11] cover session persistence`.

## Pull Requests
- Keep feature PRs narrow and targeted to one sprint branch.
- Open feature PRs into the sprint branch, not directly into `main`.
- Open sprint completion PRs from the sprint branch into `main`.
- Do not continue work on an already-merged feature branch.
- Every PR should include summary, linked `EG-*` tracking id, linked project card, target branch, touched modules, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, and a merge readiness checklist.
- Include screenshots or a short video for UI or gameplay changes.
- Mention any Unity editor steps required to verify the change.
