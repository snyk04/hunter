using System.Collections.Generic;
using System.Linq;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerFleeState : DeerState
    {
        private readonly List<Transform> _pursuers;
        
        public DeerFleeState(DeerInfo deerInfo, IEnumerable<Transform> pursuers) : base(deerInfo)
        {
            _pursuers = pursuers.ToList();
        }

        public override void Update()
        {
            if (AllPursuersAreNull() || AllPursuersAreFarAway())
            {
                DeerInfo.Animal.ChangeState(new DeerWanderingState(DeerInfo));
                return;
            }

            TryToAddNewPursuers();
            
            CurrentVelocity = DeerInfo.Rigidbody2D.velocity.normalized;
            CurrentVelocity += ComputeFleeVelocity();
            CurrentVelocity += ComputeDeerGroupVelocity();
            
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
                Vector2 pursuerToRabbitVector = DeerInfo.Position - _pursuers[i].Position();
                if (pursuerToRabbitVector.magnitude > DeerInfo.FleeStopDistance)
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
                Vector2 pursuerToRabbitVector = DeerInfo.Position - pursuer.Position();
                float coefficient = -pursuerToRabbitVector.magnitude / DeerInfo.FleeStopDistance + 1;
                desiredVelocity += pursuerToRabbitVector.normalized * coefficient;
            }

            return desiredVelocity.normalized - CurrentVelocity;
        }
        private void Move()
        {
            DeerInfo.Mover.Move(CurrentVelocity.normalized, DeerInfo.FleeSpeed);
        }
    }
}
