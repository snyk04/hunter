using UnityEngine;

namespace Hunter.Creatures
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageableComponent : MonoBehaviour
    {
        [SerializeField] private int _amountOfHealth;
        
        public Damageable Damageable { get; private set; }

        private void Awake()
        {
            Initialize(_amountOfHealth);
        }

        public void Initialize(int amountOfHealth)
        {
            Damageable = new Damageable(amountOfHealth, gameObject);
        }
    }
}
