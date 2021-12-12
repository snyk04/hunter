using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class Wolf : Animal
    {
        public Wolf(Transform transform, Mover mover, Rigidbody2D rigidbody2D, Field field,
            float wanderingSpeed,
            float seekSpeed, float seekStartDistance, float seekStopDistance,
            float killingStartDistance, float killingStopDistance, int killingAmountOfDamage, float killingAttackDelay,
            float starvingDeathTime)
        {
            var wolfInfo = new WolfInfo(
                this, 
                transform, 
                mover,
                rigidbody2D,
                field,
                wanderingSpeed,
                seekSpeed,
                seekStartDistance,
                seekStopDistance,
                killingStartDistance,
                killingStopDistance,
                killingAmountOfDamage,
                killingAttackDelay,
                starvingDeathTime
            );
            
            State = new WolfWanderingState(wolfInfo);
        }
    }
}
