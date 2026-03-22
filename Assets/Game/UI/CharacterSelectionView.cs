using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ExplorerGame.UI
{
    public sealed class CharacterSelectionView : MonoBehaviour
    {
        private readonly Rect panelRect = new Rect(24f, 24f, 320f, 180f);

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

        private void OnGUI()
        {
            GUILayout.BeginArea(panelRect, GUI.skin.box);
            GUILayout.Label("Explorer Game");
            GUILayout.Label("Choose your explorer");
            GUILayout.Space(12f);

            if (GUILayout.Button("Play as Male Explorer", GUILayout.Height(36f)))
            {
                SelectMale();
            }

            GUILayout.Space(8f);

            if (GUILayout.Button("Play as Female Explorer", GUILayout.Height(36f)))
            {
                SelectFemale();
            }

            GUILayout.EndArea();
        }
    }
}
