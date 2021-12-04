using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitWanderingState : RabbitState
    {
        private const float AllowableErrorForPositionCheck = 0.5f;

        private readonly Vector3 _startPosition; 
        private Vector3 _currentGoalPosition;

        public RabbitWanderingState(AnimalInfo animalInfo) : base(animalInfo)
        {
            _startPosition = animalInfo.Transform.position;
            _currentGoalPosition = _startPosition;
        }
        
        public override void Update()
        {
            if (LiveBeingNearby(out Transform liveBeing))
            {
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, liveBeing));
                return;
            }
            
            DecideWhereToMove();
            MoveToCurrentGoal();
        }
        
        private void DecideWhereToMove()
        {
            if (AnimalInfo.Transform.position.ApproximatelyEquals(_currentGoalPosition, AllowableErrorForPositionCheck))
            {
                ChangeCurrentGoal();
            }
        }
        private void ChangeCurrentGoal()
        {
            _currentGoalPosition = _startPosition.Add(Random.insideUnitCircle * AnimalInfo.WanderingRadius);
        }
        private void MoveToCurrentGoal()
        {
            Vector2 moveDirection = _currentGoalPosition - AnimalInfo.Transform.position;
            AnimalInfo.Mover.Move(moveDirection.normalized, AnimalInfo.WanderingSpeed);
        }
    }
}
