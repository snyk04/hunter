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
        protected Vector2 CurrentVelocity;
        
        private readonly Collider2D[] _nearbyObjects;
        protected DateTime LastMealTime;

        protected WolfState(WolfInfo wolfInfo)
        {
            WolfInfo = wolfInfo;
            
            _nearbyObjects = new Collider2D[2];
            LastMealTime = DateTime.Now;
        }

        public override void Update()
        {
            if (LastMealTime.GetPassedSeconds() >= WolfInfo.StarvingDeathTime)
            {
                WolfInfo.Damageable.Destroy();
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
        
        protected void AvoidBorders()
        {
            Vector2 desiredVelocity = Vector2.zero;
            if (WolfInfo.Position.x - WolfInfo.BorderAvoidingStartDistance <= WolfInfo.Field.XLeftBorder)
            {
                desiredVelocity += Vector2.right;
            }
            if (WolfInfo.Position.x + WolfInfo.BorderAvoidingStartDistance >= WolfInfo.Field.XRightBorder)
            {
                desiredVelocity += Vector2.left;
            }
            if (WolfInfo.Position.y - WolfInfo.BorderAvoidingStartDistance <= WolfInfo.Field.YBotBorder)
            {
                desiredVelocity += Vector2.up;
            }
            if (WolfInfo.Position.y + WolfInfo.BorderAvoidingStartDistance >= WolfInfo.Field.YTopBorder)
            {
                desiredVelocity += Vector2.down;
            }
            
            if (desiredVelocity != Vector2.zero)
            {
                // TODO : to const
                Vector2 steeringVector = (desiredVelocity - CurrentVelocity).normalized * WolfInfo.SeekSpeed * 0.3f;
                CurrentVelocity += steeringVector;
            }
        }
    }
}
