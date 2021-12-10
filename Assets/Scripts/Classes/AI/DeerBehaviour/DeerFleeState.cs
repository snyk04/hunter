using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerFleeState : DeerState
    {
        private readonly Transform _pursuer;

        private Vector2 _currentVelocity;
        
        public DeerFleeState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            if (_pursuer == null)
            {
                ChangeAnimalState(new DeerWanderingState(AnimalInfo));
                return;
            }
            
            Vector2 fleeDirection = AnimalInfo.Position - _pursuer.Position();
            if (fleeDirection.magnitude > AnimalInfo.FleeStopDistance)
            {
                ChangeAnimalState(new DeerWanderingState(AnimalInfo));
                return;
            }

            if (PursuerNearby(out Transform pursuer))
            {
                if (pursuer != _pursuer)
                {
                    ChangeAnimalState(new DeerFleeState(AnimalInfo, pursuer));
                    return;
                }
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;
            _currentVelocity += (AnimalInfo.Position - _pursuer.Position()).normalized;
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
            
            // // TODO : to const
            // if (AnimalInfo.Position.x - 5 <= AnimalInfo.Field.XLeftBorder 
            //     || AnimalInfo.Position.x + 5 >= AnimalInfo.Field.XRightBorder)
            // {
            //     Vector2 wallAvoidVector = new Vector2(-_currentVelocity.x, _currentVelocity.y).normalized;
            // }
            // if (AnimalInfo.Position.y - 5 <= AnimalInfo.Field.YBotBorder 
            //     || AnimalInfo.Position.y + 5 >= AnimalInfo.Field.YTopBorder)
            // { 
            //     Vector2 wallAvoidVector = new Vector2(_currentVelocity.x, -_currentVelocity.y).normalized; 
            // }
            
            AnimalInfo.Mover.Move(_currentVelocity.normalized, AnimalInfo.FleeSpeed);
        }
    }
}
