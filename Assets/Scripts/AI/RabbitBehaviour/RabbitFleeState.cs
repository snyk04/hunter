using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitFleeState : RabbitState
    {
        private readonly Transform _pursuer;
        
        public RabbitFleeState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            if (InSafety())
            {
                ChangeAnimalState(new RabbitWanderingState(AnimalInfo));
                return;
            }
            
            if (AnimalInfo.Transform.position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder
                || AnimalInfo.Transform.position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder
                || AnimalInfo.Transform.position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder
                || AnimalInfo.Transform.position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitAvoidBorderState(AnimalInfo, _pursuer));
                return;
            }
            
            Vector2 fleeDirection = (AnimalInfo.Transform.position - _pursuer.position).normalized;
            AnimalInfo.Mover.Move(fleeDirection, AnimalInfo.FleeSpeed);
        }

        private bool InSafety()
        {
            if (_pursuer == null)
            {
                return true;
            }
            
            Vector2 fleeDirection = AnimalInfo.Transform.position - _pursuer.position;
            if (fleeDirection.magnitude < AnimalInfo.FleeStopDistance)
            {
                return false;
            }

            if (LiveBeingNearby(out Transform liveBeing))
            {
                // TODO : multiple Move() calls
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, liveBeing));
                return false;
            }

            return true;
        }
    }
}
