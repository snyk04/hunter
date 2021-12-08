using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class RabbitComponent : MonoBehaviour
    {
        [SerializeField] private FieldComponent _field;
        
        [Header("Wandering")] 
        [SerializeField] private float _wanderingSpeed;
        [SerializeField] private float _wanderingRadius;
        [SerializeField] private float _detectionRadius;

        [Header("Flee")] 
        [SerializeField] private float _fleeSpeed;
        [SerializeField] private float _fleeStopDistance;

        [Header("Border avoiding")] 
        [SerializeField] private float _borderAvoidingStartDistance;
        [SerializeField] private float _borderAvoidingStopDistance;

        private Rabbit _rabbit;
        
        private void Awake()
        {
            Mover mover = GetComponent<MoverComponent>().Mover;

            _rabbit = new Rabbit(transform, mover, _field.Field, 
                _wanderingSpeed, _wanderingRadius, _detectionRadius,
                _fleeSpeed, _fleeStopDistance,
                _borderAvoidingStartDistance, _borderAvoidingStopDistance);
        }
        private void Update()
        {
            _rabbit.Update();
        }
    }
}
