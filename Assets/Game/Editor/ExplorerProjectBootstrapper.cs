using System;
using System.IO;
using ExplorerGame.Animals;
using ExplorerGame.Core;
using ExplorerGame.Interaction;
using ExplorerGame.Player;
using ExplorerGame.UI;
using ExplorerGame.World;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ExplorerGame.Editor
{
    public static class ExplorerProjectBootstrapper
    {
        private const string SceneFolder = "Assets/Scenes";
        private const string PrefabFolder = "Assets/Resources/Prefabs";
        private const string CharacterPrefabFolder = PrefabFolder + "/Characters";
        private const string NpcPrefabFolder = PrefabFolder + "/NPCs";
        private const string AnimalPrefabFolder = PrefabFolder + "/Animals";
        private const string PropPrefabFolder = PrefabFolder + "/Props";
        private const string MaterialFolder = PrefabFolder + "/Materials";
        private static readonly Color VillageGroundColor = new Color(0.57f, 0.61f, 0.38f);
        private static readonly Color VillagePathColor = new Color(0.63f, 0.54f, 0.39f);
        private static readonly Color VillageWallColor = new Color(0.8f, 0.71f, 0.56f);
        private static readonly Color VillageWallAltColor = new Color(0.73f, 0.64f, 0.52f);
        private static readonly Color VillageRoofColor = new Color(0.43f, 0.24f, 0.2f);
        private static readonly Color WoodDarkColor = new Color(0.33f, 0.22f, 0.12f);
        private static readonly Color WoodSignColor = new Color(0.68f, 0.58f, 0.33f);
        private static readonly Color ForestGroundColor = new Color(0.26f, 0.37f, 0.23f);
        private static readonly Color ForestTrailColor = new Color(0.43f, 0.33f, 0.23f);
        private static readonly Color ForestCanopyColor = new Color(0.3f, 0.48f, 0.27f);
        private static readonly Color ForestPortalColor = new Color(0.39f, 0.61f, 0.35f);
        private static readonly Color StoneColor = new Color(0.49f, 0.49f, 0.48f);
        private static readonly Color StoneDarkColor = new Color(0.43f, 0.43f, 0.42f);
        private static readonly Color MountainGroundColor = new Color(0.46f, 0.44f, 0.39f);
        private static readonly Color MountainPathColor = new Color(0.58f, 0.52f, 0.43f);
        private static readonly Color MountainAccentColor = new Color(0.8f, 0.69f, 0.31f);
        private static readonly Color PortalFloorColor = new Color(0.5f, 0.43f, 0.3f);
        private static readonly Color PromptAccentColor = new Color(0.73f, 0.66f, 0.34f);
        private static readonly Color CharacterMaleColor = new Color(0.31f, 0.53f, 0.79f);
        private static readonly Color CharacterFemaleColor = new Color(0.83f, 0.43f, 0.32f);
        private static readonly Color CharacterAccentColor = new Color(0.89f, 0.84f, 0.74f);
        private static readonly Color NpcGuideColor = new Color(0.45f, 0.66f, 0.46f);
        private static readonly Color NpcGuideAccentColor = new Color(0.9f, 0.84f, 0.69f);
        private static readonly Color AnimalColor = new Color(0.58f, 0.45f, 0.31f);
        private static readonly Color BootstrapSkyAmbient = new Color(0.44f, 0.49f, 0.58f);
        private static readonly Color BootstrapEquatorAmbient = new Color(0.29f, 0.32f, 0.38f);
        private static readonly Color BootstrapGroundAmbient = new Color(0.12f, 0.11f, 0.1f);
        private static readonly Color CharacterSkyAmbient = new Color(0.54f, 0.58f, 0.64f);
        private static readonly Color CharacterEquatorAmbient = new Color(0.34f, 0.32f, 0.3f);
        private static readonly Color CharacterGroundAmbient = new Color(0.13f, 0.12f, 0.11f);
        private static readonly Color WorldSkyAmbient = new Color(0.52f, 0.57f, 0.51f);
        private static readonly Color WorldEquatorAmbient = new Color(0.31f, 0.33f, 0.28f);
        private static readonly Color WorldGroundAmbient = new Color(0.11f, 0.1f, 0.08f);
        private static readonly Color BootstrapFogColor = new Color(0.28f, 0.32f, 0.38f);
        private static readonly Color CharacterFogColor = new Color(0.35f, 0.34f, 0.33f);
        private static readonly Color WorldFogColor = new Color(0.48f, 0.47f, 0.39f);

        [MenuItem("Tools/Explorer Game/Generate Project Scaffolding")]
        public static void GenerateProjectScaffolding()
        {
            EnsureFolder(SceneFolder);
            EnsureFolder(GameConstants.ResourcesFolder);
            EnsureFolder(GameConstants.ConfigFolder);
            EnsureFolder(PrefabFolder);
            EnsureFolder(CharacterPrefabFolder);
            EnsureFolder(NpcPrefabFolder);
            EnsureFolder(AnimalPrefabFolder);
            EnsureFolder(PropPrefabFolder);
            EnsureFolder(MaterialFolder);
            CreateCatalogAssets();
            CreateBootstrapScene();
            CreateCharacterSelectScene();
            CreateWorldScene(GameConstants.WorldPersistentScene, "World Root");
            CreateWorldScene(GameConstants.VillageZoneScene, "Village Zone");
            CreateWorldScene(GameConstants.ForestZoneScene, "Forest Zone");
            CreateWorldScene(GameConstants.MountainZoneScene, "Mountain Zone");
            SaveBuildSettings();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ValidateConfigAssets();
            ValidateGeneratedScenes();
        }

        [MenuItem("Tools/Explorer Game/Validate Config Assets")]
        public static void ValidateConfigAssets()
        {
            var characterCatalog = Resources.Load<CharacterCatalog>(GameConstants.CharacterConfigPath);
            var worldCatalog = Resources.Load<WorldCatalog>(GameConstants.WorldConfigPath);

            if (characterCatalog == null)
            {
                throw new FileNotFoundException("Missing CharacterCatalog at Resources/Configs/CharacterCatalog.");
            }

            if (worldCatalog == null)
            {
                throw new FileNotFoundException("Missing WorldCatalog at Resources/Configs/WorldCatalog.");
            }

            characterCatalog.Validate();
            worldCatalog.Validate();
            Debug.Log("Explorer Game config validation completed successfully.");
        }

        [MenuItem("Tools/Explorer Game/Validate Generated Scenes")]
        public static void ValidateGeneratedScenes()
        {
            ValidateScene(GameConstants.BootstrapScene, typeof(GameSession), typeof(BootstrapFlowController), typeof(Camera));
            ValidateScene(GameConstants.CharacterSelectScene, typeof(CharacterSelectionView), typeof(Camera));
            ValidateScene(GameConstants.WorldPersistentScene, typeof(WorldRuntimeController), typeof(ThirdPersonCameraRig));
            ValidateScene(GameConstants.VillageZoneScene, typeof(DialogueNpc), typeof(ZonePortal));
            ValidateScene(GameConstants.ForestZoneScene, typeof(AnimalRoamingAgent), typeof(InspectableObject), typeof(ZonePortal));
            ValidateScene(GameConstants.MountainZoneScene, typeof(InspectableObject), typeof(ZonePortal));
            Debug.Log("Explorer Game scene validation completed successfully.");
        }

        private static void CreateCatalogAssets()
        {
            var characterCatalog = LoadOrCreateAsset<CharacterCatalog>($"{GameConstants.ConfigFolder}/CharacterCatalog.asset");
            var worldCatalog = LoadOrCreateAsset<WorldCatalog>($"{GameConstants.ConfigFolder}/WorldCatalog.asset");
            var maleCharacterPrefab = CreatePlaceholderCharacterPrefab("MaleExplorer", CharacterMaleColor);
            var femaleCharacterPrefab = CreatePlaceholderCharacterPrefab("FemaleExplorer", CharacterFemaleColor);

            var maleCharacter = CreateCharacterAsset(CharacterOption.Male, "Male Explorer", maleCharacterPrefab);
            var femaleCharacter = CreateCharacterAsset(CharacterOption.Female, "Female Explorer", femaleCharacterPrefab);
            AddCharacterIfMissing(characterCatalog, maleCharacter);
            AddCharacterIfMissing(characterCatalog, femaleCharacter);

            var villageZone = CreateZoneAsset("VillageZone", "Village", new Vector3(0.6f, 1.1f, -5.2f));
            var forestZone = CreateZoneAsset("ForestZone", "Forest", new Vector3(-0.2f, 1.1f, -6.1f));
            var mountainZone = CreateZoneAsset("MountainZone", "Mountain", new Vector3(0.2f, 1.1f, -5.4f));
            AddZoneIfMissing(worldCatalog, villageZone);
            AddZoneIfMissing(worldCatalog, forestZone);
            AddZoneIfMissing(worldCatalog, mountainZone);

            EditorUtility.SetDirty(characterCatalog);
            EditorUtility.SetDirty(worldCatalog);
        }

        private static CharacterDefinition CreateCharacterAsset(CharacterOption option, string displayName, GameObject prefab)
        {
            var path = $"{GameConstants.ConfigFolder}/{displayName.Replace(" ", string.Empty)}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<CharacterDefinition>(path);
            var character = existing != null ? existing : ScriptableObject.CreateInstance<CharacterDefinition>();
            var serializedCharacter = new SerializedObject(character);
            serializedCharacter.FindProperty("option").enumValueIndex = (int)option;
            serializedCharacter.FindProperty("displayName").stringValue = displayName;
            serializedCharacter.FindProperty("prefab").objectReferenceValue = prefab;
            serializedCharacter.ApplyModifiedPropertiesWithoutUndo();
            if (existing == null)
            {
                AssetDatabase.CreateAsset(character, path);
            }

            return character;
        }

        private static ZoneDefinition CreateZoneAsset(string sceneName, string displayName, Vector3 spawnPoint)
        {
            var path = $"{GameConstants.ConfigFolder}/{sceneName}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<ZoneDefinition>(path);
            var zone = existing != null ? existing : ScriptableObject.CreateInstance<ZoneDefinition>();
            var serializedZone = new SerializedObject(zone);
            serializedZone.FindProperty("sceneName").stringValue = sceneName;
            serializedZone.FindProperty("displayName").stringValue = displayName;
            serializedZone.FindProperty("description").stringValue = $"{displayName} exploration zone";
            serializedZone.FindProperty("playerSpawnPoint").vector3Value = spawnPoint;
            serializedZone.ApplyModifiedPropertiesWithoutUndo();
            if (existing == null)
            {
                AssetDatabase.CreateAsset(zone, path);
            }

            return zone;
        }

        private static void CreateBootstrapScene()
        {
            EnsureSceneWithSingleRoot(GameConstants.BootstrapScene, GameConstants.BootstrapScene, root =>
            {
                ConfigureBootstrapAtmosphere(root.scene);
                GetOrAddComponent<GameSession>(root);
                GetOrAddComponent<BootstrapFlowController>(root);
                EnsureOverlayCamera(root.scene, "BootstrapCamera", new Vector3(0f, 2f, -6f));
            });
        }

        private static void CreateCharacterSelectScene()
        {
            EnsureSceneWithSingleRoot(GameConstants.CharacterSelectScene, GameConstants.CharacterSelectScene, root =>
            {
                ConfigureCharacterSelectAtmosphere(root.scene);
                GetOrAddComponent<CharacterSelectionView>(root);
                EnsureOverlayCamera(root.scene, "CharacterSelectCamera", new Vector3(0f, 1.5f, -8f));
            });
        }

        private static void CreateWorldScene(string sceneName, string rootName)
        {
            EnsureSceneWithSingleRoot(sceneName, rootName, root =>
            {
                root.name = rootName;
                if (sceneName == GameConstants.WorldPersistentScene)
                {
                    ConfigureWorldAtmosphere(root.gameObject.scene);
                    GetOrAddComponent<WorldRuntimeController>(root);
                    CreateCameraRig(root.transform);
                }

                if (sceneName == GameConstants.VillageZoneScene)
                {
                    DressVillageZone(root.transform);
                    CreatePlaceholderNpc(
                        root.transform,
                        new Vector3(1.8f, 0f, -0.5f),
                        "Ask for directions",
                        "The forest trail is straight ahead. Follow the marked path past the sign and step through the green arch.");
                    CreateGuideSignpost(
                        root.transform,
                        "ForestTrailSign",
                        new Vector3(1.8f, 0f, 2.4f),
                        "Read forest sign",
                        "Forest trail ahead. Pass through the green arch to continue.");
                    CreateZonePortalAnchor(
                        root.transform,
                        "MountainTrailPortal",
                        new Vector3(5.2f, 0f, -4.2f),
                        new Vector3(2f, 2.2f, 0.8f),
                        MountainAccentColor,
                        GameConstants.MountainZoneScene);
                    CreateZonePortalAnchor(
                        root.transform,
                        "ForestTrailPortal",
                        new Vector3(0.4f, 0f, 8.2f),
                        new Vector3(2.2f, 2.2f, 0.8f),
                        ForestPortalColor,
                        GameConstants.ForestZoneScene);
                }

                if (sceneName == GameConstants.ForestZoneScene)
                {
                    DressForestZone(root.transform);
                    CreatePlaceholderAnimal(root.transform, new Vector3(-2.8f, 0f, 2.8f));
                    CreatePlaceholderAnimal(root.transform, new Vector3(2.9f, 0f, -0.4f));
                    CreateInspectable(
                        root.transform,
                        "ForestMarker",
                        new Vector3(1.5f, 0f, 6.1f),
                        "Inspect forest marker",
                        "The air is cooler here. Animal tracks cut between the trees and the trail bends back toward the village arch.");
                    CreateZonePortalAnchor(
                        root.transform,
                        "VillageReturnPortal",
                        new Vector3(-0.2f, 0f, -7.2f),
                        new Vector3(2.2f, 2.2f, 0.8f),
                        WoodSignColor,
                        GameConstants.VillageZoneScene);
                }

                if (sceneName == GameConstants.MountainZoneScene)
                {
                    DressMountainZone(root.transform);
                    CreateInspectable(
                        root.transform,
                        "MountainMarker",
                        new Vector3(1.4f, 0f, 5.4f),
                        "Inspect lookout marker",
                        "The higher path opens onto a quiet lookout. The mountain route is still rough, but the ridge is visible from here.");
                    CreateZonePortalAnchor(
                        root.transform,
                        "VillageReturnPortal",
                        new Vector3(0.2f, 0f, -6.2f),
                        new Vector3(2f, 2.2f, 0.8f),
                        WoodSignColor,
                        GameConstants.VillageZoneScene);
                }
            });
        }

        private static void CreateSceneWithSingleRoot(string sceneName, System.Action<GameObject> configureRoot)
        {
            var path = $"{SceneFolder}/{sceneName}.unity";
            if (File.Exists(path))
            {
                return;
            }

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SceneManager.SetActiveScene(scene);
            var root = new GameObject(sceneName);
            configureRoot(root);
            EditorSceneManager.SaveScene(scene, path);
        }

        private static void EnsureSceneWithSingleRoot(string sceneName, string rootName, Action<GameObject> configureRoot)
        {
            var path = $"{SceneFolder}/{sceneName}.unity";
            Scene scene;

            if (File.Exists(path))
            {
                scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            }
            else
            {
                scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            }

            SceneManager.SetActiveScene(scene);
            var root = GetOrCreateRoot(scene, rootName, sceneName);
            configureRoot(root);
            EditorSceneManager.SaveScene(scene, path);
        }

        private static void SaveBuildSettings()
        {
            EditorBuildSettings.scenes = new[]
            {
                BuildScene(GameConstants.BootstrapScene),
                BuildScene(GameConstants.CharacterSelectScene),
                BuildScene(GameConstants.WorldPersistentScene),
                BuildScene(GameConstants.VillageZoneScene),
                BuildScene(GameConstants.ForestZoneScene),
                BuildScene(GameConstants.MountainZoneScene)
            };
        }

        private static EditorBuildSettingsScene BuildScene(string sceneName)
        {
            return new EditorBuildSettingsScene($"{SceneFolder}/{sceneName}.unity", true);
        }

        private static GameObject GetOrCreateRoot(Scene scene, string rootName, params string[] aliases)
        {
            var roots = scene.GetRootGameObjects();
            GameObject found = null;
            foreach (var root in roots)
            {
                if (root.name != rootName && Array.IndexOf(aliases, root.name) < 0)
                {
                    continue;
                }

                if (found == null)
                {
                    found = root;
                    continue;
                }

                Object.DestroyImmediate(root);
            }

            if (found != null)
            {
                found.name = rootName;
                return found;
            }

            return new GameObject(rootName);
        }

        private static T GetOrAddComponent<T>(GameObject gameObject) where T : Component
        {
            var existing = gameObject.GetComponent<T>();
            return existing != null ? existing : gameObject.AddComponent<T>();
        }

        private static void EnsureOverlayCamera(Scene scene, string cameraName, Vector3 position)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.GetComponentInChildren<Camera>(true) != null)
                {
                    return;
                }
            }

            var cameraRoot = new GameObject(cameraName);
            var camera = cameraRoot.AddComponent<Camera>();
            cameraRoot.transform.position = position;
            cameraRoot.transform.rotation = Quaternion.Euler(10f, 0f, 0f);
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = scene.name == GameConstants.BootstrapScene
                ? BootstrapFogColor
                : CharacterFogColor;
            camera.fieldOfView = scene.name == GameConstants.CharacterSelectScene ? 38f : 45f;
            camera.tag = "MainCamera";
            cameraRoot.AddComponent<AudioListener>();
        }

        private static void ValidateScene(string sceneName, params Type[] requiredComponents)
        {
            var path = $"{SceneFolder}/{sceneName}.unity";
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Missing scene at {path}. Run Tools/Explorer Game/Generate Project Scaffolding.");
            }

            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            foreach (var componentType in requiredComponents)
            {
                if (!SceneContainsComponent(scene, componentType))
                {
                    throw new InvalidOperationException(
                        $"Scene '{sceneName}' is missing required component '{componentType.Name}'.");
                }
            }
        }

        private static bool SceneContainsComponent(Scene scene, Type componentType)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.GetComponentInChildren(componentType, true) != null)
                {
                    return true;
                }
            }

            return false;
        }

        private static GameObject CreatePlaceholderCharacterPrefab(string prefabName, Color color)
        {
            var path = $"{CharacterPrefabFolder}/{prefabName}.prefab";
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var root = existing != null ? PrefabUtility.LoadPrefabContents(path) : new GameObject(prefabName);
            root.name = prefabName;
            GetOrAddComponent<CharacterController>(root);
            GetOrAddComponent<ThirdPersonExplorerController>(root);
            GetOrAddComponent<InteractionProbe>(root);

            var visual = root.transform.Find("Visual")?.gameObject;
            if (visual == null)
            {
                visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                visual.name = "Visual";
            }

            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, 0.95f, 0f);
            visual.transform.localScale = new Vector3(0.78f, 1.6f, 0.72f);

            var visualCollider = visual.GetComponent<Collider>();
            if (visualCollider != null)
            {
                Object.DestroyImmediate(visualCollider);
            }

            var material = CreateMaterialAsset(prefabName, color);
            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = material;
            }

            CreatePrimitive(visual.transform, PrimitiveType.Sphere, "Head", new Vector3(0f, 0.88f, 0f), new Vector3(0.5f, 0.5f, 0.5f), CharacterAccentColor);
            CreatePrimitive(visual.transform, PrimitiveType.Cube, "Shoulders", new Vector3(0f, 0.4f, 0f), new Vector3(0.72f, 0.18f, 0.3f), color);
            CreatePrimitive(visual.transform, PrimitiveType.Cube, "Satchel", new Vector3(-0.3f, 0.12f, 0.16f), new Vector3(0.18f, 0.32f, 0.12f), WoodSignColor);

            if (existing == null)
            {
                var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
                Object.DestroyImmediate(root);
                return prefab;
            }

            var updatedPrefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            PrefabUtility.UnloadPrefabContents(root);
            return updatedPrefab;
        }

        private static void CreateCameraRig(Transform parent)
        {
            CleanupDuplicateNamedChildren(parent, "ThirdPersonCameraRig");
            GameObject cameraRig = null;
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                var child = parent.GetChild(i);
                if (child.name != "ThirdPersonCameraRig")
                {
                    continue;
                }

                if (cameraRig == null)
                {
                    cameraRig = child.gameObject;
                    continue;
                }

                Object.DestroyImmediate(child.gameObject);
            }

            cameraRig ??= new GameObject("ThirdPersonCameraRig");
            cameraRig.transform.SetParent(parent, false);
            cameraRig.transform.localPosition = new Vector3(0f, 2.5f, -4.5f);
            var camera = GetOrAddComponent<Camera>(cameraRig);
            cameraRig.tag = "MainCamera";
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = WorldFogColor;
            camera.fieldOfView = 50f;
            CleanupExtraSceneAudioListeners(parent.gameObject.scene, cameraRig);
            GetOrAddComponent<AudioListener>(cameraRig);
            GetOrAddComponent<ThirdPersonCameraRig>(cameraRig);
        }

        private static void ConfigureBootstrapAtmosphere(Scene scene)
        {
            ConfigureSceneAtmosphere(
                scene,
                BootstrapSkyAmbient,
                BootstrapEquatorAmbient,
                BootstrapGroundAmbient,
                BootstrapFogColor,
                0.015f,
                new Color(0.96f, 0.89f, 0.77f),
                1.1f,
                new Vector3(35f, -28f, 0f));
        }

        private static void ConfigureCharacterSelectAtmosphere(Scene scene)
        {
            ConfigureSceneAtmosphere(
                scene,
                CharacterSkyAmbient,
                CharacterEquatorAmbient,
                CharacterGroundAmbient,
                CharacterFogColor,
                0.018f,
                new Color(0.98f, 0.9f, 0.82f),
                1.2f,
                new Vector3(28f, -24f, 0f));
        }

        private static void ConfigureWorldAtmosphere(Scene scene)
        {
            ConfigureSceneAtmosphere(
                scene,
                WorldSkyAmbient,
                WorldEquatorAmbient,
                WorldGroundAmbient,
                WorldFogColor,
                0.012f,
                new Color(0.95f, 0.91f, 0.79f),
                1.15f,
                new Vector3(42f, -32f, 0f));
        }

        private static void ConfigureSceneAtmosphere(
            Scene scene,
            Color skyAmbient,
            Color equatorAmbient,
            Color groundAmbient,
            Color fogColor,
            float fogDensity,
            Color lightColor,
            float lightIntensity,
            Vector3 lightEuler)
        {
            SceneManager.SetActiveScene(scene);
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientSkyColor = skyAmbient;
            RenderSettings.ambientEquatorColor = equatorAmbient;
            RenderSettings.ambientGroundColor = groundAmbient;
            RenderSettings.ambientIntensity = 1f;
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.ExponentialSquared;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
            EnsureDirectionalLight(scene, "SceneKeyLight", lightColor, lightIntensity, lightEuler);
        }

        private static void EnsureDirectionalLight(
            Scene scene,
            string lightName,
            Color color,
            float intensity,
            Vector3 eulerAngles)
        {
            Light keyLight = null;
            foreach (var root in scene.GetRootGameObjects())
            {
                var lights = root.GetComponentsInChildren<Light>(true);
                foreach (var light in lights)
                {
                    if (light.type != LightType.Directional)
                    {
                        continue;
                    }

                    if (keyLight == null)
                    {
                        keyLight = light;
                        continue;
                    }

                    Object.DestroyImmediate(light.gameObject);
                }
            }

            if (keyLight == null)
            {
                var lightRoot = new GameObject(lightName);
                keyLight = lightRoot.AddComponent<Light>();
                keyLight.type = LightType.Directional;
            }

            keyLight.name = lightName;
            keyLight.color = color;
            keyLight.intensity = intensity;
            keyLight.shadows = LightShadows.Soft;
            keyLight.transform.position = Vector3.zero;
            keyLight.transform.rotation = Quaternion.Euler(eulerAngles);
        }

        private static void CleanupExtraSceneAudioListeners(Scene scene, GameObject cameraRig)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                var listeners = root.GetComponentsInChildren<AudioListener>(true);
                foreach (var listener in listeners)
                {
                    if (listener == null)
                    {
                        continue;
                    }

                    if (listener.gameObject == cameraRig)
                    {
                        continue;
                    }

                    Object.DestroyImmediate(listener);
                }
            }
        }

        private static void CleanupDuplicateNamedChildren(Transform parent, string childName)
        {
            Transform keep = null;
            for (var i = parent.childCount - 1; i >= 0; i--)
            {
                var child = parent.GetChild(i);
                if (child.name != childName)
                {
                    continue;
                }

                if (keep == null)
                {
                    keep = child;
                    continue;
                }

                Object.DestroyImmediate(child.gameObject);
            }
        }

        private static void DressVillageZone(Transform parent)
        {
            CreateGround(parent, "VillageGround", new Vector3(0f, -0.05f, 0.8f), new Vector3(14f, 0.1f, 20f), VillageGroundColor);
            CreateBlock(parent, "SouthLane", new Vector3(0.6f, 0.02f, -4.4f), new Vector3(2.4f, 0.04f, 4.4f), VillagePathColor);
            CreateBlock(parent, "VillageSquare", new Vector3(0.6f, 0.02f, 0.2f), new Vector3(3.8f, 0.04f, 4.8f), VillagePathColor);
            CreateBlock(parent, "NorthTrail", new Vector3(0.4f, 0.02f, 5.8f), new Vector3(2.2f, 0.04f, 6.2f), VillagePathColor);
            CreateBlock(parent, "MountainLane", new Vector3(3.8f, 0.02f, -3.4f), new Vector3(4.8f, 0.04f, 1.8f), VillagePathColor);
            CreateHouse(parent, "VillageHouseA", new Vector3(-4.8f, 0f, -1f), VillageWallColor);
            CreateHouse(parent, "VillageHouseB", new Vector3(5.4f, 0f, -3.4f), VillageWallAltColor);
            CreateHouse(parent, "VillageHouseC", new Vector3(-5.8f, 0f, 4.4f), VillageWallAltColor);
            CreateHouse(parent, "VillageHouseD", new Vector3(4.9f, 0f, 6.2f), VillageWallColor);
            CreateTree(parent, "VillageEdgeTreeA", new Vector3(-6.4f, 0f, 8.4f));
            CreateTree(parent, "VillageEdgeTreeB", new Vector3(6.1f, 0f, 9.2f));
            CreateTree(parent, "VillageEdgeTreeC", new Vector3(-6.8f, 0f, -6.2f));
            CreateTree(parent, "VillageEdgeTreeD", new Vector3(6.7f, 0f, -7.1f));
            CreateBlock(parent, "VillageWestFence", new Vector3(-6.9f, 0.7f, 1.4f), new Vector3(0.3f, 1.4f, 14.8f), WoodDarkColor);
            CreateBlock(parent, "VillageNorthFenceWest", new Vector3(-3.4f, 0.7f, 10.2f), new Vector3(6.4f, 1.4f, 0.3f), WoodDarkColor);
            CreateBlock(parent, "VillageNorthFenceEast", new Vector3(4.4f, 0.7f, 10.2f), new Vector3(4.6f, 1.4f, 0.3f), WoodDarkColor);
            CreateBlock(parent, "VillageEastFenceNorth", new Vector3(6.9f, 0.7f, 4.1f), new Vector3(0.3f, 1.4f, 9.6f), WoodDarkColor);
            CreateBlock(parent, "VillageEastFenceSouth", new Vector3(6.9f, 0.7f, -7f), new Vector3(0.3f, 1.4f, 2.4f), WoodDarkColor);
            CreateBlock(parent, "VillageSouthBermWest", new Vector3(-4.2f, 0.4f, -8.6f), new Vector3(4.4f, 0.8f, 1.2f), StoneColor);
            CreateBlock(parent, "VillageSouthBermEast", new Vector3(3.8f, 0.45f, -8.6f), new Vector3(3.8f, 0.9f, 1.2f), StoneDarkColor);
            CreateBlock(parent, "VillageWellBase", new Vector3(-1.5f, 0.45f, 1f), new Vector3(1.4f, 0.9f, 1.4f), StoneColor);
            CreateBlock(parent, "VillageWellCap", new Vector3(-1.5f, 1.12f, 1f), new Vector3(1.8f, 0.16f, 1.8f), WoodSignColor);
            CreateBlock(parent, "VillageSign", new Vector3(1.8f, 1.05f, -0.2f), new Vector3(0.34f, 1.1f, 0.1f), WoodDarkColor);
            CreateBlock(parent, "ForestTrailMarker", new Vector3(0.4f, 0.9f, 6.9f), new Vector3(0.32f, 1.8f, 0.32f), WoodDarkColor);
            CreateBlock(parent, "ForestTrailMarkerCap", new Vector3(0.4f, 1.95f, 6.9f), new Vector3(0.9f, 0.18f, 0.9f), PromptAccentColor);
            CreateBlock(parent, "MountainTrailMarker", new Vector3(4.5f, 0.9f, -4.2f), new Vector3(0.32f, 1.8f, 0.32f), WoodDarkColor);
            CreateBlock(parent, "MountainTrailMarkerCap", new Vector3(4.5f, 1.95f, -4.2f), new Vector3(0.9f, 0.18f, 0.9f), MountainAccentColor);
        }

        private static void DressForestZone(Transform parent)
        {
            CreateGround(parent, "ForestGround", new Vector3(0f, -0.05f, 0.8f), new Vector3(10f, 0.1f, 20f), ForestGroundColor);
            CreateBlock(parent, "ForestEntryTrail", new Vector3(-0.2f, 0.02f, -6f), new Vector3(1.8f, 0.04f, 4f), ForestTrailColor);
            CreateBlock(parent, "ForestBendTrail", new Vector3(0.7f, 0.02f, -1.1f), new Vector3(2.6f, 0.04f, 4.6f), ForestTrailColor);
            CreateBlock(parent, "ForestNorthTrail", new Vector3(0.2f, 0.02f, 5.3f), new Vector3(1.9f, 0.04f, 5.2f), ForestTrailColor);
            CreateTree(parent, "ForestTreeA", new Vector3(-4.8f, 0f, -4.2f));
            CreateTree(parent, "ForestTreeB", new Vector3(3.8f, 0f, -3.1f));
            CreateTree(parent, "ForestTreeC", new Vector3(-5.4f, 0f, 1.5f));
            CreateTree(parent, "ForestTreeD", new Vector3(5.2f, 0f, 2.6f));
            CreateTree(parent, "ForestTreeE", new Vector3(-3.8f, 0f, 7.4f));
            CreateTree(parent, "ForestTreeF", new Vector3(4.6f, 0f, 8.1f));
            CreateTree(parent, "ForestTreeG", new Vector3(-1.6f, 0f, 5.1f));
            CreateTree(parent, "ForestTreeH", new Vector3(2.8f, 0f, 4.6f));
            CreateTree(parent, "ForestWestScreenA", new Vector3(-7.1f, 0f, -5.8f));
            CreateTree(parent, "ForestWestScreenB", new Vector3(-7.4f, 0f, 0.5f));
            CreateTree(parent, "ForestWestScreenC", new Vector3(-7.2f, 0f, 7.6f));
            CreateTree(parent, "ForestEastScreenA", new Vector3(7f, 0f, -5.1f));
            CreateTree(parent, "ForestEastScreenB", new Vector3(7.3f, 0f, 1.4f));
            CreateTree(parent, "ForestEastScreenC", new Vector3(7.1f, 0f, 8.2f));
            CreateBlock(parent, "ForestRockA", new Vector3(-1.8f, 0.35f, -1.4f), new Vector3(0.9f, 0.7f, 0.8f), StoneColor);
            CreateBlock(parent, "ForestRockB", new Vector3(2.4f, 0.25f, 1.2f), new Vector3(0.8f, 0.5f, 1f), StoneDarkColor);
            CreateBlock(parent, "ForestRockC", new Vector3(-2.7f, 0.3f, 4.1f), new Vector3(1f, 0.6f, 0.9f), StoneColor);
            CreateBlock(parent, "ForestRockD", new Vector3(2f, 0.22f, 6.3f), new Vector3(0.7f, 0.45f, 0.8f), StoneDarkColor);
            CreateBlock(parent, "ForestNorthBermWest", new Vector3(-3.2f, 0.6f, 10.1f), new Vector3(4f, 1.2f, 1.2f), StoneColor);
            CreateBlock(parent, "ForestNorthBermEast", new Vector3(2.8f, 0.7f, 10.1f), new Vector3(4.4f, 1.4f, 1.2f), StoneDarkColor);
            CreateBlock(parent, "ForestSouthBermWest", new Vector3(-3.6f, 0.5f, -9.1f), new Vector3(3.4f, 1f, 1.1f), StoneColor);
            CreateBlock(parent, "ForestSouthBermEast", new Vector3(3.5f, 0.5f, -9.1f), new Vector3(4.1f, 1f, 1.1f), StoneDarkColor);
            CreateBlock(parent, "ForestLookoutMarker", new Vector3(0.4f, 1.45f, 7.6f), new Vector3(0.72f, 2.9f, 0.72f), PromptAccentColor);
            CreateBlock(parent, "ForestLookoutMarkerTop", new Vector3(0.4f, 3.1f, 7.6f), new Vector3(1.1f, 0.22f, 1.1f), WoodSignColor);
            CreateBlock(parent, "VillageMarker", new Vector3(-0.2f, 0.75f, -7.6f), new Vector3(0.3f, 1.5f, 0.3f), WoodDarkColor);
        }

        private static void DressMountainZone(Transform parent)
        {
            CreateGround(parent, "MountainGround", new Vector3(0f, -0.05f, 0.8f), new Vector3(8f, 0.1f, 16f), MountainGroundColor);
            CreateBlock(parent, "CliffA", new Vector3(-4.6f, 1.5f, -0.5f), new Vector3(1.8f, 3f, 3f), StoneColor);
            CreateBlock(parent, "CliffB", new Vector3(4.8f, 1.8f, 1.9f), new Vector3(1.7f, 3.6f, 3f), StoneDarkColor);
            CreateBlock(parent, "CliffC", new Vector3(-3.8f, 1.2f, 4.6f), new Vector3(1.5f, 2.4f, 2.6f), StoneColor);
            CreateBlock(parent, "CliffD", new Vector3(3.7f, 1.4f, -4.2f), new Vector3(1.4f, 2.8f, 2.4f), StoneDarkColor);
            CreateBlock(parent, "NorthRidgeA", new Vector3(-2.6f, 1.6f, 9.4f), new Vector3(3f, 3.2f, 1.8f), StoneColor);
            CreateBlock(parent, "NorthRidgeB", new Vector3(2.5f, 1.8f, 9.6f), new Vector3(3.4f, 3.6f, 1.8f), StoneDarkColor);
            CreateBlock(parent, "WestWallA", new Vector3(-5.8f, 1.3f, -5.2f), new Vector3(1.8f, 2.6f, 3f), StoneColor);
            CreateBlock(parent, "WestWallB", new Vector3(-5.9f, 1.5f, 0.8f), new Vector3(1.8f, 3f, 3.6f), StoneDarkColor);
            CreateBlock(parent, "EastWallA", new Vector3(5.7f, 1.4f, -2.8f), new Vector3(1.6f, 2.8f, 3f), StoneDarkColor);
            CreateBlock(parent, "EastWallB", new Vector3(5.8f, 1.7f, 4.6f), new Vector3(1.8f, 3.2f, 3.2f), StoneColor);
            CreateBlock(parent, "SouthShoulderLeft", new Vector3(-2.4f, 1f, -8.1f), new Vector3(2.6f, 2f, 1.6f), StoneColor);
            CreateBlock(parent, "SouthShoulderRight", new Vector3(2.8f, 1.1f, -8.1f), new Vector3(2.8f, 2.2f, 1.6f), StoneDarkColor);
            CreateBlock(parent, "MountainEntryPath", new Vector3(0.2f, 0.05f, -5.1f), new Vector3(1.4f, 0.08f, 3.6f), MountainPathColor);
            CreateBlock(parent, "MountainRidgePath", new Vector3(-0.1f, 0.05f, -0.2f), new Vector3(1.8f, 0.08f, 6.6f), MountainPathColor);
            CreateBlock(parent, "LookoutPath", new Vector3(1.6f, 0.05f, 4.5f), new Vector3(0.9f, 0.08f, 3.4f), MountainPathColor);
            CreateBlock(parent, "MountainReturnMarker", new Vector3(0.2f, 0.8f, -6.8f), new Vector3(0.3f, 1.6f, 0.3f), WoodDarkColor);
            CreateBlock(parent, "MountainReturnMarkerTop", new Vector3(0.2f, 1.82f, -6.8f), new Vector3(0.82f, 0.16f, 0.82f), WoodSignColor);
            CreateBlock(parent, "LookoutStone", new Vector3(2.3f, 0.3f, 5.9f), new Vector3(1f, 0.6f, 1f), StoneColor);
            CreateBlock(parent, "LookoutCairnBase", new Vector3(-0.8f, 0.22f, 3.8f), new Vector3(0.6f, 0.45f, 0.6f), StoneDarkColor);
            CreateBlock(parent, "LookoutCairnTop", new Vector3(-0.8f, 0.58f, 3.8f), new Vector3(0.32f, 0.26f, 0.32f), StoneColor);
            CreateBlock(parent, "LookoutBoulderA", new Vector3(-2.1f, 0.35f, 6.6f), new Vector3(0.9f, 0.7f, 0.9f), StoneDarkColor);
            CreateBlock(parent, "LookoutBoulderB", new Vector3(2.4f, 0.28f, 7.1f), new Vector3(0.8f, 0.55f, 0.8f), StoneColor);
            CreateBlock(parent, "MountainBeacon", new Vector3(0.1f, 1.35f, 7.2f), new Vector3(0.65f, 2.7f, 0.65f), MountainAccentColor);
            CreateBlock(parent, "MountainBeaconTop", new Vector3(0.1f, 2.95f, 7.2f), new Vector3(1.1f, 0.22f, 1.1f), WoodSignColor);
        }

        private static void CreatePlaceholderNpc(Transform parent, Vector3 localPosition, string promptText, string dialogueText)
        {
            var npcPrefab = CreatePlaceholderNpcPrefab();
            if (npcPrefab == null)
            {
                return;
            }

            var instance = parent.Find("GuideNpc")?.gameObject;
            if (instance == null)
            {
                instance = PrefabUtility.InstantiatePrefab(npcPrefab) as GameObject;
            }

            if (instance == null)
            {
                return;
            }

            instance.name = "GuideNpc";
            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;

            var npc = GetOrAddComponent<DialogueNpc>(instance);
            var serializedNpc = new SerializedObject(npc);
            serializedNpc.FindProperty("promptText").stringValue = promptText;
            serializedNpc.FindProperty("dialogueText").stringValue = dialogueText;
            serializedNpc.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void CreatePlaceholderAnimal(Transform parent, Vector3 localPosition)
        {
            var animalPrefab = CreatePlaceholderAnimalPrefab();
            if (animalPrefab == null)
            {
                return;
            }

            var instance = parent.Find($"Animal_{localPosition.x}_{localPosition.z}")?.gameObject;
            if (instance == null)
            {
                instance = PrefabUtility.InstantiatePrefab(animalPrefab) as GameObject;
            }

            if (instance == null)
            {
                return;
            }

            instance.name = $"Animal_{localPosition.x}_{localPosition.z}";
            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;
        }

        private static void CreateInspectable(
            Transform parent,
            string instanceName,
            Vector3 localPosition,
            string promptText,
            string descriptionText)
        {
            var inspectablePrefab = CreateInspectablePrefab();
            if (inspectablePrefab == null)
            {
                return;
            }

            var instance = parent.Find(instanceName)?.gameObject;
            if (instance == null)
            {
                instance = PrefabUtility.InstantiatePrefab(inspectablePrefab) as GameObject;
            }

            if (instance == null)
            {
                return;
            }

            instance.name = instanceName;
            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;

            var inspectable = GetOrAddComponent<InspectableObject>(instance);
            var serializedInspectable = new SerializedObject(inspectable);
            serializedInspectable.FindProperty("promptText").stringValue = promptText;
            serializedInspectable.FindProperty("descriptionText").stringValue = descriptionText;
            serializedInspectable.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void CreateGuideSignpost(
            Transform parent,
            string instanceName,
            Vector3 localPosition,
            string promptText,
            string descriptionText)
        {
            var root = parent.Find(instanceName)?.gameObject;
            if (root == null)
            {
                root = new GameObject(instanceName);
            }

            root.transform.SetParent(parent, false);
            root.transform.localPosition = localPosition;
            root.transform.localRotation = Quaternion.identity;

            var collider = GetOrAddComponent<BoxCollider>(root);
            collider.isTrigger = false;
            collider.center = new Vector3(0f, 1.15f, 0f);
            collider.size = new Vector3(0.95f, 2.4f, 0.35f);

            var inspectable = GetOrAddComponent<InspectableObject>(root);
            var serializedInspectable = new SerializedObject(inspectable);
            serializedInspectable.FindProperty("promptText").stringValue = promptText;
            serializedInspectable.FindProperty("descriptionText").stringValue = descriptionText;
            serializedInspectable.ApplyModifiedPropertiesWithoutUndo();

            CreatePortalPillar(root.transform, "Post", new Vector3(0f, 0.95f, 0f), new Vector3(0.18f, 1.9f, 0.18f), WoodDarkColor);
            CreatePortalPillar(root.transform, "Board", new Vector3(0f, 1.65f, 0f), new Vector3(1.05f, 0.55f, 0.14f), WoodSignColor);
            CreatePortalPillar(root.transform, "Cap", new Vector3(0f, 2.15f, 0f), new Vector3(1.2f, 0.12f, 0.22f), PromptAccentColor);
        }

        private static void CreateZonePortalAnchor(
            Transform parent,
            string instanceName,
            Vector3 localPosition,
            Vector3 archScale,
            Color color,
            string targetScene)
        {
            var root = parent.Find(instanceName)?.gameObject;
            if (root == null)
            {
                root = new GameObject(instanceName);
            }

            root.transform.SetParent(parent, false);
            root.transform.localPosition = localPosition;
            root.transform.localRotation = Quaternion.identity;

            var portal = GetOrAddComponent<ZonePortal>(root);
            var serializedPortal = new SerializedObject(portal);
            serializedPortal.FindProperty("targetZoneScene").stringValue = targetScene;
            serializedPortal.FindProperty("requireTriggerEnter").boolValue = true;
            serializedPortal.ApplyModifiedPropertiesWithoutUndo();

            var trigger = GetOrAddComponent<BoxCollider>(root);
            trigger.isTrigger = true;
            trigger.center = new Vector3(0f, 1.35f, 0f);
            trigger.size = new Vector3(1.9f, 2.8f, 1.2f);

            CreatePortalPillar(root.transform, "LeftPillar", new Vector3(-0.85f, 1.25f, 0f), new Vector3(0.42f, 2.5f, 0.42f), color);
            CreatePortalPillar(root.transform, "RightPillar", new Vector3(0.85f, 1.25f, 0f), new Vector3(0.42f, 2.5f, 0.42f), color);
            CreatePortalPillar(root.transform, "TopBeam", new Vector3(0f, 2.45f, 0f), new Vector3(2.2f, 0.34f, 0.42f), color);
            CreatePortalPillar(root.transform, "Crest", new Vector3(0f, 2.95f, 0f), new Vector3(0.6f, 0.55f, 0.6f), PromptAccentColor);
            CreateBlock(root.transform, "PortalFloor", new Vector3(0f, 0.02f, 0f), new Vector3(archScale.x, 0.04f, archScale.z), PortalFloorColor);
        }

        private static void CreatePortalPillar(Transform parent, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            var pillar = parent.Find(name)?.gameObject;
            if (pillar == null)
            {
                pillar = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pillar.name = name;
            }

            pillar.transform.SetParent(parent, false);
            pillar.transform.localPosition = localPosition;
            pillar.transform.localRotation = Quaternion.identity;
            pillar.transform.localScale = localScale;

            var renderer = pillar.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = CreateMaterialAsset($"{parent.name}_{name}", color);
            }
        }

        private static GameObject CreatePlaceholderNpcPrefab()
        {
            var path = $"{NpcPrefabFolder}/GuideNpc.prefab";
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject("GuideNpc");
            root.AddComponent<CapsuleCollider>();
            root.AddComponent<DialogueNpc>();

            var visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Visual";
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, 0.98f, 0f);
            visual.transform.localScale = new Vector3(0.82f, 1.7f, 0.78f);

            var visualCollider = visual.GetComponent<Collider>();
            if (visualCollider != null)
            {
                Object.DestroyImmediate(visualCollider);
            }

            var material = CreateMaterialAsset("GuideNpc", NpcGuideColor);
            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = material;
            }

            CreatePrimitive(visual.transform, PrimitiveType.Sphere, "Head", new Vector3(0f, 0.9f, 0f), new Vector3(0.48f, 0.48f, 0.48f), NpcGuideAccentColor);
            CreatePrimitive(visual.transform, PrimitiveType.Cube, "Arms", new Vector3(0f, 0.34f, 0f), new Vector3(0.84f, 0.16f, 0.22f), NpcGuideColor);
            CreatePrimitive(visual.transform, PrimitiveType.Cylinder, "Staff", new Vector3(0.34f, 0.12f, 0f), new Vector3(0.08f, 0.7f, 0.08f), WoodDarkColor);

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            Object.DestroyImmediate(root);
            return prefab;
        }

        private static GameObject CreatePlaceholderAnimalPrefab()
        {
            var path = $"{AnimalPrefabFolder}/ForestAnimal.prefab";
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject("ForestAnimal");
            root.AddComponent<CapsuleCollider>();
            var agent = root.AddComponent<NavMeshAgent>();
            agent.speed = 1.5f;
            agent.angularSpeed = 180f;
            agent.acceleration = 4f;
            agent.stoppingDistance = 0.1f;
            root.AddComponent<AnimalRoamingAgent>();

            var visual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            visual.name = "Visual";
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, 0.45f, 0f);
            visual.transform.localScale = new Vector3(0.9f, 0.65f, 1.2f);

            var visualCollider = visual.GetComponent<Collider>();
            if (visualCollider != null)
            {
                Object.DestroyImmediate(visualCollider);
            }

            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = CreateMaterialAsset("ForestAnimal", AnimalColor);
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            Object.DestroyImmediate(root);
            return prefab;
        }

        private static GameObject CreateInspectablePrefab()
        {
            var path = $"{PropPrefabFolder}/InspectableMarker.prefab";
            var existing = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject("InspectableMarker");
            root.AddComponent<BoxCollider>();
            root.AddComponent<InspectableObject>();

            var visual = GameObject.CreatePrimitive(PrimitiveType.Cube);
            visual.name = "Visual";
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, 0.35f, 0f);
            visual.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

            var visualCollider = visual.GetComponent<Collider>();
            if (visualCollider != null)
            {
                Object.DestroyImmediate(visualCollider);
            }

            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = CreateMaterialAsset("InspectableMarker", PromptAccentColor);
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            Object.DestroyImmediate(root);
            return prefab;
        }

        private static Material CreateMaterialAsset(string assetName, Color color)
        {
            var path = $"{MaterialFolder}/{assetName}.mat";
            var existing = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (existing != null)
            {
                if (existing.color != color)
                {
                    existing.color = color;
                    EditorUtility.SetDirty(existing);
                }

                return existing;
            }

            var shader = Shader.Find("Universal Render Pipeline/Lit");
            shader ??= Shader.Find("Standard");

            var material = new Material(shader)
            {
                color = color
            };

            AssetDatabase.CreateAsset(material, path);
            return material;
        }

        private static void CreateGround(Transform parent, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            CreateBlock(parent, name, localPosition, localScale, color);
        }

        private static void CreateHouse(Transform parent, string name, Vector3 localPosition, Color color)
        {
            var houseRoot = parent.Find(name)?.gameObject ?? new GameObject(name);
            houseRoot.transform.SetParent(parent, false);
            houseRoot.transform.localPosition = localPosition;

            CreatePrimitive(houseRoot.transform, PrimitiveType.Cube, "Body", new Vector3(0f, 0.75f, 0f), new Vector3(1.6f, 1.5f, 1.4f), color);
            CreatePrimitive(houseRoot.transform, PrimitiveType.Cube, "Roof", new Vector3(0f, 1.55f, 0f), new Vector3(1.8f, 0.3f, 1.6f), VillageRoofColor);
        }

        private static void CreateTree(Transform parent, string name, Vector3 localPosition)
        {
            var treeRoot = parent.Find(name)?.gameObject ?? new GameObject(name);
            treeRoot.transform.SetParent(parent, false);
            treeRoot.transform.localPosition = localPosition;

            CreatePrimitive(treeRoot.transform, PrimitiveType.Cylinder, "Trunk", new Vector3(0f, 1f, 0f), new Vector3(0.25f, 1f, 0.25f), WoodDarkColor);
            CreatePrimitive(treeRoot.transform, PrimitiveType.Sphere, "Canopy", new Vector3(0f, 2.3f, 0f), new Vector3(1.4f, 1.2f, 1.4f), ForestCanopyColor);
        }

        private static void CreateBlock(Transform parent, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            CreatePrimitive(parent, PrimitiveType.Cube, name, localPosition, localScale, color);
        }

        private static GameObject CreatePrimitive(Transform parent, PrimitiveType type, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            var instance = parent.Find(name)?.gameObject;
            if (instance == null)
            {
                instance = GameObject.CreatePrimitive(type);
                instance.name = name;
            }

            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;
            instance.transform.localScale = localScale;

            var renderer = instance.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = CreateMaterialAsset(name, color);
            }

            return instance;
        }

        private static T LoadOrCreateAsset<T>(string path) where T : ScriptableObject
        {
            var asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
            {
                return asset;
            }

            asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            return asset;
        }

        private static void AddCharacterIfMissing(CharacterCatalog catalog, CharacterDefinition definition)
        {
            var serializedCatalog = new SerializedObject(catalog);
            var characters = serializedCatalog.FindProperty("characters");

            for (var i = 0; i < characters.arraySize; i++)
            {
                if (characters.GetArrayElementAtIndex(i).objectReferenceValue == definition)
                {
                    return;
                }
            }

            characters.InsertArrayElementAtIndex(characters.arraySize);
            characters.GetArrayElementAtIndex(characters.arraySize - 1).objectReferenceValue = definition;
            serializedCatalog.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void AddZoneIfMissing(WorldCatalog catalog, ZoneDefinition definition)
        {
            var serializedCatalog = new SerializedObject(catalog);
            var zones = serializedCatalog.FindProperty("zones");

            for (var i = 0; i < zones.arraySize; i++)
            {
                if (zones.GetArrayElementAtIndex(i).objectReferenceValue == definition)
                {
                    return;
                }
            }

            zones.InsertArrayElementAtIndex(zones.arraySize);
            zones.GetArrayElementAtIndex(zones.arraySize - 1).objectReferenceValue = definition;
            serializedCatalog.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void EnsureFolder(string folderPath)
        {
            if (AssetDatabase.IsValidFolder(folderPath))
            {
                return;
            }

            var parentPath = Path.GetDirectoryName(folderPath)?.Replace("\\", "/");
            if (!string.IsNullOrEmpty(parentPath) && !AssetDatabase.IsValidFolder(parentPath))
            {
                EnsureFolder(parentPath);
            }

            var folderName = Path.GetFileName(folderPath);
            AssetDatabase.CreateFolder(parentPath ?? "Assets", folderName);
        }
    }
}
