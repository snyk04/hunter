using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.Rabbit
{
    public class RabbitFleeState : State
    {
        private readonly Transform _pursuer;
        
        public RabbitFleeState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            Vector2 direction = AnimalInfo.Transform.position - _pursuer.position;

            CheckPursuerDistance(direction.magnitude);
            
            AnimalInfo.Mover.Move(direction.normalized, AnimalInfo.FleeSpeed);
        }

        private void CheckPursuerDistance(float distance)
        {
            if (distance >= AnimalInfo.FleeStopDistance)
            {
                ChangeState(new WanderingState(AnimalInfo));
            }
        }
    }
}
