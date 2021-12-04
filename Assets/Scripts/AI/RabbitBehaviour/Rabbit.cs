using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class Rabbit : IAnimal
    { 
        private IState _state;

        public Rabbit(Transform transform, Mover mover, float wanderingSpeed, float wanderingRadius,
            float detectionRadius, float fleeSpeed, float fleeStopDistance)
        {
            var animalInfo = new AnimalInfo(
                this,
                transform,
                mover,
                wanderingSpeed,
                wanderingRadius,
                detectionRadius,
                fleeSpeed,
                fleeStopDistance
            );

            _state = new RabbitWanderingState(animalInfo);
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
