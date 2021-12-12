using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitInfo : AnimalInfo
    {
        public readonly float WanderingSpeed;
        
        public readonly float FleeSpeed;
        public readonly float FleeStartDistance;
        public readonly float FleeStopDistance;
        
        public RabbitInfo(IAnimal animal, Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float wanderingSpeed,
            float fleeSpeed, float fleeStartDistance, float fleeStopDistance) 
            : base(animal, transform, mover, rigidbody2D, field)
        {
            WanderingSpeed = wanderingSpeed;

            FleeSpeed = fleeSpeed;
            FleeStartDistance = fleeStartDistance;
            FleeStopDistance = fleeStopDistance;
        }
    }
}
