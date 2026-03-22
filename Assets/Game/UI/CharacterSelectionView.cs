using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.UI
{
    public sealed class CharacterSelectionView : MonoBehaviour
    {
        public void SelectMale()
        {
            Select(CharacterOption.Male);
        }

        public void SelectFemale()
        {
            Select(CharacterOption.Female);
        }

        public void ApplySelection(CharacterOption option)
        {
            var session = GameSession.EnsureInstance();
            session.SelectCharacter(option);
            session.SetActiveZone(GameConstants.VillageZoneScene);
        }

        private async void Select(CharacterOption option)
        {
            ApplySelection(option);

            var operation = SceneManager.LoadSceneAsync(GameConstants.WorldPersistentScene, LoadSceneMode.Single);
            while (operation != null && !operation.isDone)
            {
                await Awaitable.NextFrameAsync();
            }
        }
    }
}
