using UnityEngine;

namespace Hunter.Creatures.Common
{
    public class Mover
    {
        private readonly float _speed;
        private readonly Rigidbody2D _rigidbody;

        public Mover(float speed, Rigidbody2D rigidbody)
        {
            _speed = speed;
            _rigidbody = rigidbody;
        }
        
        public void Move(Vector2 direction)
        {
            _rigidbody.velocity = direction * _speed;
        }
        public void Stop()
        {
            Move(Vector2.zero);
        }
    }
}
