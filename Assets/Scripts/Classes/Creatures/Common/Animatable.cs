using System;
using UnityEngine;
using Random = System.Random;

namespace Hunter.Creatures.Common
{
    public class Animatable
    {
        private const string MoveTopName = "MoveTop";
        private const string MoveRightName = "MoveRight";
        private const string MoveBotName = "MoveBot";
        private const string MoveLeftName = "MoveLeft";
        
        private readonly Animator _animator;
        private readonly Rigidbody2D _rigidbody2D;

        private readonly Random _random;
        private Vector2 _lastVelocity;

        public Animatable(Animator animator, Rigidbody2D rigidbody2D)
        {
            _animator = animator;
            _rigidbody2D = rigidbody2D;

            _random = new Random();
        }

        public void Update()
        {
            Animate();
        }

        private void Animate()
        {
            Vector2 currentVelocity = _rigidbody2D.velocity;
            bool moveTop = false;
            bool moveRight = false;
            bool moveBot = false;
            bool moveLeft = false;
            
            if (currentVelocity == _lastVelocity)
            {
                return;
            }
            _lastVelocity = currentVelocity;

            float moverSpeed = Math.Max(currentVelocity.x / currentVelocity.normalized.x,
                currentVelocity.y / currentVelocity.normalized.y);
            _animator.speed = !float.IsNaN(moverSpeed) || moverSpeed != 0 
                ? 1 
                : moverSpeed;
            
            if (Math.Abs(currentVelocity.x) > Math.Abs(currentVelocity.y))
            {
                if (currentVelocity.x > 0)
                {
                    moveRight = true;
                }
                else
                {
                    moveLeft = true;
                }
            }
            else if (Math.Abs(currentVelocity.x) < Math.Abs(currentVelocity.y))
            {
                if (currentVelocity.y > 0)
                {
                    moveTop = true;
                }
                else
                {
                    moveBot = true;
                }
            }
            else if (Math.Abs(currentVelocity.x - currentVelocity.y) < 0.01 && currentVelocity.x != 0 && currentVelocity.y != 0)
            {
                double value = _random.NextDouble();
                if (value > 0.5)
                {
                    if (currentVelocity.x > 0)
                    {
                        moveRight = true;
                    }
                    else
                    {
                        moveLeft = true;
                    }
                }
                else
                {
                    if (currentVelocity.y > 0)
                    {
                        moveTop = true;
                    }
                    else
                    {
                        moveBot = true;
                    }
                }
            }
            
            _animator.SetBool(MoveTopName, moveTop);
            _animator.SetBool(MoveRightName, moveRight);
            _animator.SetBool(MoveBotName, moveBot);
            _animator.SetBool(MoveLeftName, moveLeft);
        }
    }
}
