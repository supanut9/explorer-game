using ExplorerGame.Core;
using ExplorerGame.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.World
{
    public sealed class WorldRuntimeController : MonoBehaviour
    {
        [SerializeField] private CharacterCatalog characterCatalog;
        [SerializeField] private WorldCatalog worldCatalog;

        private async void Start()
        {
            var session = GameSession.EnsureInstance();
            characterCatalog ??= Resources.Load<CharacterCatalog>(GameConstants.CharacterConfigPath);
            worldCatalog ??= Resources.Load<WorldCatalog>(GameConstants.WorldConfigPath);

            if (worldCatalog == null)
            {
                Debug.LogError("WorldCatalog is missing. Run Tools/Explorer Game/Generate Project Scaffolding.");
                return;
            }

            var sceneName = session.ActiveZoneScene;
            if (!worldCatalog.TryGetZone(sceneName, out var zone))
            {
                Debug.LogError($"Zone '{sceneName}' was not found in WorldCatalog.");
                return;
            }

            if (!SceneManager.GetSceneByName(zone.SceneName).isLoaded)
            {
                var operation = SceneManager.LoadSceneAsync(zone.SceneName, LoadSceneMode.Additive);
                while (operation != null && !operation.isDone)
                {
                    await Awaitable.NextFrameAsync();
                }
            }

            SpawnPlayer(session.SelectedCharacter, zone);
        }

        private void SpawnPlayer(CharacterOption option, ZoneDefinition zone)
        {
            if (characterCatalog == null)
            {
                Debug.LogWarning("CharacterCatalog is missing. Player spawn skipped.");
                return;
            }

            if (!characterCatalog.TryGet(option, out var definition) || definition.Prefab == null)
            {
                Debug.LogWarning($"Character '{option}' is missing a prefab. Assign one in CharacterCatalog.");
                return;
            }

            var spawnPosition = zone.PlayerSpawnPoint + definition.SpawnOffset;
            Instantiate(definition.Prefab, spawnPosition, Quaternion.identity);
        }
    }
}
