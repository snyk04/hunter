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

        public WolfKillingState(WolfInfo wolfInfo, Transform target) : base(wolfInfo)
        {
            _target = target;
            _targetDamageable = _target.GetComponent<DamageableComponent>().Damageable;
        }

        public override void Update()
        {
            base.Update();

            if (_target == null)
            {
                WolfInfo.Animal.ChangeState(new WolfWanderingState(WolfInfo));
                LastMealTime = DateTime.Now;
                return;
            }
            
            Vector2 wolfToTargetVector = _target.Position() - WolfInfo.Position;
            if (wolfToTargetVector.magnitude >= WolfInfo.KillingStopDistance)
            {
                WolfInfo.Animal.ChangeState(new WolfSeekState(WolfInfo, _target));
                return;
            }

            if (_lastAttackTime.GetPassedSeconds() >= WolfInfo.KillingAttackDelay)
            {
                _targetDamageable.GetDamaged(WolfInfo.KillingAmountOfDamage);
                _lastAttackTime = DateTime.Now;
            }
        }
    }
}
