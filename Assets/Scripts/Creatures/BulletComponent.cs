using UnityEngine;

namespace Hunter.Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BulletComponent : MonoBehaviour
    {
        private Bullet _bullet;
        
        public void Initialize(int damage, string shooterName)
        {
            _bullet = new Bullet(damage, gameObject, shooterName);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _bullet.OnTriggerEnter2D(other);
        }
    }
}
