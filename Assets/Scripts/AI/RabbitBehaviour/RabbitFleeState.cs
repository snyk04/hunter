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
                StartWandering();
                return;
            }

            Vector2 fleeDirection = AnimalInfo.Transform.position - _pursuer.position;
            AnimalInfo.Mover.Move(fleeDirection.normalized, AnimalInfo.FleeSpeed);
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

        private void StartWandering()
        {
            ChangeAnimalState(new RabbitWanderingState(AnimalInfo));
        }
    }
}
