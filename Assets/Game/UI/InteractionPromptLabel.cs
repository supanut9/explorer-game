using UnityEngine;
using UnityEngine.UI;

namespace ExplorerGame.UI
{
    [RequireComponent(typeof(Text))]
    public sealed class InteractionPromptLabel : MonoBehaviour
    {
        [SerializeField] private string prefix = "Press E: ";

        private Text label;

        private void Awake()
        {
            label = GetComponent<Text>();
            SetPrompt(null);
        }

        public void SetPrompt(string promptText)
        {
            if (label == null)
            {
                label = GetComponent<Text>();
            }

            var hasPrompt = !string.IsNullOrWhiteSpace(promptText);
            label.text = hasPrompt ? prefix + promptText : string.Empty;
            label.enabled = hasPrompt;
        }
    }
}
