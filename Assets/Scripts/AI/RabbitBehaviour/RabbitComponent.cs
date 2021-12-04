using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class RabbitComponent : MonoBehaviour
    {
        [Header("Wandering")] 
        [SerializeField] private float _wanderingSpeed;
        [SerializeField] private float _wanderingRadius;
        [SerializeField] private float _detectionRadius;

        [Header("Flee")] 
        [SerializeField] private float _fleeSpeed;
        [SerializeField] private float _fleeStopDistance;

        private Rabbit _rabbit;
        
        private void Awake()
        {
            Mover mover = GetComponent<MoverComponent>().Mover;

            _rabbit = new Rabbit(transform, mover, _wanderingSpeed, _wanderingRadius,
                _detectionRadius, _fleeSpeed, _fleeStopDistance);
        }
        private void Update()
        {
            _rabbit.Update();
        }
    }
}
