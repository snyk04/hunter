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
            _startPosition = CalculateStartPosition();

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

        private Vector3 CalculateStartPosition()
        {
            Vector3 startPosition = AnimalInfo.Transform.position;
            
            if (startPosition.x + AnimalInfo.WanderingRadius > AnimalInfo.Field.XRightBorder)
            {
                startPosition.x = AnimalInfo.Field.XRightBorder - AnimalInfo.WanderingRadius;
            }
            else if (startPosition.x - AnimalInfo.WanderingRadius < AnimalInfo.Field.XLeftBorder)
            {
                startPosition.x = AnimalInfo.Field.XLeftBorder + AnimalInfo.WanderingRadius;
            }
            
            if (startPosition.y + AnimalInfo.WanderingRadius > AnimalInfo.Field.YTopBorder)
            {
                startPosition.y = AnimalInfo.Field.YTopBorder - AnimalInfo.WanderingRadius;
            }
            else if (startPosition.y - AnimalInfo.WanderingRadius < AnimalInfo.Field.YBotBorder)
            {
                startPosition.y = AnimalInfo.Field.YBotBorder + AnimalInfo.WanderingRadius;
            }

            return startPosition;
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
