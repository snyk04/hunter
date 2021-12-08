using Hunter.Creatures.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(MoverComponent))]
    public class MovementInputComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        private MovementInput _movementInput;

        private void Awake()
        {
            InputAction moveAction = new Controls().Player.Move;
            Mover mover = GetComponent<MoverComponent>().Mover;
            
            _movementInput = new MovementInput(moveAction, mover, _speed);
        }
        
        private void OnEnable()
        {
            _movementInput.OnEnable();
        }
        private void OnDisable()
        {
            _movementInput.OnDisable();
        }
        private void OnDestroy()
        {
            _movementInput.OnDestroy();
        }
    }
}
