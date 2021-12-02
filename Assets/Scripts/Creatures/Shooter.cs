using System;
using System.Collections;
using Hunter.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hunter.Creatures
{
    public class Shooter
    {
        public const string BulletName = "Bullet";

        private readonly GameObject _bulletPrefab;
        private readonly float _bulletSpeed;
        private readonly float _reloadTime;
        private readonly float _shotDelay;
        private readonly int _maxAmountOfBulletsInBackpack;
        private readonly int _maxAmountOfBulletsInMagazine;
        private readonly Transform _transform;

        private DateTime _lastShotTime;
        private bool _isReloading;
        
        public int AmountOfBulletsInBackpack { get; private set; }
        public int AmountOfBulletsInMagazine { get; private set; }
        public event Action OnShot;
        public event Action OnReload;

        public Shooter(GameObject bulletPrefab, float bulletSpeed, float reloadTime, float shotDelay,
            int maxAmountOfBulletsInBackpack, int maxAmountOfBulletsInMagazine, Transform transform)
        {
            _bulletPrefab = bulletPrefab;
            _bulletSpeed = bulletSpeed;
            _reloadTime = reloadTime;
            _shotDelay = shotDelay;
            _maxAmountOfBulletsInBackpack = maxAmountOfBulletsInBackpack;
            _maxAmountOfBulletsInMagazine = maxAmountOfBulletsInMagazine;
            _transform = transform;

            _isReloading = false;
            
            AmountOfBulletsInBackpack = maxAmountOfBulletsInBackpack;
            AmountOfBulletsInMagazine = maxAmountOfBulletsInMagazine;
        }

        public void Shoot()
        {
            if (!CanShoot())
            {
                return;
            }

            GameObject bullet = Object.Instantiate(_bulletPrefab, _transform.position, Quaternion.identity);
            bullet.name = BulletName;
            bullet.GetComponent<Rigidbody2D>().velocity = _transform.up * _bulletSpeed;
            
            _lastShotTime = DateTime.Now;
            AmountOfBulletsInMagazine--;
            OnShot?.Invoke();
        }
        private bool CanShoot()
        {
            if (_lastShotTime.GetPassedSeconds() < _shotDelay)
            {
                return false;
            }
            
            if (_isReloading)
            {
                return false;
            }
            
            if (AmountOfBulletsInMagazine < 1)
            {
                StartReloading();
                return false;
            }

            return true;
        }

        public void StartReloading()
        {
            if (_isReloading)
            {
                return;
            }
            
            if (AmountOfBulletsInBackpack < 1)
            {
                return;
            }

            // TODO : think about it
            _transform.GetComponent<MonoBehaviour>().StartCoroutine(ReloadingRoutine());
            _isReloading = true;
        }

        private IEnumerator ReloadingRoutine()
        {
            yield return new WaitForSeconds(_reloadTime);
            Reload();
        }
        private void Reload()
        {
            int bulletsToReload = _maxAmountOfBulletsInMagazine - AmountOfBulletsInMagazine;
            for (int i = 0; i < bulletsToReload; i++)
            {
                if (AmountOfBulletsInBackpack < 1)
                {
                    break;
                }

                AmountOfBulletsInBackpack -= 1;
                AmountOfBulletsInMagazine += 1;
            }

            _isReloading = false;
            OnReload?.Invoke();
        }
    }
}
