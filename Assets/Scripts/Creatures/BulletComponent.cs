using UnityEngine;

namespace Hunter.Creatures
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class BulletComponent : MonoBehaviour
    {
        private Bullet _bullet;
        
        public void Initialize(int damage, GameObject shooterObject)
        {
            _bullet = new Bullet(damage, gameObject, shooterObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _bullet.OnTriggerEnter2D(other);
        }
    }
}
