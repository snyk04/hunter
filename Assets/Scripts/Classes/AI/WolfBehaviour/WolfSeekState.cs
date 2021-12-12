using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfSeekState : WolfState
    {
        private readonly Transform _target;
        
        public WolfSeekState(WolfInfo wolfInfo, Transform target) : base(wolfInfo)
        {
            _target = target;
        }
        
        public override void Update()
        {
            base.Update();

            if (_target == null)
            {
                WolfInfo.Animal.ChangeState(new WolfWanderingState(WolfInfo));
            }

            if (TargetNearby(out Transform target))
            {
                if (target != _target)
                {
                    float distanceToOldTarget = (_target.Position() - WolfInfo.Position).magnitude;
                    float distanceToNewTarget = (target.Position() - WolfInfo.Position).magnitude;
                    if (distanceToNewTarget < distanceToOldTarget)
                    {
                        WolfInfo.Animal.ChangeState(new WolfSeekState(WolfInfo, target));
                        return;
                    }
                }
            }
            
            Vector2 seekDirection = _target.Position() - WolfInfo.Position;
            if (seekDirection.magnitude >= WolfInfo.SeekStopDistance)
            {
                WolfInfo.Animal.ChangeState(new WolfWanderingState(WolfInfo));
            }

            if (seekDirection.magnitude <= WolfInfo.KillingStartDistance)
            {
                WolfInfo.Animal.ChangeState(new WolfKillingState(WolfInfo, _target));
            }
            
            CurrentVelocity = WolfInfo.Rigidbody2D.velocity.normalized;
            CurrentVelocity += ComputeSeekVelocity();

            AvoidBorders();
            Move();
        }

        private Vector2 ComputeSeekVelocity()
        {
            Vector2 seekDirection = _target.Position() - WolfInfo.Position;
            Vector2 desiredVelocity = seekDirection.normalized;
            return desiredVelocity - CurrentVelocity;
        }
        private void Move()
        {
            WolfInfo.Mover.Move(CurrentVelocity.normalized, WolfInfo.SeekSpeed);
        }
    }
}
