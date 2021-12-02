using Hunter.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(ShooterComponent))]
    public class PlayerShootingInputComponent : MonoBehaviour
    {
        private PlayerShootingInput _playerShootingInput;

        private void Awake()
        {
            InputAction shootAction = new Controls().Player.Fire;
            Shooter shooter = GetComponent<ShooterComponent>().Shooter;

            _playerShootingInput = new PlayerShootingInput(shootAction, shooter);
        }
        
        private void OnEnable()
        {
            _playerShootingInput.OnEnable();
        }
        private void OnDisable()
        {
            _playerShootingInput.OnDisable();
        }
        private void OnDestroy()
        {
            _playerShootingInput.OnDestroy();
        }
    }
}
