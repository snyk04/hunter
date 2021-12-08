using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class WolfComponent : MonoBehaviour
    {
        [SerializeField] private FieldComponent _field;
        
        [Header("Wandering")]
        [SerializeField] private float _wanderingSpeed;
        [SerializeField] private float _wanderingRadius;
        [SerializeField] private float _detectionRadius;

        [Header("Seek")]
        [SerializeField] private float _seekSpeed;
        [SerializeField] private float _seekStopDistance;
        
        [Header("Killing")]
        [SerializeField] private float _killingStartDistance;
        [SerializeField] private float _killingStopDistance;
        [SerializeField] private int _killingAmountOfDamage;
        [SerializeField] private float _killingAttackDelay;

        private Wolf _wolf;

        private void Awake()
        {
            Mover mover = GetComponent<MoverComponent>().Mover;

            _wolf = new Wolf(transform, mover, _field.Field,
                _wanderingSpeed, _wanderingRadius, _detectionRadius,
                _seekSpeed, _seekStopDistance,
                _killingStartDistance, _killingStopDistance, _killingAmountOfDamage, _killingAttackDelay);
        }
        private void Update()
        {
            _wolf.Update();
        }
    }
}
