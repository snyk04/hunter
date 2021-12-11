using Hunter.Creatures.Common;
using Hunter.Creatures.Shooters;
using UnityEngine;

namespace Hunter.Objects
{
    public class MapBorder
    {
        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DamageableComponent damageableComponent))
            {
                damageableComponent.Damageable.Destroy();
            }

            if (other.TryGetComponent(out BulletComponent bulletComponent))
            {
                Object.Destroy(bulletComponent.gameObject);
            }
        }
    }
}
