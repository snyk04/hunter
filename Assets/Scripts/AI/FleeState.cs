using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    public class FleeState : State
    {
        private Transform _pursuer;
        
        public FleeState(Transform pursuer, IAnimal animal, Transform transform, Mover mover) : base(animal, transform,
            mover)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            Vector2 direction = Transform.position - _pursuer.position;
            Mover.Move(direction.normalized);
        }
    }
}
