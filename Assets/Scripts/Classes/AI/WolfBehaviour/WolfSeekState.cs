using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfSeekState : WolfState
    {
        private readonly Transform _target;

        private Vector2 _currentVelocity;

        public WolfSeekState(WolfInfo wolfInfo, Transform target) : base(wolfInfo)
        {
            _target = target;
        }
        
        public override void Update()
        {
            base.Update();

            if (_target == null)
            {
                WolfInfo.Animal.ChangeState(new WolfWanderingState(WolfInfo));
            }

            Vector2 seekDirection = _target.Position() - WolfInfo.Position;
            if (seekDirection.magnitude >= WolfInfo.SeekStopDistance)
            {
                WolfInfo.Animal.ChangeState(new WolfWanderingState(WolfInfo));
            }

            if (seekDirection.magnitude <= WolfInfo.KillingStartDistance)
            {
                WolfInfo.Animal.ChangeState(new WolfKillingState(WolfInfo, _target));
            }
            
            _currentVelocity = WolfInfo.Rigidbody2D.velocity.normalized;
            _currentVelocity += seekDirection.normalized;

            // TODO : to const
            while (!WolfInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }
            
            WolfInfo.Mover.Move(seekDirection.normalized, WolfInfo.SeekSpeed);
        }
    }
}
