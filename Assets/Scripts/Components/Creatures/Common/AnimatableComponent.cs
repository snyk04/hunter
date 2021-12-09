using UnityEngine;

namespace Hunter.Creatures.Common
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class AnimatableComponent : MonoBehaviour
    {
        private Animatable _animatable;

        private void Awake()
        {
            var animator = GetComponent<Animator>();
            var rb = GetComponent<Rigidbody2D>();
            _animatable = new Animatable(animator, rb);
        }
        private void Update()
        {
            _animatable.Update();
        }
    }
}
