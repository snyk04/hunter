using Hunter.Creatures.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(RotatorComponent))]
    public class RotationInputComponent : MonoBehaviour
    {
        private RotationInput _rotationInput;

        private void Awake()
        {
            InputAction moveAction = new Controls().Player.Look;
            Rotator rotator = GetComponent<RotatorComponent>().Rotator;
            
            _rotationInput = new RotationInput(moveAction, rotator);
        }
        
        private void OnEnable()
        {
            _rotationInput.OnEnable();
        }
        private void OnDisable()
        {
            _rotationInput.OnDisable();
        }
        private void OnDestroy()
        {
            _rotationInput.OnDestroy();
        }
    }
}
