using System;
using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitWanderingState : RabbitState
    {
        private const float WanderingArrivalDistance = 1;
        private const float ChangeCurrentGoalDelay = 5;
        private const float AllowableErrorForPositionCheck = 0.5f;

        private readonly Vector2 _startPosition; 
        private Vector2 _currentGoalPosition;
        private DateTime _lastTimeGoalChanged;
        private bool _isWaiting;

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

        private Vector2 CalculateStartPosition()
        {
            Vector2 startPosition = AnimalInfo.Position;
            
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
            if (AnimalInfo.Position.ApproximatelyEquals(_currentGoalPosition, AllowableErrorForPositionCheck))
            {
                if (!_isWaiting)
                {
                    _lastTimeGoalChanged = DateTime.Now;
                    _isWaiting = true;
                }

                if (_lastTimeGoalChanged.GetPassedSeconds() > ChangeCurrentGoalDelay)
                {
                    ChangeCurrentGoal();
                    _isWaiting = false;
                }
            }
        }
        private void ChangeCurrentGoal()
        {
            _currentGoalPosition = _startPosition + Random.insideUnitCircle * AnimalInfo.WanderingRadius;
        }
        private void MoveToCurrentGoal()
        {
            Vector2 moveDirection = _currentGoalPosition - AnimalInfo.Position;
            moveDirection = moveDirection.magnitude > AllowableErrorForPositionCheck ? moveDirection : Vector2.zero;

            float temp = moveDirection.magnitude / WanderingArrivalDistance;
            float speed = Mathf.Min(temp, 1);
            
            AnimalInfo.Mover.Move(moveDirection.normalized, AnimalInfo.WanderingSpeed * speed);
        }
    }
}
