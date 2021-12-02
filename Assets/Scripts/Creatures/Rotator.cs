using UnityEngine;

namespace Hunter.Creatures
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
            // TODO : maybe delete that shit?
            direction.Normalize();
            
            Transform.up = direction;
        }
    }
}
