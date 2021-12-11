using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Hunter.Creatures.Common
{
    public class Damageable
    {
        public int AmountOfHealth { get; private set; }
        private readonly GameObject _gameObject;

        public event Action OnDestroy;

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
            OnDestroy?.Invoke();
            Object.Destroy(_gameObject);
        }
    }
}
