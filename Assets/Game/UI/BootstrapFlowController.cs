using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.UI
{
    public sealed class BootstrapFlowController : MonoBehaviour
    {
        [SerializeField] private bool autoLoadCharacterSelect = true;

        private async void Start()
        {
            GameSession.EnsureInstance();
            if (!autoLoadCharacterSelect)
            {
                return;
            }

            if (SceneManager.GetActiveScene().name != GameConstants.CharacterSelectScene)
            {
                var operation = SceneManager.LoadSceneAsync(GameConstants.CharacterSelectScene, LoadSceneMode.Single);
                while (operation != null && !operation.isDone)
                {
                    await Awaitable.NextFrameAsync();
                }
            }
        }
    }
}
