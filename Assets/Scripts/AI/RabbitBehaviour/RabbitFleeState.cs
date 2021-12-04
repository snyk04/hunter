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
            Vector2 fleeDirection = AnimalInfo.Transform.position - _pursuer.position;

            if (InSafety(fleeDirection.magnitude))
            {
                ChangeState(new RabbitWanderingState(AnimalInfo));
            }
            
            AnimalInfo.Mover.Move(fleeDirection.normalized, AnimalInfo.FleeSpeed);
        }

        private bool InSafety(float distance)
        {
            if (distance < AnimalInfo.FleeStopDistance)
            {
                return false;
            }

            if (LiveBeingNearby(out Transform liveBeing))
            {
                ChangeState(new RabbitFleeState(AnimalInfo, liveBeing));
                return false;
            }

            return true;
        }
    }
}
