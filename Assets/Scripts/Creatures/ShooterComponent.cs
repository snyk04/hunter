using UnityEngine;

namespace Hunter.Creatures
{
    public class ShooterComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletSpeed;
        
        public Shooter Shooter { get; private set; }

        private void Awake()
        {
            Shooter = new Shooter(_bulletPrefab, _bulletSpeed, transform);
        }
    }
}
