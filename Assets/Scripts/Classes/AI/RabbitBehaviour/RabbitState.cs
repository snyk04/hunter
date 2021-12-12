using System.Collections.Generic;
using System.Linq;
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
        
        protected bool PursuersNearby(out Transform[] pursuers)
        {
            var pursuersList = new List<Transform>();
            
            Physics2D.OverlapCircleNonAlloc(AnimalInfo.Position, AnimalInfo.DetectionRadius, _nearbyObjects);
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == AnimalInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent _))
                {
                    continue;
                }

                pursuersList.Add(nearbyObject.transform);
            }

            pursuers = pursuersList.ToArray();
            return pursuersList.Any();
        }
        
        protected Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return AnimalInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
