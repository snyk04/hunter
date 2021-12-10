using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class Deer : Animal
    {
        public Deer(Transform transform, Mover mover,
            Field field,
            float wanderingSpeed, float wanderingRadius, float detectionRadius,
            float fleeSpeed, float fleeStopDistance,
            float borderAvoidingStartDistance, float borderAvoidingStopDistance)
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
                fleeStopDistance,
                borderAvoidingStartDistance: borderAvoidingStartDistance,
                borderAvoidingStopDistance: borderAvoidingStopDistance
            );

            State = new DeerWanderingState(animalInfo);
        }
    }
}
