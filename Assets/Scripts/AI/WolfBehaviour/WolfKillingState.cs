using System;
using Hunter.AI.Common;
using Hunter.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.WolfBehaviour
{
    public class WolfKillingState : WolfState
    {
        private readonly Transform _target;
        private readonly Damageable _targetDamageable;

        private DateTime _lastAttackTime;

        public WolfKillingState(AnimalInfo animaInfo, Transform target) : base(animaInfo)
        {
            _target = target;
            _targetDamageable = _target.GetComponent<DamageableComponent>().Damageable;
        }

        public override void Update()
        {
            if (_target == null)
            {
                ChangeAnimalState(new WolfWanderingState(AnimalInfo));
                return;
            }
            
            Vector2 wolfToTargetVector = _target.Position() - AnimalInfo.Position;
            if (wolfToTargetVector.magnitude >= AnimalInfo.KillingStopDistance)
            {
                ChangeAnimalState(new WolfSeekState(AnimalInfo, _target));
                return;
            }

            if (_lastAttackTime.GetPassedSeconds() >= AnimalInfo.KillingAttackDelay)
            {
                _targetDamageable.GetDamaged(AnimalInfo.KillingAmountOfDamage);
                _lastAttackTime = DateTime.Now;
            }
        }
    }
}
