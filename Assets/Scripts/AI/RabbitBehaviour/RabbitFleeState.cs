using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitFleeState : RabbitState
    {
        private readonly Transform _pursuer;

        private Vector2 FleeDirection => AnimalInfo.Transform.position - _pursuer.position;
        
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
            
            AnimalInfo.Mover.Move(FleeDirection.normalized, AnimalInfo.FleeSpeed);
        }

        private bool InSafety()
        {
            if (_pursuer == null)
            {
                return true;
            }
            
            if (FleeDirection.magnitude < AnimalInfo.FleeStopDistance)
            {
                return false;
            }

            if (LiveBeingNearby(out Transform liveBeing))
            {
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
