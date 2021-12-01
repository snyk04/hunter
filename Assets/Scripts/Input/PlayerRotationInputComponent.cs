using Hunter.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(RotatorComponent))]
    public class PlayerRotationInputComponent : MonoBehaviour
    {
        private PlayerRotationInput _playerRotationInput;

        private void Awake()
        {
            InputAction moveAction = new Controls().Player.Look;
            Rotator rotator = GetComponent<RotatorComponent>().Rotator;
            
            _playerRotationInput = new PlayerRotationInput(moveAction, rotator);
        }
        
        private void OnEnable()
        {
            _playerRotationInput.OnEnable();
        }
        private void OnDisable()
        {
            _playerRotationInput.OnDisable();
        }
        private void OnDestroy()
        {
            _playerRotationInput.OnDestroy();
        }
    }
}
