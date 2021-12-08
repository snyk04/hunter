using UnityEngine;

namespace Hunter.Creatures.Common
{
    public class Damageable
    {
        public int AmountOfHealth { get; private set; }
        private readonly GameObject _gameObject;

        public Damageable(int amountOfHealth, GameObject gameObject)
        {
            AmountOfHealth = amountOfHealth;
            _gameObject = gameObject;
        }

        public void GetDamaged(int amountOfDamage)
        {
            AmountOfHealth -= amountOfDamage;

            if (AmountOfHealth < 1)
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            Object.Destroy(_gameObject);
        }
    }
}