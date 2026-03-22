#!/usr/bin/env python3
import json
import re
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]

REQUIRED_DOCS = [
    "docs/index.md",
    "docs/game-spec.md",
    "docs/repo-standards.md",
    "docs/planning/README.md",
    "docs/planning/backlog.md",
    "docs/planning/sprint-01.md",
]

REQUIRED_ASSET_DIRS = [
    "Assets/Game/Core",
    "Assets/Game/Player",
    "Assets/Game/World",
    "Assets/Game/UI",
    "Assets/Game/Editor",
    "Assets/Game/Tests/EditMode",
]

BACKLOG_ID_PATTERN = re.compile(r"Tracking ID:\s+`(EG-\d+)`")
SPRINT_ID_PATTERN = re.compile(r"^###\s+(EG-\d+)\b", re.MULTILINE)


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)
    raise SystemExit(1)


def ensure_required_paths() -> None:
    for relative_path in REQUIRED_DOCS + REQUIRED_ASSET_DIRS:
        if not (ROOT / relative_path).exists():
            fail(f"required path is missing: {relative_path}")


def validate_asmdefs() -> None:
    for asmdef_path in ROOT.glob("Assets/**/*.asmdef"):
        try:
            data = json.loads(asmdef_path.read_text(encoding="utf-8"))
        except json.JSONDecodeError as exc:
            fail(f"invalid asmdef JSON at {asmdef_path.relative_to(ROOT)}: {exc}")

        if not isinstance(data.get("name"), str) or not data["name"].strip():
            fail(f"asmdef missing name: {asmdef_path.relative_to(ROOT)}")


def validate_meta_files() -> None:
    for asset_path in ROOT.glob("Assets/**/*"):
        if not asset_path.is_file():
            continue
        if asset_path.suffix == ".meta":
            continue
        meta_path = Path(str(asset_path) + ".meta")
        if not meta_path.exists():
            fail(f"missing Unity meta file for {asset_path.relative_to(ROOT)}")


def validate_planning_ids() -> None:
    backlog_path = ROOT / "docs/planning/backlog.md"
    sprint_path = ROOT / "docs/planning/sprint-01.md"

    backlog_text = backlog_path.read_text(encoding="utf-8")
    sprint_text = sprint_path.read_text(encoding="utf-8")

    backlog_ids = BACKLOG_ID_PATTERN.findall(backlog_text)
    sprint_ids = SPRINT_ID_PATTERN.findall(sprint_text)

    if not backlog_ids:
        fail("no EG ids found in backlog")
    if not sprint_ids:
        fail("no EG ids found in sprint plan")

    backlog_unique = set(backlog_ids)
    sprint_unique = set(sprint_ids)

    if len(backlog_unique) != len(backlog_ids):
        fail("duplicate EG ids found in backlog")

    missing_in_sprint = sorted(backlog_unique - sprint_unique)
    if missing_in_sprint:
        fail(f"backlog ids missing from sprint plan: {', '.join(missing_in_sprint)}")


def main() -> None:
    ensure_required_paths()
    validate_asmdefs()
    validate_meta_files()
    validate_planning_ids()
    print("Repo validation completed successfully.")


if __name__ == "__main__":
    main()
