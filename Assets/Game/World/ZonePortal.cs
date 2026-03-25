using ExplorerGame.Core;
using ExplorerGame.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.World
{
    public sealed class ZonePortal : MonoBehaviour
    {
        [SerializeField] private string targetZoneScene = GameConstants.VillageZoneScene;
        [SerializeField] private bool requireTriggerEnter = true;

        private bool isLoading;

        public string TargetZoneScene => targetZoneScene;

        private async void OnTriggerEnter(Collider other)
        {
            if (!requireTriggerEnter || isLoading || !other.TryGetComponent<ThirdPersonExplorerController>(out _))
            {
                return;
            }

            await TravelAsync();
        }

        public async Awaitable TravelAsync()
        {
            if (isLoading || string.IsNullOrWhiteSpace(targetZoneScene))
            {
                return;
            }

            isLoading = true;
            var session = GameSession.EnsureInstance();
            session.SetActiveZone(targetZoneScene);

            var operation = SceneManager.LoadSceneAsync(GameConstants.WorldPersistentScene, LoadSceneMode.Single);
            while (operation != null && !operation.isDone)
            {
                await Awaitable.NextFrameAsync();
            }

            isLoading = false;
        }
    }
}
