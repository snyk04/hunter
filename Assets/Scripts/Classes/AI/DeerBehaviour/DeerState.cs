using Hunter.AI.Common;
using Hunter.AI.RabbitBehaviour;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public abstract class DeerState : State
    {
        private readonly Collider2D[] _nearbyObjects;

        protected DeerState(AnimalInfo animalInfo) : base(animalInfo)
        {
            _nearbyObjects = new Collider2D[12];
        }
        
        protected bool LiveBeingNearby(out Transform liveBeing)
        {
            Physics2D.OverlapCircleNonAlloc(AnimalInfo.Position, AnimalInfo.DetectionRadius, _nearbyObjects);

            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == AnimalInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent mover)
                    || nearbyObject.TryGetComponent(out DeerComponent deer)
                    || nearbyObject.TryGetComponent(out RabbitComponent rabbit))
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