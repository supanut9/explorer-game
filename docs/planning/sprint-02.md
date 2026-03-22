# Sprint 02

## Sprint Goal
- Turn the Sprint 01 foundation into a more believable first playable by adding content, scene verification, and stronger validation.

## Committed Items
- Placeholder prefab hookup
- Zone dressing pass
- Ambient placement pass
- Scene wiring and traversal verification
- Unity validation workflow definition

## Stretch Items
- License baseline
- Shader Graph settings baseline
- CI expansion beyond the first Unity-aware workflow
- Additional repo metadata cleanup if new untracked files appear

## Dependencies
- `docs/game-spec.md`
- `docs/content-plan.md`
- `docs/content-decisions.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-01.md`
- `ProjectSettings/ShaderGraphSettings.asset`

## Risks
- Placeholder content may drift into asset-detail decisions that belong in later passes.
- Scene editing work can create noisy diffs if ownership is not kept clear.
- Unity CI may require environment, licensing, or caching decisions that are not yet documented.
- Project settings files can change implicitly when the editor version or packages change.

## Workflow
- Use `sprint/02` as the integration branch for Sprint 02.
- Checkout `main`.
- Create or checkout `sprint/02`.
- Commit Sprint 02 work directly on `sprint/02`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the Sprint 02 milestone once it exists.
- Open a final PR from `sprint/02` into `main`.
- Merge the sprint PR with a merge commit so Sprint 02 history is preserved on `main`.

## Review Notes
- Confirm the first playable loop feels populated, not just technically functional.
- Confirm scene and content changes are intentional and not editor noise.
- Confirm support work stays in support scope and does not crowd out playable progress.

## Sprint Tasks

### EG-17 License Baseline
- Story Order: `10.1`
- Status: done
- Owner: `Docs`
- Module: `Repository Docs`
- Files: `LICENSE`, `README.md`, `CONTRIBUTING.md`
- Goal: track the repository license intentionally and make the chosen baseline visible to contributors.
- Acceptance:
  - `LICENSE` is committed
  - the repo docs acknowledge the chosen license baseline where appropriate
  - license handling is no longer left as an untracked file
- Subtasks:
  - review the current license file
  - decide whether the current text is the intended repo license
  - update contributor-facing docs if needed
- Dependencies:
  - `EG-1`
- Notes:
  - the repo now tracks the MIT license baseline directly in `LICENSE`
  - README and contributor docs point contributors at the committed license text

### EG-18 ShaderGraph Settings Baseline
- Story Order: `10.2`
- Status: done
- Owner: `ProjectSettings`
- Module: `ProjectSettings`
- Files: `ProjectSettings/ShaderGraphSettings.asset`
- Goal: make the Shader Graph project setting change intentional and reviewable.
- Acceptance:
  - the current `overrideShaderVariantLimit` change is either committed or reverted deliberately
  - the final project state is treated as tracked repo configuration
- Subtasks:
  - inspect the current serialized setting change
  - decide whether the override belongs in the repo baseline
  - commit or revert the change under a tracked task
- Dependencies:
  - `EG-16`
- Notes:
  - the repo keeps `overrideShaderVariantLimit: 0` as the current Unity-generated baseline
  - project settings changes should be treated as intentional repository configuration, not ignored editor noise

### EG-19 Unity Validation Workflow
- Story Order: `11.1`
- Status: done
- Owner: `CI`
- Module: `.github/workflows`
- Files: `.github/workflows/unity-validation.yml`, `docs/planning/sprint-02.md`
- Goal: define and implement the first Unity-aware CI workflow after `repo-checks`.
- Acceptance:
  - Sprint 02 has a tracked Unity validation task
  - the workflow scope is documented before implementation
  - the first Unity-aware validation path is ready to become a required check once stable
- Subtasks:
  - define whether the first workflow covers edit mode tests, play mode tests, or config validation only
  - document environment and licensing constraints
  - implement the first Unity-aware workflow once the scope is chosen
- Dependencies:
  - `EG-16`
- Notes:
  - keep the first pass pragmatic and avoid overcommitting on full Unity CI too early
  - first pass uses GameCI EditMode validation and skips the Unity test job until license secrets are configured

### EG-20 Placeholder Prefab Hookup
- Story Order: `12.1`
- Status: done
- Owner: `Game.Player`
- Module: `Assets/Resources/Configs`, `Assets/Scenes`
- Files: `Assets/Resources/Configs/CharacterCatalog.asset`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/WorldPersistent.unity`
- Goal: move the first playable away from empty prefab references by hooking character and placeholder NPC content into the runtime flow.
- Acceptance:
  - character selection resolves to real or stable placeholder prefabs
  - the world flow has placeholder NPC content available for the interaction systems
  - the first playable loop no longer depends on missing prefab references
- Subtasks:
  - review the current character catalog asset contents
  - hook up stable placeholder prefabs where references are missing
  - confirm world-facing placeholder actors exist for interaction coverage
- Dependencies:
  - `EG-3`
  - `EG-7`
  - `EG-8`
- Notes:
  - keep placeholder-first and do not overdesign final art structure
  - implemented through scaffold generation so new project bootstrap runs create placeholder character and NPC prefabs

### EG-21 Zone Dressing Pass
- Story Order: `12.2`
- Status: done
- Owner: `Content`
- Module: `Assets/Scenes`
- Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: give each zone a readable placeholder environment so traversal feels intentional.
- Acceptance:
  - village, forest, and mountain each have a distinct placeholder dressing pass
  - zones contain basic landmarks and traversal cues
  - content stays placeholder-first and aligned with the content plan
- Subtasks:
  - add houses and path cues to village
  - add trees, shrubs, and terrain dressing to forest
  - add rock and cliff cues to mountain
- Dependencies:
  - `docs/content-plan.md`
  - `docs/content-decisions.md`
  - `EG-6`
- Notes:
  - focus on readability over asset detail
  - implemented through scaffold generation so new zone scenes start with distinct placeholder dressing

### EG-22 Ambient Placement Pass
- Story Order: `12.3`
- Status: done
- Owner: `Content`
- Module: `Assets/Scenes`
- Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: place NPCs, inspectables, and animals so the first playable loop exercises Sprint 01 systems in the world.
- Acceptance:
  - at least one talk interaction is reachable in the playable flow
  - at least one inspectable is reachable in the playable flow
  - passive animals are placed where the ambient behavior is visible
- Subtasks:
  - place villagers or guide NPCs in the village
  - place inspectables in at least one zone
  - place passive animals in forest or mountain areas
- Dependencies:
  - `EG-8`
  - `EG-9`
  - `EG-13`
  - `EG-21`
- Notes:
  - use existing placeholder systems instead of inventing new gameplay
  - implemented through scaffold generation so village, forest, and mountain scenes start with talk, inspect, and passive-animal placeholder content

### EG-23 Scene Wiring Verification
- Story Order: `13.1`
- Status: in progress
- Owner: `Game.Editor`
- Module: `Assets/Scenes`, `docs/planning`
- Files: `Assets/Scenes/Bootstrap.unity`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/WorldPersistent.unity`, `docs/planning/sprint-02.md`
- Goal: define and execute a repeatable in-editor verification pass for the first playable loop.
- Acceptance:
  - bootstrap to character select to world traversal is verified with explicit steps
  - major scene wiring assumptions are documented
  - blockers found during verification are captured as tracked follow-up work
- Subtasks:
  - define the verification checklist
  - run the playable loop manually in the editor
  - record follow-up issues if scene wiring breaks
- Dependencies:
  - `EG-4`
  - `EG-7`
  - `EG-20`
  - `EG-22`
- Notes:
  - this is the bridge between code-complete and actually playable
  - validation tooling and checklist are in place, but the manual Unity Editor verification pass still needs to be run
