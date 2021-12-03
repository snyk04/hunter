using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    public class FleeState : State
    {
        private readonly float _fleeSpeed;
        private readonly Transform _pursuer;
        
        public FleeState(float fleeSpeed, Transform pursuer, IAnimal animal, Transform transform, Mover mover) : base(animal, transform,
            mover)
        {
            _fleeSpeed = fleeSpeed;
            _pursuer = pursuer;
        }

        public override void Update()
        {
            Vector2 direction = Transform.position - _pursuer.position;
            Mover.Move(direction.normalized, _fleeSpeed);
        }
    }
}
