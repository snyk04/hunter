using Hunter.Common;
using Hunter.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    public class PlayerRotationInput
    {
        private readonly InputAction _inputAction;
        private readonly Rotator _rotator;

        public PlayerRotationInput(InputAction inputAction, Rotator rotator)
        {
            _inputAction = inputAction;
            _rotator = rotator;

            inputAction.performed += Rotate;
        }

        public void OnEnable()
        {
            _inputAction.Enable();
        }
        public void OnDisable()
        {
            _inputAction.Disable();
        }
        public void OnDestroy()
        {
            _inputAction.performed -= Rotate;
        }
        
        private void Rotate(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();

            if (direction.x < -1 || direction.x > 1 || direction.y < -1 || direction.y > 1)
            {
                direction = direction.ConvertMousePositionToNormalizedVector();
            }
            
            _rotator.LookAt(direction.normalized);
        }
    }
}
