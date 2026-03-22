using UnityEngine;

namespace ExplorerGame.Interaction
{
    public sealed class DialogueNpc : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptText = "Talk";
        [SerializeField] private string dialogueText = "Hello, explorer.";

        public string PromptText => promptText;

        public void Interact()
        {
            Debug.Log(dialogueText, this);
        }
    }
}
