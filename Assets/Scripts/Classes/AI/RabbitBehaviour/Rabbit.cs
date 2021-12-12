using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class Rabbit : Animal
    {
        public Rabbit(Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float borderAvoidingStartDistance,
            float wanderingSpeed,
            float fleeSpeed, float fleeStartDistance, float fleeStopDistance)
        {
            var rabbitInfo = new RabbitInfo(
                this,
                transform,
                mover,
                rigidbody2D,
                field,
                borderAvoidingStartDistance,
                wanderingSpeed,
                fleeSpeed,
                fleeStartDistance,
                fleeStopDistance
                );

            State = new RabbitWanderingState(rabbitInfo);
        }
    }
}
