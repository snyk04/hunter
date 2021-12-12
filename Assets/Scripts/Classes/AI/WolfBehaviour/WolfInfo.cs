using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfInfo : AnimalInfo
    {
        public readonly Damageable Damageable;
        
        public readonly float WanderingSpeed;

        public readonly float SeekSpeed;
        public readonly float SeekStartDistance;
        public readonly float SeekStopDistance;

        public readonly float KillingStartDistance;
        public readonly float KillingStopDistance;
        public readonly int KillingAmountOfDamage;
        public readonly float KillingAttackDelay;
        
        public readonly float StarvingDeathTime;
        
        public WolfInfo(IAnimal animal, Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            Damageable damageable,
            float borderAvoidingStartDistance,
            float wanderingSpeed, 
            float seekSpeed, float seekStartDistance, float seekStopDistance, 
            float killingStartDistance, float killingStopDistance, int killingAmountOfDamage, float killingAttackDelay, 
            float starvingDeathTime) 
            : base(animal, transform, mover, rigidbody2D, field, borderAvoidingStartDistance)
        {
            Damageable = damageable;
            
            WanderingSpeed = wanderingSpeed;
            
            SeekSpeed = seekSpeed;
            SeekStartDistance = seekStartDistance;
            SeekStopDistance = seekStopDistance;
            
            KillingStartDistance = killingStartDistance;
            KillingStopDistance = killingStopDistance;
            KillingAmountOfDamage = killingAmountOfDamage;
            KillingAttackDelay = killingAttackDelay;
            
            StarvingDeathTime = starvingDeathTime;
        }
    }
}
