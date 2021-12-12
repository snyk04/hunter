using System.Collections.Generic;
using System.Linq;
using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public class DeerFleeState : DeerState
    {
        private readonly List<Transform> _pursuers;

        private Vector2 _currentVelocity;
        
        public DeerFleeState(AnimalInfo animalInfo, IEnumerable<Transform> pursuers) : base(animalInfo)
        {
            _pursuers = pursuers.ToList();
        }

        public override void Update()
        {
            if (AllPursuersAreNull() || AllPursuersAreFarAway())
            {
                ChangeAnimalState(new DeerWanderingState(AnimalInfo));
                return;
            }

            TryToAddNewPursuers();
            
            // TODO : rigidbody in Mover or AnimalInfo
            _currentVelocity = AnimalInfo.Transform.GetComponent<Rigidbody2D>().velocity.normalized;

            _currentVelocity += ComputeFleeVelocity();
            _currentVelocity += ComputeDeerGroupVelocity();
            
            AvoidWalls();
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
                Vector2 pursuerToRabbitVector = AnimalInfo.Position - _pursuers[i].Position();
                if (pursuerToRabbitVector.magnitude > AnimalInfo.FleeStopDistance)
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
                Vector2 pursuerToRabbitVector = AnimalInfo.Position - pursuer.Position();
                float coefficient = -pursuerToRabbitVector.magnitude / AnimalInfo.FleeStopDistance + 1;
                desiredVelocity += pursuerToRabbitVector.normalized * coefficient;
            }

            return (desiredVelocity.normalized - _currentVelocity).normalized;
        }
        private Vector2 ComputeDeerGroupVelocity()
        {
            Vector2 deerGroupVelocity = Vector2.zero;
            if (DeerNearby(out Deer[] deer))
            {
                Vector2 separation = ComputeSeparation(deer);
                Vector2 alignment = ComputeAlignment(deer);
                Vector2 cohesion = ComputeCohesion(deer);

                deerGroupVelocity += separation * SeparationForce
                                     + alignment * AlignmentForce 
                                     + cohesion * CohesionForce;
            }

            return deerGroupVelocity;
        }
        
        private void AvoidWalls()
        {
            // TODO : to const
            while (!AnimalInfo.Field.Contains(PredictPosition(_currentVelocity.normalized, 5)))
            {
                _currentVelocity = Quaternion.Euler(0, 0, 15) * _currentVelocity;
            }
        }
        private void Move()
        {
            AnimalInfo.Mover.Move(_currentVelocity.normalized, AnimalInfo.FleeSpeed);
        }
    }
}
