﻿using Hunter.AI.Common;
using Hunter.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public class RabbitFleeState : RabbitState
    {
        private readonly Transform _pursuer;
        
        public RabbitFleeState(AnimalInfo animalInfo, Transform pursuer) : base(animalInfo)
        {
            _pursuer = pursuer;
        }

        public override void Update()
        {
            if (InSafety())
            {
                ChangeAnimalState(new RabbitWanderingState(AnimalInfo));
                return;
            }
            
            if (AnimalInfo.Position.x + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.XRightBorder
                || AnimalInfo.Position.x - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.XLeftBorder
                || AnimalInfo.Position.y + AnimalInfo.BorderAvoidingStartDistance >= AnimalInfo.Field.YTopBorder
                || AnimalInfo.Position.y - AnimalInfo.BorderAvoidingStartDistance <= AnimalInfo.Field.YBotBorder)
            {
                ChangeAnimalState(new RabbitAvoidBorderState(AnimalInfo, _pursuer));
                return;
            }
            
            Vector2 fleeDirection = (AnimalInfo.Position - _pursuer.Position()).normalized;
            AnimalInfo.Mover.Move(fleeDirection, AnimalInfo.FleeSpeed);
        }

        private bool InSafety()
        {
            if (_pursuer == null)
            {
                return true;
            }
            
            Vector2 fleeDirection = AnimalInfo.Position - _pursuer.Position();
            if (fleeDirection.magnitude < AnimalInfo.FleeStopDistance)
            {
                return false;
            }

            if (LiveBeingNearby(out Transform liveBeing))
            {
                // TODO : multiple Move() calls
                ChangeAnimalState(new RabbitFleeState(AnimalInfo, liveBeing));
                return false;
            }

            return true;
        }
    }
}