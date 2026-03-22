# Planning

Use this folder for live task state, not for AI instructions.

## Files
- `backlog.md` - current epics, stories, and task order
- `sprint-01.md` - first sprint plan with sprint overview and Sprint 01 task details
- `sprint-template.md` - reusable sprint planning format
- `task-template.md` - reusable task or subtask format

## Rule
- Keep `.codex/skills/` for reusable workflows.
- Keep `docs/planning/` for current backlog and sprint data.
- Keep the sprint overview and sprint task details in the same sprint file unless the sprint grows too large to review comfortably.
- AI should read `docs/index.md` and the relevant planning file before changing or adding tasks.
- Mirror active tasks to the GitHub Project board and assign a milestone when the work belongs to a sprint or release target.
- Start sprint work from `main`, then create or checkout the sprint branch for that sprint.
- Create one fresh `feature/<task-slug>` branch at a time from the current sprint branch.
- Open a PR from the feature branch into the sprint branch before merging.
- Include PR metadata for summary, linked `EG-*` tracking id, linked project card, target branch, scope, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, and merge readiness.
- After merge, stop using that feature branch and create the next feature branch from the updated sprint branch.
- Open a final PR from the sprint branch into `main` to finish the sprint.
