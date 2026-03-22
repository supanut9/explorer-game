using ExplorerGame.Core;
using UnityEngine;

namespace ExplorerGame.Player
{
    [CreateAssetMenu(menuName = "Explorer Game/Player/Character Definition", fileName = "CharacterDefinition")]
    public sealed class CharacterDefinition : ScriptableObject
    {
        [SerializeField] private CharacterOption option;
        [SerializeField] private string displayName = "Explorer";
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 spawnOffset = Vector3.zero;

        public CharacterOption Option => option;
        public string DisplayName => displayName;
        public GameObject Prefab => prefab;
        public Vector3 SpawnOffset => spawnOffset;
    }
}
