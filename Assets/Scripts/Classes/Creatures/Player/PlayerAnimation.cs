using System.Collections;
using Hunter.Creatures.Common;
using Hunter.Creatures.Shooters;
using UnityEngine;

namespace Hunter.Creatures.Player
{
    public class PlayerAnimation
    {
        private const string IsShootingName = "IsShooting";
        private const string IsReloadingName = "IsReloading";
        private const string IsMovingName = "IsMoving";
        
        private readonly Animator _animator;
        private readonly Mover _playerMover;
        private readonly Shooter _playerShooter;
        private readonly MonoBehaviour _coroutineStarter;

        private readonly AnimationClip _reloadAnimationClip;
        private readonly AnimationClip _shotAnimationClip;

        public PlayerAnimation(Animator animator, Mover playerMover, Shooter playerShooter, 
            AnimationClip reloadAnimationClip, AnimationClip shotAnimationClip)
        {
            _animator = animator;
            _playerMover = playerMover;
            _playerShooter = playerShooter;
            _coroutineStarter = animator.GetComponent<MonoBehaviour>();

            _reloadAnimationClip = reloadAnimationClip;
            _shotAnimationClip = shotAnimationClip;
            
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _playerShooter.OnShot += AnimateShot;
            _playerShooter.OnReloadStart += AnimateReload;
            _playerMover.OnMove += AnimateMovement;
        }

        private void AnimateShot()
        {
            _coroutineStarter.StartCoroutine(AnimatingRoutine(
                IsShootingName,
                1 / (_playerShooter.ShotDelay / _shotAnimationClip.length),
                _playerShooter.ShotDelay)
            );
        }
        private void AnimateReload()
        {
            _coroutineStarter.StartCoroutine(AnimatingRoutine(
                IsReloadingName,
                1 / (_playerShooter.ReloadTime / _reloadAnimationClip.length),
                _playerShooter.ReloadTime)
            );        
        }
        private void AnimateMovement(Vector2 direction, float speed)
        {
            bool isMoving = direction.magnitude > 0;
            speed = isMoving ? speed : 1;
            
            _animator.SetBool(IsMovingName, isMoving);
            _animator.speed = speed;
        }

        private IEnumerator AnimatingRoutine(string propertyName, float animatorSpeed, float animationLength)
        {
            _animator.SetBool(propertyName, true);
            _animator.speed = animatorSpeed;
            yield return new WaitForSeconds(animationLength);
            _animator.speed = 1;
            _animator.SetBool(propertyName, false);
        }
    }
}
