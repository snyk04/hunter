using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public abstract class WolfState : State
    {
        protected WolfState(AnimalInfo animaInfo) : base(animaInfo) { }
        
        protected bool LiveBeingNearby(out Transform liveBeing)
        {
            Collider2D nearbyObject = Physics2D.OverlapCircle(AnimalInfo.Transform.position, AnimalInfo.DetectionRadius);
            
            liveBeing = nearbyObject != null && nearbyObject.TryGetComponent(out MoverComponent _) && nearbyObject.gameObject != AnimalInfo.Transform.gameObject
                ? nearbyObject.transform
                : null;

            return liveBeing != null;
        }
    }
}
