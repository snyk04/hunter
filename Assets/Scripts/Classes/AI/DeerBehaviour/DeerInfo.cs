using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerInfo : AnimalInfo
    {
        public readonly float WanderingSpeed;
        
        public readonly float FleeSpeed;
        public readonly float FleeStartDistance;
        public readonly float FleeStopDistance;
        
        public readonly float SeparationForce;
        public readonly float AlignmentForce;
        public readonly float CohesionForce;
        public readonly DeerGroup DeerGroup;
        
        public DeerInfo(IAnimal animal, Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float borderAvoidingStartDistance,
            float wanderingSpeed,
            float fleeSpeed, float fleeStartDistance, float fleeStopDistance,
            float separationForce, float alignmentForce, float cohesionForce, DeerGroup deerGroup)
            : base(animal, transform, mover, rigidbody2D, field, borderAvoidingStartDistance)
        {
            WanderingSpeed = wanderingSpeed;

            FleeSpeed = fleeSpeed;
            FleeStartDistance = fleeStartDistance;
            FleeStopDistance = fleeStopDistance;
            
            SeparationForce = separationForce;
            AlignmentForce = alignmentForce;
            CohesionForce = cohesionForce;
            DeerGroup = deerGroup;
        }
    }
}