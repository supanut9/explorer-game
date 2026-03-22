# Decision Rules

## Use These Rules
- Follow the docs before making assumptions.
- If the docs answer the task, implement that answer.
- If the docs conflict with code, update the docs in the same change.
- If the docs do not answer the task, stop at the smallest missing decision and ask or document it first.
- Decide whether the task should be split into smaller parts by default, but only use parallel execution when the split is actually beneficial and the file ownership is disjoint.

## Safe Assumptions
- Prefer the existing repo baseline when the docs are silent.
- Keep changes small when the task does not require broader refactoring.
- Keep new work inside the owning module unless the docs say otherwise.

## Do Not Assume
- New gameplay scope
- New scene flow
- New build or tooling behavior
- New file ownership across modules

## Examples
- Single task: update a README section or one gameplay script with no cross-module dependency.
- Split task: a feature that needs a shared contract in `Game.Core` and one feature module that consumes it.
- Parallel task: docs, core contracts, and UI changes that touch different files and can be owned separately.
