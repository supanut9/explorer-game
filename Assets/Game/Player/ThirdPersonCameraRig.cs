using UnityEngine;
using UnityEngine.InputSystem;

namespace ExplorerGame.Player
{
    public sealed class ThirdPersonCameraRig : MonoBehaviour
    {
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

            var lookInput = runtimeLookAction.ReadValue<Vector2>();
            yaw += lookInput.x * lookSensitivity;
            pitch = Mathf.Clamp(pitch - lookInput.y * lookSensitivity, minPitch, maxPitch);

            var pivot = target.position + followOffset;
            var rotation = Quaternion.Euler(pitch, yaw, 0f);
            var desiredPosition = pivot - (rotation * Vector3.forward * distance);

            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref followVelocity,
                followSmoothTime);
            transform.rotation = rotation;
        }

        public void SetTarget(Transform nextTarget)
        {
            target = nextTarget;
            if (target == null)
            {
                return;
            }

            yaw = target.eulerAngles.y;
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        private static InputAction PrepareLookAction(InputActionProperty property)
        {
            if (property.action != null)
            {
                return property.action;
            }

            var action = new InputAction("Look", InputActionType.Value);
            action.AddBinding("<Mouse>/delta");
            action.AddBinding("<Gamepad>/rightStick");
            return action;
        }
    }
}
