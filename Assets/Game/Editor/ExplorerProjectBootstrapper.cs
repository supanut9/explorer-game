using System.IO;
using ExplorerGame.Core;
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

        [MenuItem("Tools/Explorer Game/Generate Project Scaffolding")]
        public static void GenerateProjectScaffolding()
        {
            EnsureFolder(SceneFolder);
            EnsureFolder(GameConstants.ResourcesFolder);
            EnsureFolder(GameConstants.ConfigFolder);
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
            var maleCharacter = CreateCharacterAsset(CharacterOption.Male, "Male Explorer");
            var femaleCharacter = CreateCharacterAsset(CharacterOption.Female, "Female Explorer");
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

        private static CharacterDefinition CreateCharacterAsset(CharacterOption option, string displayName)
        {
            var path = $"{GameConstants.ConfigFolder}/{displayName.Replace(" ", string.Empty)}.asset";
            var existing = AssetDatabase.LoadAssetAtPath<CharacterDefinition>(path);
            if (existing != null)
            {
                return existing;
            }

            var character = ScriptableObject.CreateInstance<CharacterDefinition>();
            var serializedCharacter = new SerializedObject(character);
            serializedCharacter.FindProperty("option").enumValueIndex = (int)option;
            serializedCharacter.FindProperty("displayName").stringValue = displayName;
            serializedCharacter.ApplyModifiedPropertiesWithoutUndo();
            AssetDatabase.CreateAsset(character, path);
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
