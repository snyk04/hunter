using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerWanderingState : DeerState
    {
        // TODO : to AnimalInfo
        private const float CircleDistance = 1;
        private const float CircleRadius = 1;
        
        public DeerWanderingState(DeerInfo deerInfo) : base(deerInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                DeerInfo.Animal.ChangeState(new DeerFleeState(DeerInfo, pursuers));
                return;
            }
            
            CurrentVelocity = DeerInfo.Rigidbody2D.velocity.magnitude > 0
                ? DeerInfo.Rigidbody2D.velocity.normalized
                : Random.insideUnitCircle;            
            CurrentVelocity += ComputeWanderVelocity();
            CurrentVelocity += ComputeDeerGroupVelocity();

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
            DeerInfo.Mover.Move(CurrentVelocity.normalized, DeerInfo.WanderingSpeed);
        }
    }
}
