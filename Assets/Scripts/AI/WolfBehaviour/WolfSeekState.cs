using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfSeekState : WolfState
    {
        private readonly Transform _target;

        public WolfSeekState(AnimalInfo animaInfo, Transform target) : base(animaInfo)
        {
            _target = target;
        }
        
        public override void Update()
        {
            if (_target == null)
            {
                ChangeAnimalState(new WolfWanderingState(AnimalInfo));
            }

            Vector2 seekDirection = _target.position - AnimalInfo.Transform.position;
            if (seekDirection.magnitude >= AnimalInfo.SeekStopDistance)
            {
                ChangeAnimalState(new WolfWanderingState(AnimalInfo));
            }

            if (seekDirection.magnitude <= AnimalInfo.KillingStartDistance)
            {
                ChangeAnimalState(new WolfKillingState(AnimalInfo, _target));
            }
            
            AnimalInfo.Mover.Move(seekDirection.normalized, AnimalInfo.SeekSpeed);
        }
    }
}
