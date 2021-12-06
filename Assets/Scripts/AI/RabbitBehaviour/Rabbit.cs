using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class Rabbit : Animal
    {
        public Rabbit(Transform transform, Mover mover,
            Field field,
            float wanderingSpeed, float wanderingRadius, float detectionRadius,
            float fleeSpeed, float fleeStopDistance)
        {
            var animalInfo = new AnimalInfo(
                this,
                transform,
                mover,
                field,
                wanderingSpeed,
                wanderingRadius,
                detectionRadius,
                fleeSpeed,
                fleeStopDistance
            );

            State = new RabbitWanderingState(animalInfo);
        }
    }
}
