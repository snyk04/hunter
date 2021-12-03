using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    public class WanderingState : State
    {
        private const float AllowableErrorForPositionCheck = 0.5f;
            
        private readonly float _wanderingRadius;
        
        private readonly Vector3 _startPosition; 
        private Vector3 _currentGoalPosition;

        public WanderingState(IAnimal animal, float wanderingRadius, Transform transform, Mover mover) : base(animal, transform, mover)
        {
            _wanderingRadius = wanderingRadius;
            
            _startPosition = transform.position;
            _currentGoalPosition = _startPosition;
        }
        
        public override void Update()
        {
            if (Transform.position.ApproximatelyEquals(_currentGoalPosition, AllowableErrorForPositionCheck))
            {
                DecideWhereToMove();
            }

            MoveToCurrentGoal();
        }
        
        private void DecideWhereToMove()
        {
            _currentGoalPosition = _startPosition.Add(Random.insideUnitCircle * _wanderingRadius);
        }
        private void MoveToCurrentGoal()
        {
            Vector2 moveDirection = _currentGoalPosition - Transform.position;
            Mover.Move(moveDirection.normalized);
        }
    }
}
