using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class RabbitComponent : MonoBehaviour
    {
        [Header("Wandering")] 
        [SerializeField] private float _wanderingSpeed;

        [Header("Flee")] 
        [SerializeField] private float _fleeSpeed;
        [SerializeField] private float _fleeStartDistance;
        [SerializeField] private float _fleeStopDistance;

        private Rabbit _rabbit;
        
        private void Update()
        {
            _rabbit.Update();
        }

        public void Initialize(Field field)
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            var rb2D = GetComponent<Rigidbody2D>();

            _rabbit = new Rabbit(
                transform, 
                mover, 
                rb2D, 
                field,
                _wanderingSpeed,
                _fleeSpeed, 
                _fleeStartDistance, 
                _fleeStopDistance
            );
        }
    }
}
