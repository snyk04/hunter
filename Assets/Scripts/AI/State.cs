using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    public abstract class State : IState
    {
        private readonly IAnimal _animal;
        protected readonly Transform Transform;
        protected readonly Mover Mover; 
        
        protected State(IAnimal animal, Transform transform, Mover mover)
        {
            _animal = animal;
            Transform = transform;
            Mover = mover;
        }
        
        public abstract void Update();

        protected void ChangeState(IState state)
        {
            _animal.ChangeState(state);
        }
    }
}
