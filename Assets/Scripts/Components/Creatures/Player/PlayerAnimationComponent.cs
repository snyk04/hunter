using Hunter.Creatures.Common;
using Hunter.Creatures.Shooters;
using UnityEngine;

namespace Hunter.Creatures.Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MoverComponent))]
    [RequireComponent(typeof(ShooterComponent))]
    public class PlayerAnimationComponent : MonoBehaviour
    {
        [SerializeField] private AnimationClip _reloadAnimationClip;
        [SerializeField] private AnimationClip _shotAnimationClip;
        
        public PlayerAnimation PlayerAnimation { get; private set; }

        private void Awake()
        {
            var animator = GetComponent<Animator>();
            Mover mover = GetComponent<MoverComponent>().Mover;
            Shooter shooter = GetComponent<ShooterComponent>().Shooter;

            PlayerAnimation = new PlayerAnimation(animator, mover, shooter, _reloadAnimationClip, _shotAnimationClip);
        }
    }
}
