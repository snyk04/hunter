using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitWanderingState : RabbitState
    {
        // TODO : to AnimalInfo
        private const float CircleDistance = 1;
        private const float CircleRadius = 1;
        
        public RabbitWanderingState(RabbitInfo rabbitInfo) : base(rabbitInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                RabbitInfo.Animal.ChangeState(new RabbitFleeState(RabbitInfo, pursuers));
                return;
            }

            CurrentVelocity = RabbitInfo.Rigidbody2D.velocity.magnitude > 0
                ? RabbitInfo.Rigidbody2D.velocity.normalized
                : Random.insideUnitCircle;
            CurrentVelocity += ComputeWanderVelocity();
            
            AvoidBorders();
            Move();
        }

        private Vector2 ComputeWanderVelocity()
        {
            Vector2 circleCenter = RabbitInfo.Position + CurrentVelocity * CircleDistance;
            Vector3 displacement = Quaternion.Euler(0, 0, Random.Range(-2f, 2f)) * CurrentVelocity * CircleRadius;
            Vector2 displacementPosition = circleCenter + new Vector2(displacement.x, displacement.y);
            Vector2 wanderVelocity = displacementPosition - circleCenter;

            return wanderVelocity - CurrentVelocity;
        }
        private void Move()
        {
            RabbitInfo.Mover.Move(CurrentVelocity.normalized, RabbitInfo.WanderingSpeed);
        }
    }
}
