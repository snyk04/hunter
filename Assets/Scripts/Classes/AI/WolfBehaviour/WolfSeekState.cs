using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfSeekState : WolfState
    {
        private readonly Transform _target;

        private Vector2 _currentVelocity;

        public WolfSeekState(AnimalInfo animaInfo, Transform target) : base(animaInfo)
        {
            _target = target;
        }
        
        public override void Update()
        {
            base.Update();

            if (_target == null)
            {
                ChangeAnimalState(new WolfWanderingState(AnimalInfo));
            }

            Vector2 seekDirection = _target.Position() - AnimalInfo.Position;
            if (seekDirection.magnitude >= AnimalInfo.SeekStopDistance)
            {
                ChangeAnimalState(new WolfWanderingState(AnimalInfo));
            }

            if (seekDirection.magnitude <= AnimalInfo.KillingStartDistance)
            {
                ChangeAnimalState(new WolfKillingState(AnimalInfo, _target));
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;
            _currentVelocity += seekDirection.normalized;

            // TODO : to const
            while (!AnimalInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }
            
            AnimalInfo.Mover.Move(seekDirection.normalized, AnimalInfo.SeekSpeed);
        }
    }
}
