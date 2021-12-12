using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfWanderingState : WolfState
    {
        private Vector2 _currentVelocity;

        public WolfWanderingState(WolfInfo wolfInfo) : base(wolfInfo) { }
        
        public override void Update()
        {
            base.Update();
            
            if (TargetNearby(out Transform pursuer))
            {
                WolfInfo.Animal.ChangeState(new WolfSeekState(WolfInfo, pursuer));
                return;
            }
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = WolfInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;

            // TODO : to const
            while (!WolfInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }

            WolfInfo.Mover.Move(_currentVelocity.normalized, WolfInfo.WanderingSpeed);
        }
    }
}
