using UnityEngine;

namespace Hunter.Creatures
{
    public class Shooter
    {
        public const string BulletName = "Bullet";
        
        private readonly GameObject _bulletPrefab;
        private readonly float _bulletSpeed;
        private readonly Transform _transform;

        public Shooter(GameObject bulletPrefab, float bulletSpeed, Transform transform)
        {
            _bulletPrefab = bulletPrefab;
            _bulletSpeed = bulletSpeed;
            _transform = transform;
        }

        public void Shoot()
        {
            GameObject bullet = Object.Instantiate(_bulletPrefab, _transform.position, Quaternion.identity);
            bullet.name = BulletName;
            
            bullet.GetComponent<Rigidbody2D>().velocity = _transform.up * _bulletSpeed;
        }
    }
}
