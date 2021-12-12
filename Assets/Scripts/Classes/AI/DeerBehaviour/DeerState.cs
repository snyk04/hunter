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
        // TODO : to AnimalInfo
        protected const float SeparationForce = 1;
        protected const float AlignmentForce = 1;
        protected const float CohesionForce = 1;
        
        private const float FriendsDetectionRadius = 50;
        
        private readonly Collider2D[] _nearbyObjects;

        protected DeerState(AnimalInfo animalInfo) : base(animalInfo)
        {
            _nearbyObjects = new Collider2D[12];
        }
        
        protected bool PursuersNearby(out Transform[] pursuers)
        {
            var pursuersList = new List<Transform>();
            
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

                pursuersList.Add(nearbyObject.transform);
            }
            
            pursuers = pursuersList.ToArray();
            return pursuersList.Any();
        }
        protected bool DeerNearby(out Deer[] deer)
        {
            var deerList = new List<Deer>();
            
            Physics2D.OverlapCircleNonAlloc(AnimalInfo.Position, FriendsDetectionRadius, _nearbyObjects);
            
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == AnimalInfo.Transform.gameObject 
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
                separation += neighbourDeer.AnimalInfo.Position - AnimalInfo.Position;
            }

            separation *= -1;
            return separation.normalized;
        }
        protected Vector2 ComputeAlignment(Deer[] deer)
        {
            Vector2 alignment = Vector2.zero;
            foreach (Deer neighbourDeer in deer)
            {
                alignment += neighbourDeer.AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity;
            }
            alignment /= deer.Count();

            return alignment.normalized;
        }
        protected Vector2 ComputeCohesion(Deer[] deer)
        {
            Vector2 cohesion = Vector2.zero;
            foreach (Deer neighbourDeer in deer)
            {
                cohesion += neighbourDeer.AnimalInfo.Position;
            }
            cohesion /= deer.Count();
            cohesion -= AnimalInfo.Position;

            return cohesion.normalized;
        }
        
        protected Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return AnimalInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
