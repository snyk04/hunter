using System.Collections.Generic;
using System.Linq;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitFleeState : RabbitState
    {
        private readonly List<Transform> _pursuers;
        
        public RabbitFleeState(RabbitInfo rabbitInfo, IEnumerable<Transform> pursuers) : base(rabbitInfo)
        {
            _pursuers = pursuers.ToList();
        }

        public override void Update()
        {
            if (AllPursuersAreNull())
            {
                RabbitInfo.Animal.ChangeState(new RabbitWanderingState(RabbitInfo));
                return;
            }

            if (AllPursuersAreFarAway())
            {
                RabbitInfo.Animal.ChangeState(new RabbitWanderingState(RabbitInfo));
                return;
            }

            TryToAddNewPursuers();
            
            CurrentVelocity = RabbitInfo.Rigidbody2D.velocity;
            CurrentVelocity += ComputeFleeVelocity();

            AvoidBorders();
            Move();
        }

        private bool AllPursuersAreNull()
        {
            for (int i = _pursuers.Count - 1; i >= 0; i--)
            {
                if (_pursuers[i] != null)
                {
                    return false;
                }

                _pursuers.RemoveAt(i);
            }
            
            return true;
        }
        private bool AllPursuersAreFarAway()
        {
            for (int i = _pursuers.Count - 1; i >= 0; i--)
            {
                Vector2 pursuerToRabbitVector = RabbitInfo.Position - _pursuers[i].Position();
                if (pursuerToRabbitVector.magnitude > RabbitInfo.FleeStopDistance)
                {
                    _pursuers.RemoveAt(i);
                }
            }

            return !_pursuers.Any();
        }

        private void TryToAddNewPursuers()
        {
            if (PursuersNearby(out Transform[] newPursuers))
            {
                foreach (Transform newPursuer in newPursuers)
                {
                    if (!_pursuers.Contains(newPursuer))
                    {
                        _pursuers.Add(newPursuer);
                    }
                }
            }
        }

        private Vector2 ComputeFleeVelocity()
        {
            // TODO : refactor
            Vector2 desiredVelocity = Vector2.zero;
            foreach (Transform pursuer in _pursuers)
            {
                Vector2 pursuerToRabbitVector = RabbitInfo.Position - pursuer.Position();
                float coefficient = -pursuerToRabbitVector.magnitude / RabbitInfo.FleeStopDistance + 1;
                desiredVelocity += pursuerToRabbitVector.normalized * coefficient;
            }

            // TODO : to const
            return (desiredVelocity - CurrentVelocity).normalized * RabbitInfo.FleeSpeed * 0.25f;
        }
        private void Move()
        {
            RabbitInfo.Mover.Move(CurrentVelocity.normalized, RabbitInfo.FleeSpeed);
        }
    }
}
