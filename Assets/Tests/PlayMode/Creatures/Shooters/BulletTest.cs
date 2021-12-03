using System.Collections;
using Hunter.Creatures.Shooters;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures.Shooters
{
    public class BulletTest
    {
        private const int AmountOfHealth = 3;
        private const int BulletDamage = 1;
        
        [UnityTest]
        public IEnumerator TestOnTriggerEnter2D()
        {
            var damageableObject = new GameObject();
            var circleCollider2D = damageableObject.AddComponent<CircleCollider2D>();
            var damageable = damageableObject.AddComponent<DamageableComponent>();
            damageable.Initialize(AmountOfHealth);
            
            var bulletObject = new GameObject();
            var bullet = new Bullet(BulletDamage, bulletObject, new GameObject());
            
            bullet.OnTriggerEnter2D(circleCollider2D);
            yield return null;
            Assert.IsTrue(bulletObject == null);
            Assert.AreEqual(AmountOfHealth - BulletDamage, damageable.Damageable.AmountOfHealth);
        }
    }
}
