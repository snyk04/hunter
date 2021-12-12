using System.Collections;
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
        [SerializeField] private float _borderAvoidingStartDistance;
        [SerializeField] private float _starvingDeathTime;

        private Wolf _wolf;

        public void Initialize(Field field)
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            var rb2D = GetComponent<Rigidbody2D>();
            Damageable damageable = GetComponent<DamageableComponent>().Damageable;

            _wolf = new Wolf(
                transform,
                mover,
                rb2D,
                field,
                damageable,
                _borderAvoidingStartDistance,
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

            StartCoroutine(UpdateRoutine());
        }

        private IEnumerator UpdateRoutine()
        {
            while (true)
            {
                _wolf.Update();
                yield return new WaitForSeconds(1 / 15f);
            }
        }
    }
}
