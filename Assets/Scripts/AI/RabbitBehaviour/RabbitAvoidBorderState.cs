using Hunter.AI.Common;
using Hunter.Common;
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
            
            _fleeDirection = (AnimalInfo.Position - _pursuer.Position()).normalized;
            if (AnimalInfo.Position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder 
                || AnimalInfo.Position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder)
            {
                _fleeDirection = Quaternion.Euler(0, 0, 90) * _fleeDirection;
            }
            if (AnimalInfo.Position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder 
                || AnimalInfo.Position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                _fleeDirection = Quaternion.Euler(0, 0, 90) * _fleeDirection;
            }
        }

        public override void Update()
        {
            if (AnimalInfo.Position.x + AnimalInfo.BorderAvoidingStopDistance < AnimalInfo.Field.XRightBorder
                && AnimalInfo.Position.x - AnimalInfo.BorderAvoidingStopDistance > AnimalInfo.Field.XLeftBorder
                && AnimalInfo.Position.y + AnimalInfo.BorderAvoidingStopDistance < AnimalInfo.Field.YTopBorder
                && AnimalInfo.Position.y - AnimalInfo.BorderAvoidingStopDistance > AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, _pursuer));
                return;
            }
            
            if (AnimalInfo.Position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder 
                || AnimalInfo.Position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder
                || AnimalInfo.Position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder
                || AnimalInfo.Position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitAvoidBorderState(AnimalInfo, _pursuer));
            }

            AnimalInfo.Mover.Move(_fleeDirection.normalized, AnimalInfo.FleeSpeed);
        }
    }
}
