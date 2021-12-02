using UnityEngine;

namespace Hunter.Creatures
{
    public class Rotator
    {
        private readonly Transform _transform;

        public Rotator(Transform transform)
        {
            _transform = transform;
        }
        
        public void LookAt(Vector2 direction)
        {
            // TODO : maybe delete that shit?
            direction.Normalize();
            
            _transform.up = direction;
        }
    }
}
