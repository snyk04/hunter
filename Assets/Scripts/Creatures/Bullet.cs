using UnityEngine;

namespace Hunter.Creatures
{
    public class Bullet
    {
        private readonly int _damage;
        private readonly GameObject _gameObject;
        private readonly string _shooterName; 

        public Bullet(int damage, GameObject gameObject, string shooterName)
        {
            _damage = damage;
            _gameObject = gameObject;
            _shooterName = shooterName;
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.name == _shooterName)
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
