namespace ExplorerGame.Interaction
{
    public interface IInteractable
    {
        string PromptText { get; }
        void Interact();
    }
}
