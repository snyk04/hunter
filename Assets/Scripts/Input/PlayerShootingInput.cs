using Hunter.Creatures;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    public class PlayerShootingInput
    {
        private readonly InputAction _inputAction;
        private readonly Shooter _shooter;

        public PlayerShootingInput(InputAction inputAction, Shooter shooter)
        {
            _inputAction = inputAction;
            _shooter = shooter;
            
            _inputAction.performed += Shoot;
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
            _inputAction.performed -= Shoot;
        }
        
        private void Shoot(InputAction.CallbackContext context)
        {
            _shooter.Shoot();
        }
    }
}