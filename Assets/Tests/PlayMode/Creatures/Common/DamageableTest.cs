using System.Collections;
using Hunter.Creatures.Common;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures.Common
{
    public class DamageableTest
    {
        private const int AmountOfHealth = 3;
        private const int AmountOfDamage = 1;    
        private const int AmountOfDeathDamage = 3;

        private void CreatDamageable(out Damageable damageable, out GameObject gameObject)
        {
            gameObject = new GameObject();
            damageable = new Damageable(AmountOfHealth, gameObject);
        }
        
        [Test]
        public void TestGetDamaged()
        {
            CreatDamageable(out Damageable damageable, out GameObject gameObject);
            
            damageable.GetDamaged(AmountOfDamage);
            Assert.AreEqual(AmountOfHealth - AmountOfDamage, damageable.AmountOfHealth);
        }
        
        [UnityTest]
        public IEnumerator TestDestroy()
        {
            CreatDamageable(out Damageable damageable, out GameObject gameObject);
            
            damageable.GetDamaged(AmountOfDeathDamage);
            yield return null;
            Assert.IsTrue(gameObject == null);
        }
    }
}
