using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitAvoidBorderState : RabbitState
    {
        private readonly Transform _pursuer;

        private readonly Vector2 _fleeDirection;
        
        public RabbitAvoidBorderState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
            
            _fleeDirection = (AnimalInfo.Transform.position - _pursuer.position).normalized;
            if (AnimalInfo.Transform.position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder 
                || AnimalInfo.Transform.position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder)
            {
                _fleeDirection = Quaternion.Euler(0, 0, 90) * _fleeDirection;
            }
            if (AnimalInfo.Transform.position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder 
                || AnimalInfo.Transform.position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                _fleeDirection = Quaternion.Euler(0, 0, 90) * _fleeDirection;
            }
        }

        public override void Update()
        {
            if (AnimalInfo.Transform.position.x + AnimalInfo.BorderAvoidingStopDistance < AnimalInfo.Field.XRightBorder
                && AnimalInfo.Transform.position.x - AnimalInfo.BorderAvoidingStopDistance > AnimalInfo.Field.XLeftBorder
                && AnimalInfo.Transform.position.y + AnimalInfo.BorderAvoidingStopDistance < AnimalInfo.Field.YTopBorder
                && AnimalInfo.Transform.position.y - AnimalInfo.BorderAvoidingStopDistance > AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, _pursuer));
                return;
            }
            
            if (AnimalInfo.Transform.position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder 
                || AnimalInfo.Transform.position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder
                || AnimalInfo.Transform.position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder
                || AnimalInfo.Transform.position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitAvoidBorderState(AnimalInfo, _pursuer));
            }

            AnimalInfo.Mover.Move(_fleeDirection.normalized, AnimalInfo.FleeSpeed);
        }
    }
}
