using UnityEngine;

namespace Hunter.Creatures
{
    public class Damageable
    {
        public int AmountOfHealth { get; private set; }
        private readonly Object _object;

        public Damageable(int amountOfHealth, Object @object)
        {
            AmountOfHealth = amountOfHealth;
            _object = @object;
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
            Object.Destroy(_object);
        }
    }
}