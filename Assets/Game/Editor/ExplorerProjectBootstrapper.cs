using System.IO;
using ExplorerGame.Core;
using ExplorerGame.Interaction;
using ExplorerGame.Player;
using ExplorerGame.UI;
using ExplorerGame.World;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.Editor
{
    public static class ExplorerProjectBootstrapper
    {
        private const string SceneFolder = "Assets/Scenes";
        private const string PrefabFolder = "Assets/Resources/Prefabs";
        private const string CharacterPrefabFolder = PrefabFolder + "/Characters";
        private const string NpcPrefabFolder = PrefabFolder + "/NPCs";
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

            var villageZone = CreateZoneAsset("VillageZone", "Village", new Vector3(0f, 0f, 0f));
            var forestZone = CreateZoneAsset("ForestZone", "Forest", new Vector3(10f, 0f, 10f));
            var mountainZone = CreateZoneAsset("MountainZone", "Mountain", new Vector3(20f, 0f, 20f));
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
            if (existing != null)
            {
                return existing;
            }

            var zone = ScriptableObject.CreateInstance<ZoneDefinition>();
            var serializedZone = new SerializedObject(zone);
            serializedZone.FindProperty("sceneName").stringValue = sceneName;
            serializedZone.FindProperty("displayName").stringValue = displayName;
            serializedZone.FindProperty("description").stringValue = $"{displayName} exploration zone";
            serializedZone.FindProperty("playerSpawnPoint").vector3Value = spawnPoint;
            serializedZone.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.CreateAsset(zone, path);
            return zone;
        }

        private static void CreateBootstrapScene()
        {
            CreateSceneWithSingleRoot(GameConstants.BootstrapScene, root =>
            {
                root.AddComponent<GameSession>();
                root.AddComponent<BootstrapFlowController>();
            });
        }

        private static void CreateCharacterSelectScene()
        {
            CreateSceneWithSingleRoot(GameConstants.CharacterSelectScene, root =>
            {
                root.AddComponent<CharacterSelectionView>();
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
                    CreatePlaceholderNpc(root.transform, new Vector3(3f, 0f, 2f));
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
