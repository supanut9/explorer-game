using UnityEngine;
using UnityEngine.InputSystem;

namespace ExplorerGame.Player
{
    public sealed class ThirdPersonCameraRig : MonoBehaviour
    {
        private const float LookDeadzone = 0.01f;
        private const float KeyboardLookSpeed = 120f;

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 followOffset = new(0f, 1.8f, 0f);
        [SerializeField] private float distance = 4.5f;
        [SerializeField] private float followSmoothTime = 0.08f;
        [SerializeField] private float lookSensitivity = 0.08f;
        [SerializeField] private float minPitch = -25f;
        [SerializeField] private float maxPitch = 65f;
        [SerializeField] private InputActionProperty lookAction;

        private InputAction runtimeLookAction;
        private Vector3 followVelocity;
        private float yaw;
        private float pitch = 12f;

        private void OnEnable()
        {
            runtimeLookAction = PrepareLookAction(lookAction);
            runtimeLookAction.Enable();
        }

        private void OnDisable()
        {
            runtimeLookAction.Disable();
        }

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var desiredPosition = UpdateDesiredPosition();
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref followVelocity,
                followSmoothTime);
        }

        public void SetTarget(Transform nextTarget)
        {
            target = nextTarget;
            if (target == null)
            {
                return;
            }

            yaw = target.eulerAngles.y;
            var desiredPosition = UpdateDesiredPosition();
            followVelocity = Vector3.zero;
            transform.position = desiredPosition;
        }

        private Vector3 UpdateDesiredPosition()
        {
            var lookInput = ReadLookInput();
            yaw += lookInput.x * lookSensitivity;
            pitch = Mathf.Clamp(pitch - lookInput.y * lookSensitivity, minPitch, maxPitch);

            var pivot = target.position + followOffset;
            var rotation = Quaternion.Euler(pitch, yaw, 0f);
            var desiredPosition = pivot - (rotation * Vector3.forward * distance);
            transform.rotation = rotation;
            return desiredPosition;
        }

        private static InputAction PrepareLookAction(InputActionProperty property)
        {
            if (HasUsableAction(property))
            {
                return property.action;
            }

            var action = new InputAction("Look", InputActionType.Value);
            action.AddBinding("<Mouse>/delta");
            return action;
        }

        private static bool HasUsableAction(InputActionProperty property)
        {
            var reference = property.reference;
            var action = property.action;
            return reference != null && action != null && action.bindings.Count > 0;
        }

        private static Vector2 ApplyDeadzone(Vector2 input, float deadzone)
        {
            return input.sqrMagnitude < deadzone * deadzone ? Vector2.zero : input;
        }

        private Vector2 ReadLookInput()
        {
            if (HasUsableAction(lookAction))
            {
                return ApplyDeadzone(runtimeLookAction.ReadValue<Vector2>(), LookDeadzone);
            }

            var mouse = Mouse.current;
            var mouseInput = mouse != null ? ApplyDeadzone(mouse.delta.ReadValue(), LookDeadzone) : Vector2.zero;
            if (mouseInput != Vector2.zero)
            {
                return mouseInput;
            }

            var keyboard = Keyboard.current;
            if (keyboard == null)
            {
                return Vector2.zero;
            }

            var x = 0f;
            var y = 0f;

            if (keyboard.leftArrowKey.isPressed)
            {
                x -= KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.rightArrowKey.isPressed)
            {
                x += KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.qKey.isPressed)
            {
                x -= KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.cKey.isPressed)
            {
                x += KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.upArrowKey.isPressed)
            {
                y += KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.downArrowKey.isPressed)
            {
                y -= KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.rKey.isPressed)
            {
                y += KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            if (keyboard.fKey.isPressed)
            {
                y -= KeyboardLookSpeed * Time.unscaledDeltaTime;
            }

            return new Vector2(x, y);
        }
    }
}
