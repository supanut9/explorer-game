using ExplorerGame.Core;
using ExplorerGame.World;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace ExplorerGame.Tests.EditMode
{
    public sealed class WorldCatalogTests
    {
        [Test]
        public void TryGetZone_ReturnsZoneDefinitionBySceneName()
        {
            var catalog = ScriptableObject.CreateInstance<WorldCatalog>();
            var village = CreateZone(GameConstants.VillageZoneScene, "Village");
            AddZone(catalog, village);

            Assert.IsTrue(catalog.TryGetZone(GameConstants.VillageZoneScene, out var definition));
            Assert.AreSame(village, definition);

            Object.DestroyImmediate(village);
            Object.DestroyImmediate(catalog);
        }

        [Test]
        public void Validate_RequiresVillageForestAndMountainZones()
        {
            var catalog = ScriptableObject.CreateInstance<WorldCatalog>();
            var village = CreateZone(GameConstants.VillageZoneScene, "Village");
            var forest = CreateZone(GameConstants.ForestZoneScene, "Forest");
            var mountain = CreateZone(GameConstants.MountainZoneScene, "Mountain");
            AddZone(catalog, village);
            AddZone(catalog, forest);
            AddZone(catalog, mountain);

            Assert.DoesNotThrow(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
            Object.DestroyImmediate(village);
            Object.DestroyImmediate(forest);
            Object.DestroyImmediate(mountain);
        }

        [Test]
        public void Validate_RejectsMissingRequiredZone()
        {
            var catalog = ScriptableObject.CreateInstance<WorldCatalog>();
            var village = CreateZone(GameConstants.VillageZoneScene, "Village");
            var forest = CreateZone(GameConstants.ForestZoneScene, "Forest");
            AddZone(catalog, village);
            AddZone(catalog, forest);

            Assert.Throws<System.InvalidOperationException>(() => catalog.Validate());

            Object.DestroyImmediate(catalog);
            Object.DestroyImmediate(village);
            Object.DestroyImmediate(forest);
        }

        private static ZoneDefinition CreateZone(string sceneName, string displayName)
        {
            var zone = ScriptableObject.CreateInstance<ZoneDefinition>();
            var serializedZone = new SerializedObject(zone);
            serializedZone.FindProperty("sceneName").stringValue = sceneName;
            serializedZone.FindProperty("displayName").stringValue = displayName;
            serializedZone.FindProperty("description").stringValue = displayName + " exploration zone";
            serializedZone.ApplyModifiedPropertiesWithoutUndo();
            return zone;
        }

        private static void AddZone(WorldCatalog catalog, ZoneDefinition zone)
        {
            var serializedCatalog = new SerializedObject(catalog);
            var zones = serializedCatalog.FindProperty("zones");
            zones.InsertArrayElementAtIndex(zones.arraySize);
            zones.GetArrayElementAtIndex(zones.arraySize - 1).objectReferenceValue = zone;
            serializedCatalog.ApplyModifiedPropertiesWithoutUndo();
        }
    }
}
