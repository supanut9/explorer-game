using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.UI;

namespace ExplorerGame.UI
{
    [RequireComponent(typeof(Text))]
    public sealed class SelectedCharacterLabel : MonoBehaviour
    {
        [SerializeField] private string prefix = "Selected Character: ";

        private Text label;

        private void Awake()
        {
            label = GetComponent<Text>();
        }

        private void Update()
        {
            var option = GameSession.HasInstance ? GameSession.Instance.SelectedCharacter : CharacterOption.Male;
            label.text = prefix + option;
        }
    }
}
