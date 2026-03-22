# Sprint 05

## Sprint Goal
- Give the connected exploration slice a coherent visual direction, stronger scene mood, and cleaner player-facing presentation without changing the core traversal and interaction loop.

## Committed Items
- World palette and material cohesion
- Lighting and atmosphere pass
- Landmark silhouette pass
- Character select presentation refresh
- Interaction prompt presentation polish

## Stretch Items
- Avatar and NPC placeholder cleanup
- Presentation review checklist

## Dependencies
- `docs/game-spec.md`
- `docs/content-plan.md`
- `docs/content-decisions.md`
- `docs/repo-standards.md`
- `docs/planning/backlog.md`
- `docs/planning/sprint-04.md`
- `Assets/Game/Editor/ExplorerProjectBootstrapper.cs`
- `Assets/Game/UI/CharacterSelectionView.cs`
- `Assets/Game/UI/InteractionPromptLabel.cs`

## Risks
- Visual polish can drift into uncontrolled content work if the sprint is not constrained to palette, framing, and readability.
- Changing materials and lighting can accidentally reduce traversal readability if visual direction wins over navigation clarity.
- Character-select polish can become a UI redesign unless the scope stays presentation-only.
- Placeholder cleanup can create prefab churn without enough improvement if silhouette and proportion goals are not explicit.

## Workflow
- Use `sprint/05` as the integration branch for Sprint 05.
- Checkout `main`.
- Create or checkout `sprint/05`.
- Commit Sprint 05 work directly on `sprint/05`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the Sprint 05 milestone once it exists.
- Open a final PR from `sprint/05` into `main`.
- Merge the sprint PR with a merge commit so Sprint 05 history is preserved on `main`.

## Review Notes
- Confirm the world no longer reads like random colored primitives.
- Confirm village, forest, and mountain each feel distinct while still belonging to one game.
- Confirm traversal anchors remain visible after the presentation pass.
- Confirm the player-facing UI stays minimal and readable.
- Outcome:
  - confirmed in-editor after scaffold rerun
  - village and forest screenshots show clearer palette separation, stronger signposting silhouettes, and improved placeholder character readability
  - interaction prompt remains visible over the updated backgrounds

## Sprint Tasks

### EG-53 World Palette And Material Cohesion
- Story Order: `17.1`
- Status: done
- Owner: `Content`, `Game.Editor`
- Module: `Assets/Resources/Prefabs/Materials`, `Assets/Scenes`
- Files: `Assets/Resources/Prefabs/Materials/*.mat`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: replace the arbitrary placeholder color mix with a deliberate shared palette and clearer biome identity.
- Acceptance:
  - village, forest, and mountain each use a smaller, more intentional material palette
  - traversal anchors and landmarks still stand out clearly
  - the resulting scenes feel visually related instead of randomly colored
- Subtasks:
  - define a palette direction for each biome
  - update materials and affected scene instances
  - verify that route readability survives the palette pass
- Dependencies:
  - `EG-48`
  - `EG-52`
- Notes:
  - this is a palette pass, not final art replacement

### EG-54 Lighting And Atmosphere Pass
- Story Order: `17.2`
- Status: done
- Owner: `Content`, `Game.Editor`
- Module: `Assets/Scenes`
- Files: `Assets/Scenes/Bootstrap.unity`, `Assets/Scenes/CharacterSelect.unity`, `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: improve scene mood through lighting, sky, and framing so the connected slice feels intentionally presented.
- Acceptance:
  - core scenes no longer rely on raw default lighting feel
  - atmosphere improves readability instead of obscuring traversal
  - bootstrap, character select, and world scenes feel visually connected
- Subtasks:
  - tune scene lighting and sky presentation
  - adjust framing where mood or readability is currently weak
  - verify that camera and traversal readability still hold after the pass
- Dependencies:
  - `EG-45`
  - `EG-53`
- Notes:
  - keep this pass lightweight and repeatable

### EG-55 Landmark Silhouette Pass
- Story Order: `17.3`
- Status: done
- Owner: `Content`
- Module: `Assets/Scenes`
- Files: `Assets/Scenes/VillageZone.unity`, `Assets/Scenes/ForestZone.unity`, `Assets/Scenes/MountainZone.unity`
- Goal: make important landmarks and traversal anchors recognizable at a glance through better shape, spacing, and contrast.
- Acceptance:
  - village signs, forest portal, and mountain landmarks read clearly at distance
  - major silhouettes are not lost against the environment
  - landmark improvements do not block movement or interactions
- Subtasks:
  - identify weak silhouettes in the current exploration slice
  - reshape or reposition key props and landmarks
  - retest the connected path after silhouette changes
- Dependencies:
  - `EG-45`
  - `EG-48`
- Notes:
  - prioritize clarity over asset count

### EG-56 Character Select Presentation Refresh
- Story Order: `18.1`
- Status: done
- Owner: `Game.UI`, `Game.Editor`
- Module: `Assets/Game/UI`, `Assets/Scenes`
- Files: `Assets/Game/UI/CharacterSelectionView.cs`, `Assets/Scenes/CharacterSelect.unity`
- Goal: keep character select minimal while making it feel intentionally framed and visually aligned with the world.
- Acceptance:
  - avatar choice remains obvious
  - presentation feels cleaner and more deliberate than the current scaffold view
  - no additional UI systems are introduced
- Subtasks:
  - improve framing and presentation of the existing select scene
  - tighten copy and layout where needed
  - verify that the flow still routes cleanly into the world
- Dependencies:
  - `EG-27`
  - `EG-54`
- Notes:
  - this is presentation polish, not flow redesign

### EG-57 Interaction Prompt Presentation Polish
- Story Order: `18.2`
- Status: done
- Owner: `Game.UI`, `Game.Interaction`
- Module: `Assets/Game/UI`, `Assets/Scenes`
- Files: `Assets/Game/UI/InteractionPromptLabel.cs`, `Assets/Scenes/WorldPersistent.unity`
- Goal: make the prompt more legible and visually integrated while keeping it lightweight.
- Acceptance:
  - prompts remain minimal and unobtrusive
  - prompt readability improves over varied backgrounds
  - prompt styling fits the updated presentation direction
- Subtasks:
  - improve prompt readability and placement
  - align visual treatment with the new scene mood
  - verify prompt behavior still matches the existing interaction flow
- Dependencies:
  - `EG-8`
  - `EG-54`
- Notes:
  - keep the prompt system simple

### EG-58 Avatar And Npc Placeholder Cleanup
- Story Order: `18.3`
- Status: done
- Owner: `Content`, `Game.Player`, `Game.Interaction`
- Module: `Assets/Resources/Prefabs/Characters`, `Assets/Resources/Prefabs/NPCs`
- Files: `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Resources/Prefabs/NPCs/*.prefab`
- Goal: make placeholder characters and NPCs read better in silhouette and material treatment without introducing final art dependencies.
- Acceptance:
  - player and guide NPC placeholders look less like raw capsules
  - silhouettes remain readable in gameplay camera framing
  - cleanup stays compatible with the current runtime and scaffold flow
- Subtasks:
  - identify the most distracting placeholder issues
  - improve silhouette, proportions, and material treatment
  - verify camera framing and interaction still read cleanly
- Dependencies:
  - `EG-54`
  - `EG-55`
- Notes:
  - this stays firmly placeholder-first

### EG-59 Presentation Review Checklist
- Story Order: `19.1`
- Status: done
- Owner: `Docs`, `Game.Editor`
- Module: `docs/planning`
- Files: `docs/planning/sprint-05.md`
- Goal: make the visual pass reviewable with a repeatable checklist instead of subjective spot checks.
- Acceptance:
  - Sprint 05 defines a concrete review path for palette, atmosphere, silhouettes, and UI readability
  - the checklist names which scenes to review and what to look for
  - the checklist distinguishes mandatory fixes from nice-to-have polish
- Subtasks:
  - define the minimal visual review route
  - list observable review criteria by scene
  - capture the sprint outcome against that checklist
- Dependencies:
  - `EG-53`
  - `EG-54`
  - `EG-55`
  - `EG-56`
  - `EG-57`
- Notes:
  - this is how the sprint should be closed, not how it starts

## Presentation Review Checklist

### Mandatory Route
- Open `Bootstrap` and confirm the entry camera no longer feels like raw default Unity framing.
- Move through `CharacterSelect` and confirm the selection panel reads clearly at first glance.
- Enter the world and walk the village -> forest route.
- Stop at the village signs, forest portal, forest lookout marker, and mountain beacon.
- Approach one NPC and one inspectable to verify prompt readability against the updated backgrounds.

### Mandatory Checks
- Palette:
  - village, forest, and mountain each read as distinct biomes
  - materials feel related instead of randomly assigned
  - traversal anchors still stand out from the environment
- Atmosphere:
  - lighting, fog, and ambient color improve mood without hiding the route
  - bootstrap, character select, and world scenes feel visually related
- Silhouette:
  - village signs, forest portal, and mountain beacon read from gameplay distance
  - placeholder avatars and the guide NPC are less raw than capsule-only shapes
- UI:
  - character-select copy and hierarchy are readable without explanation
  - interaction prompts remain legible over ground, props, and horizon lines

### Nice To Have
- Character-select framing feels deliberate enough to capture in a short clip without extra cleanup.
- Forest and mountain landmarks feel memorable, not just functional.
- Placeholder figures have enough personality to support one more sprint before final art replacement.

### EG-60 Sprint 05 Presentation Asset Sync
- Story Order: `19.2`
- Status: done
- Owner: `Content`, `Game.Editor`
- Module: `Assets/Scenes`, `Assets/Resources/Prefabs`
- Files: `Assets/Scenes/*.unity`, `Assets/Resources/Prefabs/Characters/*.prefab`, `Assets/Resources/Prefabs/Materials/*.mat`
- Goal: capture the reviewed Sprint 05 scene, prefab, and material changes as the baseline presentation assets.
- Acceptance:
  - regenerated scene assets match the scaffold-driven palette, atmosphere, and silhouette pass
  - character prefabs serialize the placeholder silhouette cleanup
  - only intentional visual baseline assets are committed, with editor noise left out
- Subtasks:
  - regenerate scaffolding after the Sprint 05 content changes
  - review the resulting scene, prefab, and material diff
  - commit the visual baseline assets without editor-only noise
- Dependencies:
  - `EG-53`
  - `EG-54`
  - `EG-55`
  - `EG-56`
  - `EG-58`
- Notes:
  - this is the sync point between generator code and serialized Unity assets

### EG-61 Repo Ignore And Starter Asset Cleanup
- Story Order: `19.3`
- Status: done
- Owner: `Docs`, `ProjectSettings`
- Module: `.gitignore`, `ProjectSettings`, `Assets`
- Files: `.gitignore`, `ProjectSettings/ProjectSettings.asset`, `Assets/Scenes/SampleScene.unity`, `Assets/Readme.asset`, `Assets/TutorialInfo/*`, `Assets/Settings/SampleSceneProfile.asset`
- Goal: remove leftover Unity template artifacts and ignore recurring editor-only scene template metadata.
- Acceptance:
  - `ProjectSettings/SceneTemplateSettings.json` is ignored
  - the project template default scene points at `Bootstrap`
  - Unity starter tutorial assets no longer remain in the tracked repo
- Subtasks:
  - add the ignore rule for scene template metadata
  - repoint the template default scene away from `SampleScene`
  - delete unused tutorial and starter sample assets
- Dependencies:
  - `EG-60`
- Notes:
  - this is repository cleanup, not gameplay scope

### EG-62 Sprint 05 Material Serialization Sync
- Story Order: `19.4`
- Status: done
- Owner: `Content`, `Game.Editor`
- Module: `Assets/Resources/Prefabs/Materials`
- Files: `Assets/Resources/Prefabs/Materials/*.mat`
- Goal: align the tracked material serialization with the reviewed Sprint 05 palette so `_Color` and `_BaseColor` no longer drift.
- Acceptance:
  - material files serialize the Sprint 05 palette consistently
  - the sync does not introduce new colors or alter the approved presentation direction
  - the diff remains limited to material serialization updates
- Subtasks:
  - inspect representative material diffs
  - confirm the updates are consistent normalization, not new art direction
  - commit the material sync separately from starter-asset cleanup
- Dependencies:
  - `EG-53`
  - `EG-60`
- Notes:
  - this is a serialization sync, not a new visual pass
