using Hunter.Creatures.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    public class MovementInput
    {
        private readonly InputAction _inputAction;
        private readonly Mover _mover;
        private readonly float _speed;

        public MovementInput(InputAction inputAction, Mover mover, float speed)
        {
            _inputAction = inputAction;
            _mover = mover;
            _speed = speed;
            
            _inputAction.performed += Move;
            _inputAction.canceled += Stop;
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
            _inputAction.performed -= Move;
            _inputAction.canceled -= Stop;
        }
        
        private void Move(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _mover.Move(direction, _speed);
        }
        private void Stop(InputAction.CallbackContext context)
        {
            _mover.Stop();
        }
    }
}
