using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public abstract class RabbitState : State
    {
        private readonly Collider2D[] _nearbyObjects;

        protected RabbitState(AnimalInfo animalInfo) : base(animalInfo)
        {
            _nearbyObjects = new Collider2D[2];
        }
        
        protected bool LiveBeingNearby(out Transform liveBeing)
        {
            Physics2D.OverlapCircleNonAlloc(AnimalInfo.Position, AnimalInfo.DetectionRadius, _nearbyObjects);

            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == AnimalInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent _))
                {
                    continue;
                }

                liveBeing = nearbyObject.transform;
                return true;
            }

            liveBeing = null;
            return false;
        }
    }
}
