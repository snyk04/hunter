using System.Collections;
using Hunter.Creatures;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures
{
    public class ShooterTest
    {
        private const float BulletSpeed = 5;

        [UnityTest]
        public IEnumerator TestShoot()
        {
            var bulletPrefab = new GameObject();
            var rigidbody = bulletPrefab.AddComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;
            
            Transform transform = new GameObject().transform;
            
            var shooter = new Shooter(bulletPrefab, BulletSpeed, transform);
            shooter.Shoot();

            yield return null;

            Vector2 expected = transform.up * BulletSpeed;
            Vector2 actual = GameObject.Find(Shooter.BulletName).GetComponent<Rigidbody2D>().velocity;
            
            Assert.AreEqual(expected, actual);
        }
    }
}
