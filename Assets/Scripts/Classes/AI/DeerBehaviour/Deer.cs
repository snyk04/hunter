using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class Deer : Animal
    {
        public DeerInfo DeerInfo { get; }
        
        public Deer(Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float borderAvoidingStartDistance,
            float wanderingSpeed,
            float fleeSpeed, float fleeStartDistance, float fleeStopDistance,
            float separationForce, float alignmentForce, float cohesionForce, DeerGroup deerGroup)
        {
            DeerInfo = new DeerInfo(
                this,
                transform,
                mover,
                rigidbody2D,
                field,
                borderAvoidingStartDistance,
                wanderingSpeed,
                fleeSpeed,
                fleeStartDistance,
                fleeStopDistance,
                separationForce,
                alignmentForce,
                cohesionForce,
                deerGroup
            );

            State = new DeerWanderingState(DeerInfo);
        }
    }
}
