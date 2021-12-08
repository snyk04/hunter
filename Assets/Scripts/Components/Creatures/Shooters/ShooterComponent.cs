using UnityEngine;

namespace Hunter.Creatures.Shooters
{
    public class ShooterComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private int _bulletDamage;
        [SerializeField] private float _reloadTime;
        [SerializeField] private float _shotDelay;
        [SerializeField] private int _maxAmountOfBulletsInBackpack;
        [SerializeField] private int _maxAmountOfBulletsInMagazine;

        public Shooter Shooter { get; private set; }

        private void Awake()
        {
            Shooter = new Shooter(_bulletPrefab, _bulletSpeed, _bulletDamage, _reloadTime, _shotDelay,
                _maxAmountOfBulletsInBackpack, _maxAmountOfBulletsInMagazine, transform);
        }
    }
}
