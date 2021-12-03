using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI
{
    [RequireComponent(typeof(MoverComponent))]
    public class RabbitComponent : MonoBehaviour
    {
        [SerializeField] private float _wanderingRadius;
        [SerializeField] private Transform _pursuer;
        
        public Rabbit Rabbit { get; private set; }
        
        private Mover _mover;

        private Vector3 _startPosition; 

        private void Awake()
        {
            _mover = GetComponent<MoverComponent>().Mover;
            
            Rabbit = new Rabbit(_wanderingRadius, transform, _mover);

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
            Rabbit.ChangeState(new FleeState(_pursuer, Rabbit, transform, _mover));
        }
    }
}
