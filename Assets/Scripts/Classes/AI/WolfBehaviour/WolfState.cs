using System;
using Hunter.AI.Common;
using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public abstract class WolfState : State
    {
        private readonly Collider2D[] _nearbyObjects;

        protected DateTime _lastMealTime;

        protected WolfState(AnimalInfo animaInfo) : base(animaInfo)
        {
            _nearbyObjects = new Collider2D[2];
            _lastMealTime = DateTime.Now;
        }

        public override void Update()
        {
            if (_lastMealTime.GetPassedSeconds() >= AnimalInfo.StarvingDeathTime)
            {
                // TODO : hardcode!!!
                AnimalInfo.Transform.GetComponent<DamageableComponent>().Damageable.Destroy();
            }
        }
        
        protected bool TargetNearby(out Transform target)
        {
            Physics2D.OverlapCircleNonAlloc(AnimalInfo.Position, AnimalInfo.DetectionRadius, _nearbyObjects);

            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == AnimalInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent _)
                    || nearbyObject.TryGetComponent(out WolfComponent wolfComponent))
                {
                    continue;
                }

                target = nearbyObject.transform;
                return true;
            }

            target = null;
            return false;
        }
        
        protected Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return AnimalInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
