---
name: interaction
description: Use for proximity prompts, nearest-target interaction, placeholder NPC talk behavior, and inspect interactions. Do not use for large dialogue, quest, or inventory systems.
---

# interaction

## Read first
- `docs/game-spec.md`
- `docs/content-decisions.md`
- the current sprint file in `docs/planning/`, if one exists
- `Assets/Game/UI/InteractionPromptLabel.cs`
- `Assets/Game/Core/GameSession.cs`

## Scope
- `Assets/Game/Interaction/*`
- interaction-facing UI bindings in `Assets/Game/UI/*`
- interaction play mode coverage when needed

## Rules
- keep interaction light and non-hostile
- prefer one small interface or contract over a large interaction framework
- select the nearest valid interactable only
- keep prompts lightweight and readable
- placeholder behaviors can log or display simple text, but must not create quest or inventory systems

## Concerns
- do not add heavy dialogue systems
- do not require scene-only wiring for logic that can be tested in code
- do not let invalid or non-interactable objects steal focus
- do not mix unrelated gameplay systems into interaction work

## Outputs
- interaction contracts
- prompt label binding
- probe logic
- placeholder interactable behaviors
- play mode tests when runtime behavior changes

## Done checks
- nearest valid target is selected predictably
- prompt text updates with the current target
- triggering the active target works
- non-interactable objects are ignored
- tests cover the runtime path if interaction logic changed
