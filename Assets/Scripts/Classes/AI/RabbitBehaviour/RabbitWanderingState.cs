using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitWanderingState : RabbitState
    {
        private Vector2 _currentVelocity;

        public RabbitWanderingState(RabbitInfo rabbitInfo) : base(rabbitInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                RabbitInfo.Animal.ChangeState(new RabbitFleeState(RabbitInfo, pursuers));
                return;
            }
            
            _currentVelocity = RabbitInfo.Rigidbody2D.velocity.normalized;

            // TODO : to const
            while (!RabbitInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }

            RabbitInfo.Mover.Move(_currentVelocity.normalized, RabbitInfo.WanderingSpeed);
        }
    }
}
