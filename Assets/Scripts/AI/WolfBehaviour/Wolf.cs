using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class Wolf : Animal
    {
        public Wolf(Transform transform, Mover mover, 
            float wanderingSpeed, float wanderingRadius, float detectionRadius, 
            float seekSpeed, float seekStopDistance,
            float killingStartDistance, float killingStopDistance, int killingAmountOfDamage, float killingAttackDelay)
        {
            var animalInfo = new AnimalInfo(
                this, 
                transform, 
                mover,
                wanderingSpeed,
                wanderingRadius,
                detectionRadius,
                seekSpeed: seekSpeed,
                seekStopDistance: seekStopDistance,
                killingStartDistance: killingStartDistance,
                killingStopDistance: killingStopDistance,
                killingAmountOfDamage: killingAmountOfDamage,
                killingAttackDelay: killingAttackDelay
                );

            State = new WolfWanderingState(animalInfo);
        }
    }
}
