# Sprint 06

## Sprint Goal
- Align the repository's Codex workflow metadata with the current native skills, subagents, and command-rules model so future AI work follows the repo standard by default.

## Committed Items
- Codex-native skills, subagents, and rules migration

## Stretch Items
- Follow-up cleanup for any stale planning references discovered during the migration review

## Dependencies
- `AGENTS.md`
- `docs/index.md`
- `docs/repo-standards.md`
- `docs/ai-workflow.md`
- `docs/task-skills.md`
- `docs/agent-roles.md`
- `docs/planning/backlog.md`
- `https://developers.openai.com/codex/guides/agents-md`
- `https://developers.openai.com/codex/skills`
- `https://developers.openai.com/codex/subagents`
- `https://developers.openai.com/codex/rules`

## Risks
- Repo-local Codex metadata can drift again if routing rules in `AGENTS.md` and the tracked skill or subagent files stop matching.
- Removing the legacy Markdown-only layout can break expectations in older notes if planning references are not updated at the same time.
- Command rules that are too broad can slow normal repo work, while rules that are too loose can undercut the intended safety policy.

## Workflow
- Use `sprint/06` as the integration branch for Sprint 06.
- Checkout `main`.
- Create or checkout `sprint/06`.
- Commit Sprint 06 work directly on `sprint/06`.
- Keep each commit scoped to one logical change and one `EG-*` task.
- Track active work on the GitHub Project board and apply the Sprint 06 milestone once it exists.
- Open a final PR from `sprint/06` into `main`.
- Merge the sprint PR with a merge commit so Sprint 06 history is preserved on `main`.

## Review Notes
- Confirm `AGENTS.md` routes planning, runtime, docs, testing, and bootstrap tasks to the right repo skills.
- Confirm each repo skill uses `SKILL.md` plus YAML frontmatter.
- Confirm each repo subagent uses `.toml` metadata instead of Markdown role notes.
- Confirm command rules match the repo's desired git and GitHub safety posture.
- Confirm project docs point to the new Codex-native layout and no longer treat the old layout as canonical.

## Sprint Tasks

### EG-63 Codex-Native Skills, Subagents, And Rules Migration
- Story Order: `20.1`
- Status: done
- Owner: `Docs`
- Module: `AGENTS.md`, `.agents`, `.codex`, `docs`
- Files: `AGENTS.md`, `.agents/skills/*/SKILL.md`, `.codex/agents/*.toml`, `.codex/rules/*.rules`, `docs/index.md`, `docs/ai-workflow.md`, `docs/task-skills.md`, `docs/agent-roles.md`, `docs/planning/README.md`
- Goal: move the repo from the legacy Markdown-only Codex layout to the current native skills, subagents, and command-rules model.
- Acceptance:
  - the repo's skill definitions live under `.agents/skills/<skill>/SKILL.md` with YAML frontmatter
  - the repo's custom subagents live under `.codex/agents/*.toml`
  - the repo's command approval policy is versioned under `.codex/rules/*.rules`
  - `AGENTS.md` routes Codex to the correct skills and subagents for common repo task families
  - project docs point to the new Codex-native structure consistently
- Subtasks:
  - replace the legacy Markdown-only skill layout with `SKILL.md` files
  - replace Markdown agent role files with `.toml` subagent definitions
  - add repo command rules for safe git and GitHub usage
  - update docs and planning references to the new layout
- Dependencies:
  - `EG-15`
- Notes:
  - keep the migration scoped to workflow metadata and documentation, not gameplay behavior
  - verified the repo command policy with `codex execpolicy check` for allow, prompt, and forbidden git paths
