using Hunter.Creatures.Common;
using Hunter.Creatures.Shooters;
using UnityEngine;

namespace Hunter.Creatures.Player
{
    [RequireComponent(typeof(MoverComponent))]
    [RequireComponent(typeof(ShooterComponent))]
    public class PlayerSoundsComponent : MonoBehaviour
    {
        [Header("Audiosources")]
        [SerializeField] private AudioSource _gunAudioSource;
        [SerializeField] private AudioSource _movementAudioSource;

        [Header("Sounds")]
        [SerializeField] private AudioClip _reloadSound;
        [SerializeField] private AudioClip _shotSound;
        [SerializeField] private AudioClip[] _stepSounds;

        [Header("Settings")]
        [SerializeField] private float _stepSoundDelay;

        public PlayerSounds PlayerSounds { get; private set; }

        private void Awake()
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            Shooter shooter = GetComponent<ShooterComponent>().Shooter;

            PlayerSounds = new PlayerSounds(_gunAudioSource, _movementAudioSource, _reloadSound, _shotSound, _stepSounds,
                mover, shooter, this, _stepSoundDelay);
        }
    }
}
