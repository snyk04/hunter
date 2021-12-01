using Hunter.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Hunter.Input
{
    [RequireComponent(typeof(MoverComponent))]
    public class PlayerMovementInputComponent : MonoBehaviour
    {
        private PlayerMovementInput _playerMovementInput;

        private void Awake()
        {
            InputAction moveAction = new Controls().Player.Move;
            Mover mover = GetComponent<MoverComponent>().Mover;
            
            _playerMovementInput = new PlayerMovementInput(moveAction, mover);
        }
        
        private void OnEnable()
        {
            _playerMovementInput.OnEnable();
        }
        private void OnDisable()
        {
            _playerMovementInput.OnDisable();
        }
        private void OnDestroy()
        {
            _playerMovementInput.OnDestroy();
        }
    }
}
