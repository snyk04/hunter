using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class Deer : Animal
    {
        public DeerInfo DeerInfo { get; }
        
        public Deer(Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float wanderingSpeed,
            float fleeSpeed, float fleeStartDistance, float fleeStopDistance)
        {
            DeerInfo = new DeerInfo(
                this,
                transform,
                mover,
                rigidbody2D,
                field,
                wanderingSpeed,
                fleeSpeed,
                fleeStartDistance,
                fleeStopDistance
            );

            State = new DeerWanderingState(DeerInfo);
        }
    }
}
