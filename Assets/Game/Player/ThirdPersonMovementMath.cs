using UnityEngine;

namespace ExplorerGame.Player
{
    public static class ThirdPersonMovementMath
    {
        public static Vector3 GetCameraRelativeDirection(Vector2 moveInput, Transform reference)
        {
            if (reference == null)
            {
                return new Vector3(moveInput.x, 0f, moveInput.y);
            }

            var forward = reference.forward;
            forward.y = 0f;
            forward.Normalize();

            var right = reference.right;
            right.y = 0f;
            right.Normalize();

            var direction = (forward * moveInput.y) + (right * moveInput.x);
            return direction.sqrMagnitude > 1f ? direction.normalized : direction;
        }
    }
}
