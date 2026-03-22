using UnityEngine;

namespace ExplorerGame.Interaction
{
    public sealed class InspectableObject : MonoBehaviour, IInteractable
    {
        [SerializeField] private string promptText = "Inspect";
        [SerializeField] private string descriptionText = "A curious object.";

        public string PromptText => promptText;

        public void Interact()
        {
            Debug.Log(descriptionText, this);
        }
    }
}
