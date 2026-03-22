using UnityEngine;
using UnityEngine.UI;

namespace ExplorerGame.UI
{
    [RequireComponent(typeof(Text))]
    public sealed class InteractionPromptLabel : MonoBehaviour
    {
        [SerializeField] private string prefix = "[E] ";
        [SerializeField] private Color textColor = new Color(0.96f, 0.94f, 0.86f);
        [SerializeField] private Color outlineColor = new Color(0.08f, 0.08f, 0.07f, 0.95f);
        [SerializeField] private int fontSize = 20;

        private Text label;
        private Outline outline;

        private void Awake()
        {
            label = GetComponent<Text>();
            outline = GetComponent<Outline>();
            if (outline == null)
            {
                outline = gameObject.AddComponent<Outline>();
            }

            label.alignment = TextAnchor.LowerCenter;
            label.fontSize = fontSize;
            label.color = textColor;
            outline.effectColor = outlineColor;
            outline.effectDistance = new Vector2(2f, -2f);
            SetPrompt(null);
        }

        public void SetPrompt(string promptText)
        {
            if (label == null)
            {
                label = GetComponent<Text>();
            }

            var hasPrompt = !string.IsNullOrWhiteSpace(promptText);
            label.text = hasPrompt ? prefix + promptText.ToUpperInvariant() : string.Empty;
            label.enabled = hasPrompt;
            if (outline != null)
            {
                outline.enabled = hasPrompt;
            }
        }
    }
}
