using Hunter.Creatures.Shooters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(ShooterComponent))]
    public class ShootingInputComponent : MonoBehaviour
    {
        private ShootingInput _shootingInput;

        private void Awake()
        {
            InputAction shootAction = new Controls().Player.Fire;
            Shooter shooter = GetComponent<ShooterComponent>().Shooter;

            _shootingInput = new ShootingInput(shootAction, shooter);
        }
        
        private void OnEnable()
        {
            _shootingInput.OnEnable();
        }
        private void OnDisable()
        {
            _shootingInput.OnDisable();
        }
        private void OnDestroy()
        {
            _shootingInput.OnDestroy();
        }
    }
}
