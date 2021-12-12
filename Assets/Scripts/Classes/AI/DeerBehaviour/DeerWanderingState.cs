using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerWanderingState : DeerState
    {
        private Vector2 _currentVelocity;

        public DeerWanderingState(DeerInfo deerInfo) : base(deerInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                DeerInfo.Animal.ChangeState(new DeerFleeState(DeerInfo, pursuers));
                return;
            }
            
            _currentVelocity = DeerInfo.Rigidbody2D.velocity.normalized;
            if (DeerNearby(out DeerInfo[] deerInfos))
            {
                Vector2 separation = ComputeSeparation(deerInfos);
                Vector2 alignment = ComputeAlignment(deerInfos);
                Vector2 cohesion = ComputeCohesion(deerInfos);

                _currentVelocity += separation * DeerInfo.SeparationForce
                                    + alignment * DeerInfo.AlignmentForce 
                                    + cohesion * DeerInfo.CohesionForce;
            }
            
            while (!DeerInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, DeerInfo.BorderAvoidingStartDistance)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }

            DeerInfo.Mover.Move(_currentVelocity.normalized, DeerInfo.WanderingSpeed);
        }
    }
}
