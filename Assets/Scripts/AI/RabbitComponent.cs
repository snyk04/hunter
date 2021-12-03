using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    [RequireComponent(typeof(MoverComponent))]
    public class RabbitComponent : MonoBehaviour
    {
        [Header("Wandering")]
        [SerializeField] private float _wanderingSpeed;
        [SerializeField] private float _wanderingRadius;
        
        [Header("Flee")]
        [SerializeField] private float _fleeSpeed;
        [SerializeField] private Transform _pursuer;

        public Rabbit Rabbit { get; private set; }
        
        private Mover _mover;

        private Vector3 _startPosition; 

        private void Awake()
        {
            _mover = GetComponent<MoverComponent>().Mover;
            
            Rabbit = new Rabbit(_wanderingSpeed, _wanderingRadius, _fleeSpeed, transform, _mover);

            _startPosition = transform.position;
        }
        private void Update()
        {
            Rabbit.Update();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_startPosition, _wanderingRadius);
        }

        public void StartAfraid()
        {
            Rabbit.ChangeState(new FleeState(_fleeSpeed, _pursuer, Rabbit, transform, _mover));
        }
    }
}
