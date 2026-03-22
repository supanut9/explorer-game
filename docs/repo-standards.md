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

## Input Configuration
- `InputActionProperty` fields may be configured either with an `InputActionReference` or with a valid inline action on the component.
- Empty serialized `InputActionProperty` values should be treated as missing input and should fall back to the repo's desktop baseline controls.
- Generated placeholder content should remain playable on keyboard and mouse even when no action asset is wired in the Inspector.

## Content Rules
- Placeholder-first is the default.
- Replace art by swapping prefabs or catalog entries, not by rewriting systems.
- Preserve stable names for scenes, assets, and content IDs when possible.

## Project Settings
- Treat `ProjectSettings/*` changes as tracked repository configuration, not editor noise.
- Commit project-setting diffs only when the resulting baseline is intentional and reviewable.
- If a project setting changes implicitly, either revert it or document why the repo should keep it.

## AI Editing
- Change the smallest module that can own the behavior.
- Update docs in the same task when adding a new workflow or contract.
- Add validation before adding cleverness.
- Prefer repeatable editor tooling over hidden manual setup.

## Branch Workflow
- Start from `main` before beginning sprint work.
- Use one sprint branch per sprint, such as `sprint/01`.
- Create the sprint branch from `main`, or check it out if it already exists.
- Commit sprint work directly on the sprint branch unless a separate feature branch is explicitly needed.
- Keep each commit scoped to one logical change and one `EG-*` task.
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
- Open sprint completion PRs from the sprint branch into `main`.
- Merge sprint completion PRs into `main` with a merge commit, not squash merge, so the sprint's `EG-*` commit history stays preserved on `main`.
- After a sprint PR is merged, the sprint branch can be deleted.
- Every PR should include body metadata for summary, linked `EG-*` tracking ids, linked project card, target branch, touched modules, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, risks or notes, and a merge readiness checklist.
- Every GitHub PR should also set metadata on the PR itself: labels, project, milestone, and assignee.
- PRs can and should carry multiple labels when the scope spans multiple kinds of work or modules.
- Use repo labels from these groups:
  - type: `type:docs`, `type:feature`, `type:fix`, `type:test`, `type:refactor`, `type:chore`
  - sprint: `sprint:01` for Sprint 01 work
  - area: `area:repo`, `area:core`, `area:player`, `area:world`, `area:interaction`, `area:animals`, `area:ui`, `area:editor`, `area:tests`
  - optional status: `status:review`, `status:blocked`
- Sprint PRs should include at least one `type:*` label, one `sprint:*` label, and one or more `area:*` labels.
- Add reviewers when the change needs explicit review ownership.
- Include screenshots or a short video for UI or gameplay changes.
- Mention any Unity editor steps required to verify the change.
