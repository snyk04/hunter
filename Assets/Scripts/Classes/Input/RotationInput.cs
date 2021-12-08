using Hunter.Creatures.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    public class RotationInput
    {
        private const string MouseDeviceName = "Mouse";
        
        private readonly InputAction _inputAction;
        private readonly Rotator _rotator;

        public RotationInput(InputAction inputAction, Rotator rotator)
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
            
            if (context.control.device.name == MouseDeviceName)
            {
                Vector3 pointerScenePosition = Camera.main.ScreenToWorldPoint(direction);
                direction = pointerScenePosition - _rotator.Transform.position;
            }
            
            _rotator.LookAt(direction.normalized);
        }
    }
}
