using System;
using UnityEngine;
using Random = System.Random;

namespace Hunter.Creatures.Common
{
    public class Animatable : IDisposable
    {
        private const float ZeroSpeedThreshold = 0.01f;
        
        private const string MoveTopName = "MoveTop";
        private const string MoveRightName = "MoveRight";
        private const string MoveBotName = "MoveBot";
        private const string MoveLeftName = "MoveLeft";
        
        private readonly Animator _animator;
        private readonly Mover _mover;

        private readonly Random _random;
        private Vector2 _lastDirection;
        private float _lastSpeed;

        public Animatable(Animator animator, Mover mover)
        {
            _animator = animator;
            _mover = mover;

            _random = new Random();
            _lastDirection = default;
            _lastSpeed = default;

            _mover.OnMove += Animate;
        }
        
        private void Animate(Vector2 direction, float speed)
        {
            bool moveTop = false;
            bool moveRight = false;
            bool moveBot = false;
            bool moveLeft = false;
            
            if (direction == _lastDirection && Math.Abs(speed - _lastSpeed) < 0.01)
            {
                return;
            }
            _lastDirection = direction;
            _lastSpeed = speed;
            
            _animator.speed = Math.Abs(speed) < ZeroSpeedThreshold ? 1 : speed;
            
            if (Math.Abs(direction.x) > Math.Abs(direction.y))
            {
                moveRight = direction.x > 0;
                moveLeft = direction.x < 0;
            }
            else if (Math.Abs(direction.x) < Math.Abs(direction.y))
            {
                moveTop = direction.y > 0;
                moveBot = direction.y < 0;
            }
            else if (direction.x == direction.y && direction.x != 0 && direction.y != 0)
            {
                double value = _random.NextDouble();
                if (value > 0.5)
                {
                    moveRight = direction.x > 0;
                    moveLeft = direction.x < 0;
                }
                else
                {
                    moveTop = direction.y > 0;
                    moveBot = direction.y < 0;
                }
            }
            
            _animator.SetBool(MoveTopName, moveTop);
            _animator.SetBool(MoveRightName, moveRight);
            _animator.SetBool(MoveBotName, moveBot);
            _animator.SetBool(MoveLeftName, moveLeft);
        }

        public void Dispose()
        {
            _mover.OnMove -= Animate;
        }
    }
}
