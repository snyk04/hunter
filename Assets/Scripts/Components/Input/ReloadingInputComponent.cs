using Hunter.Creatures.Shooters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(ShooterComponent))]
    public class ReloadingInputComponent : MonoBehaviour
    {
        private ReloadingInput _reloadingInput;

        private void Awake()
        {
            InputAction reloadAction = new Controls().Player.Reload;
            Shooter shooter = GetComponent<ShooterComponent>().Shooter;

            _reloadingInput = new ReloadingInput(reloadAction, shooter);
        }
        
        private void OnEnable()
        {
            _reloadingInput.OnEnable();
        }
        private void OnDisable()
        {
            _reloadingInput.OnDisable();
        }
        private void OnDestroy()
        {
            _reloadingInput.OnDestroy();
        }
    }
}
