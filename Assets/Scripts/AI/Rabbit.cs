using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    public class Rabbit : IAnimal
    { 
        private IState _state;

        public Rabbit(float wanderingSpeed, float wanderingRadius, float fleeSpeed, Transform transform, Mover mover)
        {
            _state = new WanderingState(this, wanderingSpeed, wanderingRadius, transform, mover);
        }
        
        public void Update()
        {
            _state.Update();
        }

        public void ChangeState(IState state)
        {
            _state = state;
        }
    }
}