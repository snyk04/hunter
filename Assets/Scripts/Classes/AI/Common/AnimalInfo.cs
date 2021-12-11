using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.Common
{
    public class AnimalInfo
    {
        public readonly IAnimal Animal;
        public readonly Transform Transform;
        public readonly Mover Mover;

        public readonly Field Field;
        
        public readonly float WanderingSpeed;
        public readonly float WanderingRadius;
        public readonly float DetectionRadius;
        
        public readonly float FleeSpeed;
        public readonly float FleeStopDistance;

        public readonly float SeekSpeed;
        public readonly float SeekStopDistance;

        public readonly float KillingStartDistance;
        public readonly float KillingStopDistance;
        public readonly int KillingAmountOfDamage;
        public readonly float KillingAttackDelay;
        
        public readonly float BorderAvoidingStartDistance;
        public readonly float BorderAvoidingStopDistance;

        public readonly float StarvingDeathTime;

        public Vector2 Position => Transform.Position();
        
        public AnimalInfo(IAnimal animal, Transform transform, Mover mover, Field field,
            float wanderingSpeed = default, float wanderingRadius = default, float detectionRadius = default, 
            float fleeSpeed = default, float fleeStopDistance = default, 
            float seekSpeed = default, float seekStopDistance = default,
            float killingStartDistance = default, float killingStopDistance = default, int killingAmountOfDamage = default,
                float killingAttackDelay = default,
            float borderAvoidingStartDistance = default, float borderAvoidingStopDistance = default,
            float starvingDeathTime = default)
        {
            Animal = animal;
            Transform = transform;
            Mover = mover;

            Field = field;
            
            WanderingSpeed = wanderingSpeed;
            WanderingRadius = wanderingRadius;
            DetectionRadius = detectionRadius;
            
            FleeSpeed = fleeSpeed;
            FleeStopDistance = fleeStopDistance;

            SeekSpeed = seekSpeed;
            SeekStopDistance = seekStopDistance;

            KillingStartDistance = killingStartDistance;
            KillingStopDistance = killingStopDistance;
            KillingAmountOfDamage = killingAmountOfDamage;
            KillingAttackDelay = killingAttackDelay;
            
            BorderAvoidingStartDistance = borderAvoidingStartDistance;
            BorderAvoidingStopDistance = borderAvoidingStopDistance;

            StarvingDeathTime = starvingDeathTime;
        }
    }
}
