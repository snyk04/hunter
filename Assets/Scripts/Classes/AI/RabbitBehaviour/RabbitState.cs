﻿using System.Collections.Generic;
using System.Linq;
using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.RabbitBehaviour
{
    public abstract class RabbitState : State
    {
        protected RabbitInfo RabbitInfo { get; }
        protected Vector2 CurrentVelocity;
        
        private readonly Collider2D[] _nearbyObjects;

        protected RabbitState(RabbitInfo rabbitInfo)
        {
            RabbitInfo = rabbitInfo;
            
            _nearbyObjects = new Collider2D[2];
        }
        
        protected bool PursuersNearby(out Transform[] pursuers)
        {
            var pursuersList = new List<Transform>();
            
            Physics2D.OverlapCircleNonAlloc(RabbitInfo.Position, RabbitInfo.FleeStartDistance, _nearbyObjects);
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == RabbitInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent _))
                {
                    continue;
                }

                pursuersList.Add(nearbyObject.transform);
            }

            pursuers = pursuersList.ToArray();
            return pursuersList.Any();
        }
        private Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return RabbitInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
        
        protected void AvoidBorders()
        {
            // Vector2 desiredVelocity = Vector2.zero;
            // if (RabbitInfo.Position.x - RabbitInfo.BorderAvoidingStartDistance <= RabbitInfo.Field.XLeftBorder)
            // {
            //     desiredVelocity += Vector2.right;
            // }
            // if (RabbitInfo.Position.x + RabbitInfo.BorderAvoidingStartDistance >= RabbitInfo.Field.XRightBorder)
            // {
            //     desiredVelocity += Vector2.left;
            // }
            // if (RabbitInfo.Position.y - RabbitInfo.BorderAvoidingStartDistance <= RabbitInfo.Field.YBotBorder)
            // {
            //     desiredVelocity += Vector2.up;
            // }
            // if (RabbitInfo.Position.y + RabbitInfo.BorderAvoidingStartDistance >= RabbitInfo.Field.YTopBorder)
            // {
            //     desiredVelocity += Vector2.down;
            // }
            //
            // if (desiredVelocity != Vector2.zero)
            // {
            //     CurrentVelocity += (desiredVelocity - CurrentVelocity) / 5;
            //     Debug.Log($"{CurrentVelocity}");
            // }
            
            while (!RabbitInfo.Field.Contains(PredictPosition(CurrentVelocity.normalized, RabbitInfo.BorderAvoidingStartDistance)))
            {
                CurrentVelocity = Quaternion.Euler(0, 0, 15) * CurrentVelocity;
            }
        }
    }
}
