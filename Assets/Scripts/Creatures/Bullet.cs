using UnityEngine;

namespace Hunter.Creatures
{
    public class Bullet
    {
        private readonly int _damage;
        private readonly GameObject _gameObject;
        private readonly GameObject _shooterObject; 

        public Bullet(int damage, GameObject gameObject, GameObject shooterObject)
        {
            _damage = damage;
            _gameObject = gameObject;
            _shooterObject = shooterObject;
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == _shooterObject)
            {
                return;
            }
            
            if (other.TryGetComponent(out DamageableComponent damageableComponent))
            {
                damageableComponent.Damageable.GetDamaged(_damage);
            }
            
            Object.Destroy(_gameObject);
        }
    }
}
