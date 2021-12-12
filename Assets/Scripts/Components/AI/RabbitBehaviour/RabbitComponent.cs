using System.Collections;
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

        [Header("Other")] 
        [SerializeField] private float _borderAvoidingStartDistance; 

        private Rabbit _rabbit;

        public void Initialize(Field field)
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            var rb2D = GetComponent<Rigidbody2D>();

            _rabbit = new Rabbit(
                transform, 
                mover, 
                rb2D, 
                field,
                _borderAvoidingStartDistance,
                _wanderingSpeed,
                _fleeSpeed, 
                _fleeStartDistance, 
                _fleeStopDistance
            );
            
            StartCoroutine(UpdateRoutine());
        }
        
        private IEnumerator UpdateRoutine()
        {
            while (true)
            {
                _rabbit.Update();
                yield return new WaitForSeconds(1 / 15f);
            }
        }
    }
}
