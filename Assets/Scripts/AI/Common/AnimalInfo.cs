using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.Common
{
    public class AnimalInfo
    {
        public readonly IAnimal Animal;
        public readonly Transform Transform;
        public readonly Mover Mover; 
        
        public readonly float WanderingSpeed;
        public readonly float WanderingRadius;
        public readonly float DetectionRadius;
        
        public readonly float FleeSpeed;
        public readonly float FleeStopDistance;

        public AnimalInfo(IAnimal animal, Transform transform, Mover mover, float wanderingSpeed = default,
            float wanderingRadius = default, float detectionRadius = default, float fleeSpeed = default, 
            float fleeStopDistance = default)
        {
            Animal = animal;
            Transform = transform;
            Mover = mover;
            
            WanderingSpeed = wanderingSpeed;
            WanderingRadius = wanderingRadius;
            DetectionRadius = detectionRadius;
            
            FleeSpeed = fleeSpeed;
            FleeStopDistance = fleeStopDistance;
        }
    }
}
