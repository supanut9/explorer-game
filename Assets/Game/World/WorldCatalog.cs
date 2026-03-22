using System;
using System.Collections.Generic;
using ExplorerGame.Core;
using UnityEngine;

namespace ExplorerGame.World
{
    [CreateAssetMenu(menuName = "Explorer Game/World/World Catalog", fileName = "WorldCatalog")]
    public sealed class WorldCatalog : ScriptableObject
    {
        [SerializeField] private List<ZoneDefinition> zones = new();

        public IReadOnlyList<ZoneDefinition> Zones => zones;

        public bool TryGetZone(string sceneName, out ZoneDefinition definition)
        {
            foreach (var zone in zones)
            {
                if (zone != null && zone.SceneName == sceneName)
                {
                    definition = zone;
                    return true;
                }
            }

            definition = null;
            return false;
        }

        public void Validate()
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            var requiredZones = new HashSet<string>(StringComparer.Ordinal)
            {
                GameConstants.VillageZoneScene,
                GameConstants.ForestZoneScene,
                GameConstants.MountainZoneScene
            };

            foreach (var zone in zones)
            {
                if (zone == null)
                {
                    throw new InvalidOperationException("World catalog contains a null zone.");
                }

                if (string.IsNullOrWhiteSpace(zone.SceneName))
                {
                    throw new InvalidOperationException($"Zone '{zone.name}' is missing a scene name.");
                }

                if (string.IsNullOrWhiteSpace(zone.DisplayName))
                {
                    throw new InvalidOperationException($"Zone '{zone.SceneName}' is missing a display name.");
                }

                if (!seen.Add(zone.SceneName))
                {
                    throw new InvalidOperationException($"World catalog contains duplicate scene '{zone.SceneName}'.");
                }

                requiredZones.Remove(zone.SceneName);
            }

            if (seen.Count == 0)
            {
                throw new InvalidOperationException("World catalog must contain at least one zone.");
            }

            if (requiredZones.Count > 0)
            {
                throw new InvalidOperationException(
                    $"World catalog must contain the required zones: {string.Join(", ", requiredZones)}.");
            }
        }
    }
}
