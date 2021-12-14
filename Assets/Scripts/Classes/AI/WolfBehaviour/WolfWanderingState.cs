using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfWanderingState : WolfState
    {
        // TODO : to AnimalInfo
        private const float CircleDistance = 1;
        private const float CircleRadius = 1;
        
        public WolfWanderingState(WolfInfo wolfInfo) : base(wolfInfo) { }
        
        public override void Update()
        {
            base.Update();
            
            if (TargetNearby(out Transform pursuer))
            {
                WolfInfo.Animal.ChangeState(new WolfSeekState(WolfInfo, pursuer));
                return;
            }
            
            CurrentVelocity = WolfInfo.Rigidbody2D.velocity.magnitude > 0
                ? WolfInfo.Rigidbody2D.velocity
                : Random.insideUnitCircle;            
            CurrentVelocity += ComputeWanderVelocity();

            AvoidBorders();
            Move();
        }
        
        private Vector2 ComputeWanderVelocity()
        {
            Vector2 circleCenterVector = CurrentVelocity * CircleDistance;
            Vector3 displacement = Quaternion.Euler(0, 0, Random.Range(-15f, 15f)) * CurrentVelocity * CircleRadius;

            return circleCenterVector + (Vector2)displacement;
        }

        private void Move()
        {
            WolfInfo.Mover.Move(CurrentVelocity.normalized, WolfInfo.WanderingSpeed);
        }
    }
}
