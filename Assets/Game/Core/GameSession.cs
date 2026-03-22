using UnityEngine;

namespace ExplorerGame.Core
{
    public sealed class GameSession : MonoBehaviour
    {
        private static GameSession instance;

        [SerializeField] private CharacterOption selectedCharacter = CharacterOption.Male;
        [SerializeField] private string activeZoneScene = GameConstants.VillageZoneScene;

        public static GameSession Instance => instance;
        public static bool HasInstance => instance != null;

        public CharacterOption SelectedCharacter => selectedCharacter;
        public string ActiveZoneScene => activeZoneScene;

        public static GameSession EnsureInstance()
        {
            if (instance != null)
            {
                return instance;
            }

            var root = new GameObject(nameof(GameSession));
            instance = root.AddComponent<GameSession>();
            DontDestroyOnLoad(root);
            return instance;
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public void SelectCharacter(CharacterOption option)
        {
            selectedCharacter = option;
        }

        public void SetActiveZone(string sceneName)
        {
            if (!string.IsNullOrWhiteSpace(sceneName))
            {
                activeZoneScene = sceneName;
            }
        }

        public void ResetState()
        {
            selectedCharacter = CharacterOption.Male;
            activeZoneScene = GameConstants.VillageZoneScene;
        }
    }
}
