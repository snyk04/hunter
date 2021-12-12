using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class WolfComponent : MonoBehaviour
    {
        [Header("Wandering")] 
        [SerializeField] private float _wanderingSpeed;

        [Header("Seek")] 
        [SerializeField] private float _seekSpeed;
        [SerializeField] private float _seekStartDistance;
        [SerializeField] private float _seekStopDistance;

        [Header("Killing")] 
        [SerializeField] private float _killingStartDistance;
        [SerializeField] private float _killingStopDistance;
        [SerializeField] private int _killingAmountOfDamage;
        [SerializeField] private float _killingAttackDelay;

        [Header("Other")] 
        [SerializeField] private float _starvingDeathTime;

        private Wolf _wolf;

        private void Update()
        {
            _wolf.Update();
        }

        public void Initialize(Field field)
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            var rb2D = GetComponent<Rigidbody2D>();

            _wolf = new Wolf(
                transform,
                mover,
                rb2D,
                field,
                _wanderingSpeed,
                _seekSpeed,
                _seekStartDistance,
                _seekStopDistance,
                _killingStartDistance,
                _killingStopDistance,
                _killingAmountOfDamage,
                _killingAttackDelay,
                _starvingDeathTime
            );
        }
    }
}
