using System.Collections;
using Hunter.Creatures.Common;
using Hunter.Creatures.Shooters;
using UnityEngine;

namespace Hunter.Creatures.Player
{
    public class PlayerSounds
    {
        private readonly AudioSource _gunAudioSource;
        private readonly AudioSource _movementAudioSource;

        private readonly AudioClip _reloadSound;
        private readonly AudioClip _shotSound;
        private readonly AudioClip[] _stepSounds;
        
        private readonly Mover _playerMover;
        private readonly Shooter _playerShooter;
        private readonly MonoBehaviour _coroutineManager;

        private Coroutine _movementSoundCoroutine;

        private readonly float _stepSoundDelay;

        public PlayerSounds(AudioSource gunAudioSource, AudioSource movementAudioSource, AudioClip reloadSound,
            AudioClip shotSound, AudioClip[] stepSounds, Mover playerMover, Shooter playerShooter, MonoBehaviour coroutineManager, float stepSoundDelay)
        {
            _gunAudioSource = gunAudioSource;
            _movementAudioSource = movementAudioSource;
            _reloadSound = reloadSound;
            _shotSound = shotSound;
            _stepSounds = stepSounds;
            _playerMover = playerMover;
            _playerShooter = playerShooter;
            _coroutineManager = coroutineManager;
            _stepSoundDelay = stepSoundDelay;

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _playerMover.OnMove += HandleMovementSound;
            _playerShooter.OnReloadStart += () => PlaySound(_gunAudioSource, _reloadSound);
            _playerShooter.OnShot += () => PlaySound(_gunAudioSource, _shotSound);
        }

        private void HandleMovementSound(Vector2 direction, float speed)
        {
            if (direction.magnitude > 0)
            {
                _movementSoundCoroutine ??= _coroutineManager.StartCoroutine(MovementSoundRoutine());
                return;
            }

            if (_movementSoundCoroutine != null)
            {
                _coroutineManager.StopCoroutine(_movementSoundCoroutine);
                _movementSoundCoroutine = null;
            }
        }
        private IEnumerator MovementSoundRoutine()
        {
            while (true)
            {
                PlaySound(_movementAudioSource, _stepSounds[Random.Range(0, _stepSounds.Length - 1)]);
                yield return new WaitForSeconds(_stepSoundDelay);
            }
        }
        
        private void PlaySound(AudioSource audioSource, AudioClip sound)
        {
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}
