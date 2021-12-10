using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerWanderingState : DeerState
    {
        private Vector2 _currentVelocity;

        public DeerWanderingState(AnimalInfo animalInfo) : base(animalInfo) { }
        
        public override void Update()
        {
            if (PursuerNearby(out Transform pursuer))
            {
                ChangeAnimalState(new DeerFleeState(AnimalInfo, pursuer));
                return;
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;
            if (DeerNearby(out Deer[] deer))
            {
                Vector2 separation = ComputeSeparation(deer);
                Vector2 alignment = ComputeAlignment(deer);
                Vector2 cohesion = ComputeCohesion(deer);

                _currentVelocity += separation * SeparationForce
                                    + alignment * AlignmentForce 
                                    + cohesion * CohesionForce;
            }
            
            // TODO : to const
            while (!AnimalInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }

            AnimalInfo.Mover.Move(_currentVelocity.normalized, AnimalInfo.WanderingSpeed);
        }
    }
}
