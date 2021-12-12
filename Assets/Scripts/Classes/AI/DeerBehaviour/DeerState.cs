using System.Collections.Generic;
using System.Linq;
using Hunter.AI.Common;
using Hunter.AI.RabbitBehaviour;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public abstract class DeerState : State
    {
        private const float FriendsDetectionRadius = 50;
        
        protected DeerInfo DeerInfo { get; }
        
        private readonly Collider2D[] _nearbyObjects;

        protected DeerState(DeerInfo deerInfo)
        {
            DeerInfo = deerInfo;
            
            _nearbyObjects = new Collider2D[12];
        }
        
        protected bool PursuersNearby(out Transform[] pursuers)
        {
            var pursuersList = new List<Transform>();
            
            Physics2D.OverlapCircleNonAlloc(DeerInfo.Position, DeerInfo.FleeStartDistance, _nearbyObjects);
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == DeerInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent mover)
                    || nearbyObject.TryGetComponent(out DeerComponent deer)
                    || nearbyObject.TryGetComponent(out RabbitComponent rabbit))
                {
                    continue;
                }

                pursuersList.Add(nearbyObject.transform);
            }
            
            pursuers = pursuersList.ToArray();
            return pursuersList.Any();
        }
        protected bool DeerNearby(out Deer[] deer)
        {
            var deerList = new List<Deer>();
            
            Physics2D.OverlapCircleNonAlloc(DeerInfo.Position, FriendsDetectionRadius, _nearbyObjects);
            
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == DeerInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out DeerComponent deerComponent))
                {
                    continue;
                }

                deerList.Add(deerComponent.Deer);
            }

            deer = deerList.ToArray();

            return deerList.Any();
        }
        
        protected Vector2 ComputeSeparation(IEnumerable<Deer> deer)
        {
            Vector2 separation = Vector2.zero;
            foreach (Deer neighbourDeer in deer)
            {
                separation += neighbourDeer.DeerInfo.Position - DeerInfo.Position;
            }

            separation *= -1;
            return separation.normalized;
        }
        protected Vector2 ComputeAlignment(Deer[] deer)
        {
            Vector2 alignment = Vector2.zero;
            foreach (Deer neighbourDeer in deer)
            {
                alignment += neighbourDeer.DeerInfo.Transform.GetComponent<Rigidbody2D>().velocity;
            }
            alignment /= deer.Count();

            return alignment.normalized;
        }
        protected Vector2 ComputeCohesion(Deer[] deer)
        {
            Vector2 cohesion = Vector2.zero;
            foreach (Deer neighbourDeer in deer)
            {
                cohesion += neighbourDeer.DeerInfo.Position;
            }
            cohesion /= deer.Count();
            cohesion -= DeerInfo.Position;

            return cohesion.normalized;
        }
        
        protected Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return DeerInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
