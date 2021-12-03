using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.Rabbit
{
    public class Rabbit : IAnimal
    { 
        private IState _state;
        
        public AnimalInfo AnimalInfo { get; }

        public Rabbit(Transform transform, Mover mover, float wanderingSpeed, float wanderingRadius,
            float detectionRadius, float fleeSpeed, float fleeStopDistance)
        {
            AnimalInfo = new AnimalInfo(
                this,
                transform,
                mover,
                wanderingSpeed,
                wanderingRadius,
                detectionRadius,
                fleeSpeed,
                fleeStopDistance
            );
            
            _state = new WanderingState(AnimalInfo);
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
