using System;
using Hunter.AI.Common;
using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public abstract class WolfState : State
    {
        protected WolfInfo WolfInfo { get; }
        
        private readonly Collider2D[] _nearbyObjects;
        // TODO : update after killing
        protected DateTime _lastMealTime;

        protected WolfState(WolfInfo wolfInfo)
        {
            WolfInfo = wolfInfo;
            
            _nearbyObjects = new Collider2D[2];
            _lastMealTime = DateTime.Now;
        }

        public override void Update()
        {
            if (_lastMealTime.GetPassedSeconds() >= WolfInfo.StarvingDeathTime)
            {
                // TODO : hardcode!!!
                WolfInfo.Transform.GetComponent<DamageableComponent>().Damageable.Destroy();
            }
        }
        
        protected bool TargetNearby(out Transform target)
        {
            Physics2D.OverlapCircleNonAlloc(WolfInfo.Position, WolfInfo.SeekStartDistance, _nearbyObjects);

            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == WolfInfo.Transform.gameObject 
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
            return WolfInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
