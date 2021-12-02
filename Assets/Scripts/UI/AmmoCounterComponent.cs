using System;
using Hunter.Creatures;
using UnityEngine;
using UnityEngine.UI;

namespace Hunter.UI
{
    public class AmmoCounterComponent : MonoBehaviour
    {
        [SerializeField] private Text _counterText;
        [SerializeField] private ShooterComponent _shooterComponent;

        private AmmoCounter _ammoCounter;

        private void Awake()
        {
            _ammoCounter = new AmmoCounter(_counterText, _shooterComponent.Shooter);
        }
    }
}
