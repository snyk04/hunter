using Hunter.AI.Common;
using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.Rabbit
{
    public class WanderingState : State
    {
        private const float AllowableErrorForPositionCheck = 0.5f;

        private readonly Vector3 _startPosition; 
        private Vector3 _currentGoalPosition;

        public WanderingState(AnimalInfo animalInfo) : base(animalInfo)
        {
            _startPosition = animalInfo.Transform.position;
            _currentGoalPosition = _startPosition;
        }
        
        public override void Update()
        {
            CheckForLiveBeingsNearby();
            
            if (AnimalInfo.Transform.position.ApproximatelyEquals(_currentGoalPosition, AllowableErrorForPositionCheck))
            {
                DecideWhereToMove();
            }

            MoveToCurrentGoal();
        }

        private void CheckForLiveBeingsNearby()
        {
            Collider2D nearbyLiveBeing = Physics2D.OverlapCircle(AnimalInfo.Transform.position, AnimalInfo.DetectionRadius);
            if (nearbyLiveBeing != null && nearbyLiveBeing.TryGetComponent(out MoverComponent moverComponent))
            {
                ChangeState(new RabbitFleeState(AnimalInfo, nearbyLiveBeing.transform));
            }
        }
        private void DecideWhereToMove()
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
