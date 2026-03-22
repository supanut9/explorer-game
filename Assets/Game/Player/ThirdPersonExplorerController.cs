using ExplorerGame.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ExplorerGame.Player
{
    [RequireComponent(typeof(CharacterController))]
    public sealed class ThirdPersonExplorerController : MonoBehaviour
    {
        [SerializeField] private Transform movementReference;
        [SerializeField] private float walkSpeed = 3.5f;
        [SerializeField] private float sprintSpeed = 5.5f;
        [SerializeField] private float rotationSpeed = 540f;
        [SerializeField] private float gravity = -20f;
        [SerializeField] private InputActionProperty moveAction;
        [SerializeField] private InputActionProperty sprintAction;

        private CharacterController characterController;
        private InputAction runtimeMoveAction;
        private InputAction runtimeSprintAction;
        private Vector3 velocity;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
        }

        private void OnEnable()
        {
            runtimeMoveAction = PrepareMoveAction(moveAction);
            runtimeSprintAction = PrepareSprintAction(sprintAction);
            runtimeMoveAction.Enable();
            runtimeSprintAction.Enable();
        }

        private void OnDisable()
        {
            runtimeMoveAction.Disable();
            runtimeSprintAction.Disable();
        }

        private void Update()
        {
            var reference = movementReference != null ? movementReference : Camera.main != null ? Camera.main.transform : null;
            var moveInput = runtimeMoveAction.ReadValue<Vector2>();
            var moveDirection = ThirdPersonMovementMath.GetCameraRelativeDirection(moveInput, reference);
            var isSprinting = runtimeSprintAction.IsPressed();
            var speed = isSprinting ? sprintSpeed : walkSpeed;

            if (moveDirection.sqrMagnitude > 0.0001f)
            {
                var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime);
            }

            if (characterController.isGrounded && velocity.y < 0f)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;

            var displacement = (moveDirection * speed) + Vector3.up * velocity.y;
            characterController.Move(displacement * Time.deltaTime);
        }

        private static InputAction PrepareMoveAction(InputActionProperty property)
        {
            if (property.action != null)
            {
                return property.action;
            }

            var action = new InputAction("Move", InputActionType.Value);
            action.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            action.AddBinding("<Gamepad>/leftStick");
            return action;
        }

        private static InputAction PrepareSprintAction(InputActionProperty property)
        {
            if (property.action != null)
            {
                return property.action;
            }

            var action = new InputAction("Sprint", InputActionType.Button);
            action.AddBinding("<Keyboard>/leftShift");
            action.AddBinding("<Gamepad>/leftStickPress");
            return action;
        }
    }
}
