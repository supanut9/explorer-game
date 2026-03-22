using ExplorerGame.Core;
using ExplorerGame.Player;
using ExplorerGame.World;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace ExplorerGame.Tests.EditMode
{
    public sealed class CatalogValidationTests
    {
        [Test]
        public void CharacterCatalog_Validate_RequiresMaleAndFemaleEntries()
        {
            var catalog = ScriptableObject.CreateInstance<CharacterCatalog>();
            AddCharacter(catalog, CharacterOption.Male, "Male Explorer");
            AddCharacter(catalog, CharacterOption.Female, "Female Explorer");

            Assert.DoesNotThrow(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
        }

        [Test]
        public void CharacterCatalog_Validate_RejectsMissingRequiredEntry()
        {
            var catalog = ScriptableObject.CreateInstance<CharacterCatalog>();
            AddCharacter(catalog, CharacterOption.Male, "Male Explorer");

            Assert.Throws<System.InvalidOperationException>(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
        }

        [Test]
        public void WorldCatalog_Validate_RequiresVillageForestAndMountainZones()
        {
            var catalog = ScriptableObject.CreateInstance<WorldCatalog>();
            AddZone(catalog, GameConstants.VillageZoneScene, "Village");
            AddZone(catalog, GameConstants.ForestZoneScene, "Forest");
            AddZone(catalog, GameConstants.MountainZoneScene, "Mountain");

            Assert.DoesNotThrow(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
        }

        [Test]
        public void WorldCatalog_Validate_RejectsMissingRequiredZone()
        {
            var catalog = ScriptableObject.CreateInstance<WorldCatalog>();
            AddZone(catalog, GameConstants.VillageZoneScene, "Village");
            AddZone(catalog, GameConstants.ForestZoneScene, "Forest");

            Assert.Throws<System.InvalidOperationException>(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
        }

        private static void AddCharacter(CharacterCatalog catalog, CharacterOption option, string displayName)
        {
            var character = ScriptableObject.CreateInstance<CharacterDefinition>();
            var serializedCharacter = new SerializedObject(character);
            serializedCharacter.FindProperty("option").enumValueIndex = (int)option;
            serializedCharacter.FindProperty("displayName").stringValue = displayName;
            serializedCharacter.ApplyModifiedPropertiesWithoutUndo();

            var serializedCatalog = new SerializedObject(catalog);
            var characters = serializedCatalog.FindProperty("characters");
            characters.InsertArrayElementAtIndex(characters.arraySize);
            characters.GetArrayElementAtIndex(characters.arraySize - 1).objectReferenceValue = character;
            serializedCatalog.ApplyModifiedPropertiesWithoutUndo();
        }

        private static void AddZone(WorldCatalog catalog, string sceneName, string displayName)
        {
            var zone = ScriptableObject.CreateInstance<ZoneDefinition>();
            var serializedZone = new SerializedObject(zone);
            serializedZone.FindProperty("sceneName").stringValue = sceneName;
            serializedZone.FindProperty("displayName").stringValue = displayName;
            serializedZone.FindProperty("description").stringValue = displayName + " exploration zone";
            serializedZone.ApplyModifiedPropertiesWithoutUndo();

            var serializedCatalog = new SerializedObject(catalog);
            var zones = serializedCatalog.FindProperty("zones");
            zones.InsertArrayElementAtIndex(zones.arraySize);
            zones.GetArrayElementAtIndex(zones.arraySize - 1).objectReferenceValue = zone;
            serializedCatalog.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
