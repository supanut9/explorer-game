#!/usr/bin/env python3
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parents[1]

SCENES = [
    "Bootstrap",
    "CharacterSelect",
    "WorldPersistent",
    "VillageZone",
    "ForestZone",
    "MountainZone",
]

REQUIRED_ASSETS = [
    "Assets/Resources/Configs/CharacterCatalog.asset",
    "Assets/Resources/Configs/WorldCatalog.asset",
    "Assets/Resources/Prefabs/Characters/MaleExplorer.prefab",
    "Assets/Resources/Prefabs/Characters/FemaleExplorer.prefab",
]

SCENE_TEXT_CHECKS = {
    "Assets/Scenes/Bootstrap.unity": [
        "GameSession",
        "BootstrapFlowController",
    ],
    "Assets/Scenes/CharacterSelect.unity": [
        "CharacterSelectionView",
    ],
    "Assets/Scenes/WorldPersistent.unity": [
        "WorldRuntimeController",
        "ThirdPersonCameraRig",
    ],
    "Assets/Scenes/VillageZone.unity": [
        "GuideNpc",
        "ForestTrailPortal",
        "ForestTrailSign",
        "MountainTrailSign",
    ],
    "Assets/Scenes/ForestZone.unity": [
        "VillageReturnPortal",
        "ForestMarker",
    ],
    "Assets/Scenes/MountainZone.unity": [
        "MountainMarker",
        "MountainBeacon",
    ],
}


def fail(message: str) -> None:
    print(f"ERROR: {message}", file=sys.stderr)
    raise SystemExit(1)


def ensure_paths() -> None:
    for relative_path in REQUIRED_ASSETS:
        if not (ROOT / relative_path).exists():
            fail(f"required Unity asset is missing: {relative_path}")

    for scene_name in SCENES:
        scene_path = ROOT / "Assets/Scenes" / f"{scene_name}.unity"
        if not scene_path.exists():
            fail(f"required scene is missing: {scene_path.relative_to(ROOT)}")


def validate_build_settings() -> None:
    build_settings_path = ROOT / "ProjectSettings/EditorBuildSettings.asset"
    if not build_settings_path.exists():
        fail("missing ProjectSettings/EditorBuildSettings.asset")

    text = build_settings_path.read_text(encoding="utf-8")
    for scene_name in SCENES:
        expected_path = f"Assets/Scenes/{scene_name}.unity"
        if expected_path not in text:
            fail(f"build settings missing scene: {expected_path}")


def validate_scene_content() -> None:
    for relative_path, markers in SCENE_TEXT_CHECKS.items():
        scene_path = ROOT / relative_path
        text = scene_path.read_text(encoding="utf-8")
        for marker in markers:
            if marker not in text:
                fail(f"scene {relative_path} is missing expected marker: {marker}")


def main() -> None:
    ensure_paths()
    validate_build_settings()
    validate_scene_content()
    print("Unity project validation completed successfully.")


if __name__ == "__main__":
    main()
