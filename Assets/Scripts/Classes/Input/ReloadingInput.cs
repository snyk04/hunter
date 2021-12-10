using Hunter.Creatures.Shooters;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    public class ReloadingInput
    {
        private readonly InputAction _inputAction;
        private readonly Shooter _shooter;

        public ReloadingInput(InputAction inputAction, Shooter shooter)
        {
            _inputAction = inputAction;
            _shooter = shooter;
            
            _inputAction.performed += Reload;
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
            _inputAction.performed -= Reload;
        }
        
        private void Reload(InputAction.CallbackContext context)
        {
            _shooter.StartReloading();
        }
    }
}
