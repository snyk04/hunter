using UnityEngine;

namespace Hunter.Creatures.Common
{
    public class Rotator
    {
        public readonly Transform Transform;

        public Rotator(Transform transform)
        {
            Transform = transform;
        }
        
        public void LookAt(Vector2 direction)
        {
            Transform.up = direction.normalized;
        }
    }
}
