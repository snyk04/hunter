using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public abstract class RabbitState : State
    {
        protected RabbitState(AnimalInfo animalInfo) : base(animalInfo)
        {
        }
        
        protected bool LiveBeingNearby(out Transform liveBeing)
        {
            Collider2D nearbyObject = Physics2D.OverlapCircle(AnimalInfo.Transform.position, AnimalInfo.DetectionRadius);
            
            liveBeing = nearbyObject != null && nearbyObject.TryGetComponent(out MoverComponent _)
                ? nearbyObject.transform
                : null;

            return liveBeing != null;
        }
    }
}
