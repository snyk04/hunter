using UnityEngine;

namespace Hunter.Creatures
{
    public class DamageableComponent : MonoBehaviour
    {
        [SerializeField] private int _amountOfHealth;
        
        public Damageable Damageable { get; private set; }

        private void Awake()
        {
            Damageable = new Damageable(_amountOfHealth, this);
        }
    }
}