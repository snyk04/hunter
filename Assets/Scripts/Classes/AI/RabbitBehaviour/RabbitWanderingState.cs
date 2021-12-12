using System.Collections.Generic;
using Hunter.AI.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitWanderingState : RabbitState
    {
        private Vector2 _currentVelocity;

        public RabbitWanderingState(AnimalInfo animalInfo) : base(animalInfo) { }
        
        public override void Update()
        {
            if (PursuersNearby(out Transform[] pursuers))
            {
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, pursuers));
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
