using System.Collections.Generic;
using ExplorerGame.UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ExplorerGame.Interaction
{
    public sealed class InteractionProbe : MonoBehaviour
    {
        [SerializeField] private float radius = 2.5f;
        [SerializeField] private LayerMask interactionMask = ~0;
        [SerializeField] private InteractionPromptLabel promptLabel;
        [SerializeField] private InputActionProperty interactAction;

        private readonly Collider[] overlapResults = new Collider[16];
        private InputAction runtimeInteractAction;
        private IInteractable currentTarget;

        public IInteractable CurrentTarget => currentTarget;

        private void Awake()
        {
            promptLabel ??= FindAnyObjectByType<InteractionPromptLabel>();
        }

        private void OnEnable()
        {
            runtimeInteractAction = PrepareInteractAction(interactAction);
            runtimeInteractAction.Enable();
        }

        private void OnDisable()
        {
            runtimeInteractAction.Disable();
            SetCurrentTarget(null);
        }

        private void Update()
        {
            RefreshCurrentTarget();
            if (ReadInteractPressed())
            {
                TriggerCurrentTarget();
            }
        }

        public bool TriggerCurrentTarget()
        {
            if (currentTarget == null)
            {
                return false;
            }

            currentTarget.Interact();
            return true;
        }

        private void RefreshCurrentTarget()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(
                transform.position,
                radius,
                overlapResults,
                interactionMask,
                QueryTriggerInteraction.Collide);

            IInteractable nearest = null;
            var nearestDistance = float.MaxValue;

            for (var i = 0; i < hitCount; i++)
            {
                var collider = overlapResults[i];
                if (collider == null)
                {
                    continue;
                }

                if (!TryGetInteractable(collider, out var interactable))
                {
                    continue;
                }

                var distance = (collider.transform.position - transform.position).sqrMagnitude;
                if (distance < nearestDistance)
                {
                    nearest = interactable;
                    nearestDistance = distance;
                }
            }

            SetCurrentTarget(nearest);

            for (var i = 0; i < hitCount; i++)
            {
                overlapResults[i] = null;
            }
        }

        private void SetCurrentTarget(IInteractable nextTarget)
        {
            currentTarget = nextTarget;
            if (promptLabel == null)
            {
                return;
            }

            promptLabel.SetPrompt(currentTarget?.PromptText);
        }

        private static bool TryGetInteractable(Collider collider, out IInteractable interactable)
        {
            var behaviours = ListPool<MonoBehaviour>.Get();
            collider.GetComponentsInParent(true, behaviours);

            foreach (var behaviour in behaviours)
            {
                if (behaviour is IInteractable candidate)
                {
                    interactable = candidate;
                    ListPool<MonoBehaviour>.Release(behaviours);
                    return true;
                }
            }

            ListPool<MonoBehaviour>.Release(behaviours);
            interactable = null;
            return false;
        }

        private static InputAction PrepareInteractAction(InputActionProperty property)
        {
            if (HasUsableAction(property))
            {
                return property.action;
            }

            var action = new InputAction("Interact", InputActionType.Button);
            action.AddBinding("<Keyboard>/e");
            return action;
        }

        private static bool HasUsableAction(InputActionProperty property)
        {
            var reference = property.reference;
            var action = property.action;
            return reference != null && action != null && action.bindings.Count > 0;
        }

        private bool ReadInteractPressed()
        {
            if (HasUsableAction(interactAction))
            {
                return runtimeInteractAction.WasPressedThisFrame();
            }

            var keyboard = Keyboard.current;
            return keyboard != null && keyboard.eKey.wasPressedThisFrame;
        }

        private static class ListPool<T> where T : class
        {
            private static readonly Stack<List<T>> Pool = new();

            public static List<T> Get()
            {
                return Pool.Count > 0 ? Pool.Pop() : new List<T>(4);
            }

            public static void Release(List<T> list)
            {
                list.Clear();
                Pool.Push(list);
            }
        }
    }
}
