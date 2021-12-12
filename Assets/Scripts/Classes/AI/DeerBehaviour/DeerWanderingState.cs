using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerWanderingState : DeerState
    {
        public DeerWanderingState(DeerInfo deerInfo) : base(deerInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                DeerInfo.Animal.ChangeState(new DeerFleeState(DeerInfo, pursuers));
                return;
            }
            
            CurrentVelocity = DeerInfo.Rigidbody2D.velocity.normalized;
            CurrentVelocity += ComputeDeerGroupVelocity();
            
            AvoidBorders();
            Move();
        }

        private void Move()
        {
            DeerInfo.Mover.Move(CurrentVelocity.normalized, DeerInfo.WanderingSpeed);
        }
    }
}
