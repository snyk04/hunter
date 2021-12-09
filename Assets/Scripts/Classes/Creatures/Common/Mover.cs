using System;
using UnityEngine;

namespace Hunter.Creatures.Common
{
    public class Mover
    {
        private readonly Rigidbody2D _rigidbody;

        public event Action<Vector2, float> OnMove;

        public Mover(Rigidbody2D rigidbody)
        {
            _rigidbody = rigidbody;
        }
        
        public void Move(Vector2 direction, float speed)
        {
            OnMove?.Invoke(direction, speed);
            _rigidbody.velocity = direction * speed;
        }
        public void Stop()
        {
            Move(Vector2.zero, 0);
        }
    }
}
