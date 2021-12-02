﻿using System.Collections;
using Hunter.Creatures;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Creatures
{
    public class ShooterTest
    {
        private const float BulletSpeed = 5;
        private const float ReloadTime = 2;
        private const float ShotDelay = 0.5f;
        private const int MaxAmountOfBulletsInBackpack = 25;
        private const int MaxAmountOfBulletsInMagazine = 5;

        private void CreateShooter(out Shooter shooter, out Transform transform)
        {
            var bulletPrefab = new GameObject();
            var rigidbody = bulletPrefab.AddComponent<Rigidbody2D>();
            rigidbody.isKinematic = true;

            var shooterObject = new GameObject();
            transform = shooterObject.transform;
            shooterObject.AddComponent<ShooterComponent>();

            shooter = new Shooter(bulletPrefab, BulletSpeed, ReloadTime, ShotDelay, MaxAmountOfBulletsInBackpack,
                MaxAmountOfBulletsInMagazine, transform);
        }
        
        [UnityTest]
        public IEnumerator TestShoot()
        {
            CreateShooter(out Shooter shooter, out Transform transform);
            
            shooter.Shoot();

            yield return null;

            Vector2 expected = transform.up * BulletSpeed;
            Vector2 actual = GameObject.Find(Shooter.BulletName).GetComponent<Rigidbody2D>().velocity;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(MaxAmountOfBulletsInMagazine - 1, shooter.AmountOfBulletsInMagazine);
        }
        [UnityTest]
        public IEnumerator TestReload()
        {
            CreateShooter(out Shooter shooter, out Transform transform);
            
            shooter.Shoot();
            yield return null;
            shooter.StartReloading();
            yield return new WaitForSeconds(ReloadTime);
            yield return null;
            Assert.AreEqual(MaxAmountOfBulletsInMagazine, shooter.AmountOfBulletsInMagazine);
            Assert.AreEqual(MaxAmountOfBulletsInBackpack - 1, shooter.AmountOfBulletsInBackpack);
        }
        [UnityTest]
        public IEnumerator TestAutoReloadAfterAllBulletsAreShot()
        {
            CreateShooter(out Shooter shooter, out Transform transform);

            for (int i = 0; i < MaxAmountOfBulletsInMagazine + 1; i++)
            {
                yield return new WaitForSeconds(ShotDelay);
                yield return null;
                shooter.Shoot();
            }
            
            yield return new WaitForSeconds(ReloadTime);
            yield return null;
            Assert.AreEqual(MaxAmountOfBulletsInMagazine, shooter.AmountOfBulletsInMagazine);
            Assert.AreEqual(MaxAmountOfBulletsInBackpack - MaxAmountOfBulletsInMagazine
                , shooter.AmountOfBulletsInBackpack);
        }
        [UnityTest]
        public IEnumerator TestManualReloadAfterAllBulletsAreShot()
        {
            CreateShooter(out Shooter shooter, out Transform transform);

            for (int i = 0; i < MaxAmountOfBulletsInMagazine; i++)
            {
                shooter.Shoot();
                yield return new WaitForSeconds(ShotDelay);
                yield return null;
            }
            
            shooter.StartReloading();
            yield return new WaitForSeconds(ReloadTime);
            yield return null;
            Assert.AreEqual(MaxAmountOfBulletsInMagazine, shooter.AmountOfBulletsInMagazine);
            Assert.AreEqual(MaxAmountOfBulletsInBackpack - MaxAmountOfBulletsInMagazine
                , shooter.AmountOfBulletsInBackpack);
        }
    }
}
