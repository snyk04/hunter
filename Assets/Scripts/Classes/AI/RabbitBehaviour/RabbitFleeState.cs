using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitFleeState : RabbitState
    {
        private readonly Transform _pursuer;

        private Vector2 _currentVelocity;
        
        public RabbitFleeState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            if (_pursuer == null)
            {
                ChangeAnimalState(new RabbitWanderingState(AnimalInfo));
                return;
            }
            
            Vector2 fleeDirection = AnimalInfo.Position - _pursuer.Position();
            if (fleeDirection.magnitude > AnimalInfo.FleeStopDistance)
            {
                ChangeAnimalState(new RabbitWanderingState(AnimalInfo));
                return;
            }

            if (PursuerNearby(out Transform pursuer))
            {
                if (pursuer != _pursuer)
                {
                    ChangeAnimalState(new RabbitFleeState(AnimalInfo, pursuer));
                    return;
                }
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;
            _currentVelocity += (AnimalInfo.Position - _pursuer.Position()).normalized;

            // TODO : to const
            while (!AnimalInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }
            
            AnimalInfo.Mover.Move(_currentVelocity.normalized, AnimalInfo.FleeSpeed);
        }
    }
}
