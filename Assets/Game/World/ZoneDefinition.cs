using UnityEngine;

namespace ExplorerGame.World
{
    [CreateAssetMenu(menuName = "Explorer Game/World/Zone Definition", fileName = "ZoneDefinition")]
    public sealed class ZoneDefinition : ScriptableObject
    {
        [SerializeField] private string sceneName = "VillageZone";
        [SerializeField] private string displayName = "Village";
        [SerializeField] private string description = "Starting zone";
        [SerializeField] private Vector3 playerSpawnPoint = Vector3.zero;

        public string SceneName => sceneName;
        public string DisplayName => displayName;
        public string Description => description;
        public Vector3 PlayerSpawnPoint => playerSpawnPoint;
    }
}
