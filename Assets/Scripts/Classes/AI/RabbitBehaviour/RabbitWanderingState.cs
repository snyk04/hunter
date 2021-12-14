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
                ? RabbitInfo.Rigidbody2D.velocity
                : Random.insideUnitCircle * RabbitInfo.WanderingSpeed;
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
            RabbitInfo.Mover.Move(CurrentVelocity.normalized, RabbitInfo.WanderingSpeed);
        }
    }
}
