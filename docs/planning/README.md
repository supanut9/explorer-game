# Planning

Use this folder for live task state, not for AI instructions.

## Files
- `backlog.md` - current epics, stories, and task order
- `sprint-01.md` - first sprint plan with sprint overview and Sprint 01 task details
- `sprint-02.md` - second sprint plan with repo stabilization and CI follow-up work
- `sprint-template.md` - reusable sprint planning format
- `task-template.md` - reusable task or subtask format

## Rule
- Keep `.codex/skills/` for reusable workflows.
- Keep `docs/planning/` for current backlog and sprint data.
- Keep the sprint overview and sprint task details in the same sprint file unless the sprint grows too large to review comfortably.
- AI should read `docs/index.md` and the relevant planning file before changing or adding tasks.
- Mirror active tasks to the GitHub Project board and assign a milestone when the work belongs to a sprint or release target.
- Start sprint work from `main`, then create or checkout the sprint branch for that sprint.
- Commit sprint work directly on the sprint branch unless a separate feature branch is explicitly needed.
- Include PR body metadata for summary, linked `EG-*` tracking ids, linked project card, target branch, scope, touched scenes or config assets when applicable, test evidence, sprint or milestone metadata, risks or notes, and merge readiness.
- Set GitHub PR metadata for labels, project, milestone, and assignee before review or merge.
- Use the repo label taxonomy: one or more `type:*` labels, the active `sprint:*` label, and one or more `area:*` labels. Add `status:*` labels only when needed.
- Merge sprint PRs into `main` with a merge commit, then delete the sprint branch after merge.
- Open a final PR from the sprint branch into `main` to finish the sprint.
