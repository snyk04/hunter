using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfWanderingState : WolfState
    {
        private Vector2 _currentVelocity;

        public WolfWanderingState(AnimalInfo animalInfo) : base(animalInfo) { }
        
        public override void Update()
        {
            if (TargetNearby(out Transform pursuer))
            {
                ChangeAnimalState(new WolfSeekState(AnimalInfo, pursuer));
                return;
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;

            // TODO : to const
            while (!AnimalInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }

            AnimalInfo.Mover.Move(_currentVelocity.normalized, AnimalInfo.WanderingSpeed);
        }
    }
}
