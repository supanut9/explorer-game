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
            ValidateScene(GameConstants.VillageZoneScene, typeof(DialogueNpc));
            ValidateScene(GameConstants.ForestZoneScene, typeof(AnimalRoamingAgent), typeof(InspectableObject));
            ValidateScene(GameConstants.MountainZoneScene, typeof(InspectableObject));
            Debug.Log("Explorer Game scene validation completed successfully.");
        }

        private static void CreateCatalogAssets()
        {
            var characterCatalog = LoadOrCreateAsset<CharacterCatalog>($"{GameConstants.ConfigFolder}/CharacterCatalog.asset");
            var worldCatalog = LoadOrCreateAsset<WorldCatalog>($"{GameConstants.ConfigFolder}/WorldCatalog.asset");
            var maleCharacterPrefab = CreatePlaceholderCharacterPrefab("MaleExplorer", new Color(0.27f, 0.54f, 0.88f));
            var femaleCharacterPrefab = CreatePlaceholderCharacterPrefab("FemaleExplorer", new Color(0.88f, 0.45f, 0.31f));

            var maleCharacter = CreateCharacterAsset(CharacterOption.Male, "Male Explorer", maleCharacterPrefab);
            var femaleCharacter = CreateCharacterAsset(CharacterOption.Female, "Female Explorer", femaleCharacterPrefab);
            AddCharacterIfMissing(characterCatalog, maleCharacter);
            AddCharacterIfMissing(characterCatalog, femaleCharacter);

            var villageZone = CreateZoneAsset("VillageZone", "Village", new Vector3(0f, 1.1f, 0f));
            var forestZone = CreateZoneAsset("ForestZone", "Forest", new Vector3(0f, 1.1f, 0f));
            var mountainZone = CreateZoneAsset("MountainZone", "Mountain", new Vector3(0f, 1.1f, 0f));
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
            EnsureSceneWithSingleRoot(GameConstants.BootstrapScene, root =>
            {
                GetOrAddComponent<GameSession>(root);
                GetOrAddComponent<BootstrapFlowController>(root);
                EnsureOverlayCamera(root.scene, "BootstrapCamera", new Vector3(0f, 2f, -6f));
            });
        }

        private static void CreateCharacterSelectScene()
        {
            EnsureSceneWithSingleRoot(GameConstants.CharacterSelectScene, root =>
            {
                GetOrAddComponent<CharacterSelectionView>(root);
                EnsureOverlayCamera(root.scene, "CharacterSelectCamera", new Vector3(0f, 1.5f, -8f));
            });
        }

        private static void CreateWorldScene(string sceneName, string rootName)
        {
            CreateSceneWithSingleRoot(sceneName, root =>
            {
                root.name = rootName;
                if (sceneName == GameConstants.WorldPersistentScene)
                {
                    root.AddComponent<WorldRuntimeController>();
                    CreateCameraRig(root.transform);
                }

                if (sceneName == GameConstants.VillageZoneScene)
                {
                    DressVillageZone(root.transform);
                    CreatePlaceholderNpc(root.transform, new Vector3(3f, 0f, 2f));
                }

                if (sceneName == GameConstants.ForestZoneScene)
                {
                    DressForestZone(root.transform);
                    CreatePlaceholderAnimal(root.transform, new Vector3(-2.5f, 0f, 1.5f));
                    CreatePlaceholderAnimal(root.transform, new Vector3(2.2f, 0f, -1.2f));
                    CreateInspectable(root.transform, "ForestMarker", new Vector3(1.2f, 0f, 3f));
                }

                if (sceneName == GameConstants.MountainZoneScene)
                {
                    DressMountainZone(root.transform);
                    CreateInspectable(root.transform, "MountainMarker", new Vector3(-1.8f, 0f, -2.4f));
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

        private static void EnsureSceneWithSingleRoot(string sceneName, Action<GameObject> configureRoot)
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
            var root = GetOrCreateRoot(scene, sceneName);
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

        private static GameObject GetOrCreateRoot(Scene scene, string rootName)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                if (root.name == rootName)
                {
                    return root;
                }
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
            camera.clearFlags = CameraClearFlags.Skybox;
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
            if (existing != null)
            {
                return existing;
            }

            var root = new GameObject(prefabName);
            root.AddComponent<CharacterController>();
            root.AddComponent<ThirdPersonExplorerController>();

            var visual = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            visual.name = "Visual";
            visual.transform.SetParent(root.transform, false);
            visual.transform.localPosition = new Vector3(0f, 1f, 0f);
            visual.transform.localScale = new Vector3(1f, 2f, 1f);

            var material = CreateMaterialAsset(prefabName, color);
            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = material;
            }

            var prefab = PrefabUtility.SaveAsPrefabAsset(root, path);
            Object.DestroyImmediate(root);
            return prefab;
        }

        private static void CreateCameraRig(Transform parent)
        {
            var cameraRig = new GameObject("ThirdPersonCameraRig");
            cameraRig.transform.SetParent(parent, false);
            cameraRig.transform.position = new Vector3(0f, 2.5f, -4.5f);
            cameraRig.AddComponent<Camera>();
            cameraRig.AddComponent<AudioListener>();
            cameraRig.AddComponent<ThirdPersonCameraRig>();
        }

        private static void DressVillageZone(Transform parent)
        {
            CreateGround(parent, "VillageGround", new Vector3(0f, -0.05f, 0f), new Vector3(2.5f, 0.1f, 2.5f), new Color(0.42f, 0.64f, 0.35f));
            CreateBlock(parent, "MainPath", new Vector3(0f, 0.02f, 0f), new Vector3(1.4f, 0.04f, 0.4f), new Color(0.58f, 0.49f, 0.38f));
            CreateHouse(parent, "VillageHouseA", new Vector3(-4f, 0f, 2f), new Color(0.82f, 0.73f, 0.58f));
            CreateHouse(parent, "VillageHouseB", new Vector3(4f, 0f, -1.5f), new Color(0.75f, 0.66f, 0.54f));
            CreateBlock(parent, "VillageSign", new Vector3(1.5f, 0.8f, 1.2f), new Vector3(0.3f, 0.8f, 0.08f), new Color(0.41f, 0.28f, 0.15f));
        }

        private static void DressForestZone(Transform parent)
        {
            CreateGround(parent, "ForestGround", new Vector3(0f, -0.05f, 0f), new Vector3(3f, 0.1f, 3f), new Color(0.22f, 0.46f, 0.24f));
            CreateTree(parent, "ForestTreeA", new Vector3(-4f, 0f, 3f));
            CreateTree(parent, "ForestTreeB", new Vector3(3f, 0f, -2f));
            CreateTree(parent, "ForestTreeC", new Vector3(5f, 0f, 4f));
            CreateBlock(parent, "ForestRockA", new Vector3(-1.5f, 0.35f, -3f), new Vector3(0.8f, 0.7f, 0.7f), new Color(0.45f, 0.47f, 0.48f));
            CreateBlock(parent, "ForestRockB", new Vector3(2.5f, 0.25f, 1.5f), new Vector3(0.6f, 0.5f, 0.9f), new Color(0.43f, 0.45f, 0.46f));
        }

        private static void DressMountainZone(Transform parent)
        {
            CreateGround(parent, "MountainGround", new Vector3(0f, -0.05f, 0f), new Vector3(3f, 0.1f, 3f), new Color(0.38f, 0.38f, 0.36f));
            CreateBlock(parent, "CliffA", new Vector3(-4f, 1.2f, 2f), new Vector3(1.6f, 2.4f, 2f), new Color(0.47f, 0.47f, 0.46f));
            CreateBlock(parent, "CliffB", new Vector3(3.5f, 1.6f, -1.5f), new Vector3(1.4f, 3.2f, 1.8f), new Color(0.44f, 0.44f, 0.43f));
            CreateBlock(parent, "MountainPath", new Vector3(0f, 0.05f, 1.5f), new Vector3(0.6f, 0.08f, 2.1f), new Color(0.54f, 0.5f, 0.43f));
            CreateBlock(parent, "LookoutStone", new Vector3(1.8f, 0.3f, -2.8f), new Vector3(0.9f, 0.6f, 0.9f), new Color(0.5f, 0.5f, 0.49f));
        }

        private static void CreatePlaceholderNpc(Transform parent, Vector3 localPosition)
        {
            var npcPrefab = CreatePlaceholderNpcPrefab();
            if (npcPrefab == null)
            {
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(npcPrefab) as GameObject;
            if (instance == null)
            {
                return;
            }

            instance.name = "GuideNpc";
            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;
        }

        private static void CreatePlaceholderAnimal(Transform parent, Vector3 localPosition)
        {
            var animalPrefab = CreatePlaceholderAnimalPrefab();
            if (animalPrefab == null)
            {
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(animalPrefab) as GameObject;
            if (instance == null)
            {
                return;
            }

            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;
        }

        private static void CreateInspectable(Transform parent, string instanceName, Vector3 localPosition)
        {
            var inspectablePrefab = CreateInspectablePrefab();
            if (inspectablePrefab == null)
            {
                return;
            }

            var instance = PrefabUtility.InstantiatePrefab(inspectablePrefab) as GameObject;
            if (instance == null)
            {
                return;
            }

            instance.name = instanceName;
            instance.transform.SetParent(parent, false);
            instance.transform.localPosition = localPosition;
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
            visual.transform.localPosition = new Vector3(0f, 1f, 0f);
            visual.transform.localScale = new Vector3(1f, 2f, 1f);

            var visualCollider = visual.GetComponent<Collider>();
            if (visualCollider != null)
            {
                Object.DestroyImmediate(visualCollider);
            }

            var material = CreateMaterialAsset("GuideNpc", new Color(0.35f, 0.75f, 0.48f));
            var renderer = visual.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sharedMaterial = material;
            }

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
                renderer.sharedMaterial = CreateMaterialAsset("ForestAnimal", new Color(0.62f, 0.48f, 0.34f));
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
                renderer.sharedMaterial = CreateMaterialAsset("InspectableMarker", new Color(0.72f, 0.63f, 0.3f));
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
            var houseRoot = new GameObject(name);
            houseRoot.transform.SetParent(parent, false);
            houseRoot.transform.localPosition = localPosition;

            CreatePrimitive(houseRoot.transform, PrimitiveType.Cube, "Body", new Vector3(0f, 0.75f, 0f), new Vector3(1.6f, 1.5f, 1.4f), color);
            CreatePrimitive(houseRoot.transform, PrimitiveType.Cube, "Roof", new Vector3(0f, 1.55f, 0f), new Vector3(1.8f, 0.3f, 1.6f), new Color(0.45f, 0.2f, 0.16f));
        }

        private static void CreateTree(Transform parent, string name, Vector3 localPosition)
        {
            var treeRoot = new GameObject(name);
            treeRoot.transform.SetParent(parent, false);
            treeRoot.transform.localPosition = localPosition;

            CreatePrimitive(treeRoot.transform, PrimitiveType.Cylinder, "Trunk", new Vector3(0f, 1f, 0f), new Vector3(0.25f, 1f, 0.25f), new Color(0.38f, 0.24f, 0.1f));
            CreatePrimitive(treeRoot.transform, PrimitiveType.Sphere, "Canopy", new Vector3(0f, 2.3f, 0f), new Vector3(1.4f, 1.2f, 1.4f), new Color(0.21f, 0.52f, 0.24f));
        }

        private static void CreateBlock(Transform parent, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            CreatePrimitive(parent, PrimitiveType.Cube, name, localPosition, localScale, color);
        }

        private static GameObject CreatePrimitive(Transform parent, PrimitiveType type, string name, Vector3 localPosition, Vector3 localScale, Color color)
        {
            var instance = GameObject.CreatePrimitive(type);
            instance.name = name;
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
