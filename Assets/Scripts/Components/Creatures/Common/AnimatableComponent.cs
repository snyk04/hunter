using UnityEngine;

namespace Hunter.Creatures.Common
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(MoverComponent))]
    public class AnimatableComponent : MonoBehaviour
    {
        private Animatable _animatable;

        private void Awake()
        {
            var animator = GetComponent<Animator>();
            Mover mover = GetComponent<MoverComponent>().Mover;
            _animatable = new Animatable(animator, mover);
        }
    }
}
