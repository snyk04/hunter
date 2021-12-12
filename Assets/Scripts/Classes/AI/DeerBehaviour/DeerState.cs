using System.Collections.Generic;
using System.Linq;
using Hunter.AI.Common;
using Hunter.AI.RabbitBehaviour;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    public abstract class DeerState : State
    {
        protected DeerInfo DeerInfo { get; }
        protected Vector2 CurrentVelocity;
        
        private readonly Collider2D[] _nearbyObjects;

        protected DeerState(DeerInfo deerInfo)
        {
            DeerInfo = deerInfo;
            
            _nearbyObjects = new Collider2D[12];
        }
        
        protected bool PursuersNearby(out Transform[] pursuers)
        {
            var pursuersList = new List<Transform>();
            
            Physics2D.OverlapCircleNonAlloc(DeerInfo.Position, DeerInfo.FleeStartDistance, _nearbyObjects);
            foreach (Collider2D nearbyObject in _nearbyObjects)
            {
                if (nearbyObject == null 
                    || nearbyObject.gameObject == DeerInfo.Transform.gameObject 
                    || !nearbyObject.TryGetComponent(out MoverComponent mover)
                    || nearbyObject.TryGetComponent(out DeerComponent deer)
                    || nearbyObject.TryGetComponent(out RabbitComponent rabbit))
                {
                    continue;
                }

                pursuersList.Add(nearbyObject.transform);
            }
            
            pursuers = pursuersList.ToArray();
            return pursuersList.Any();
        }

        protected Vector2 ComputeDeerGroupVelocity()
        {
            Vector2 deerGroupVelocity = Vector2.zero;
            if (DeerNearby(out DeerInfo[] deerInfos))
            {
                Vector2 separation = ComputeSeparation(deerInfos);
                Vector2 alignment = ComputeAlignment(deerInfos);
                Vector2 cohesion = ComputeCohesion(deerInfos);

                deerGroupVelocity += separation * DeerInfo.SeparationForce
                                     + alignment * DeerInfo.AlignmentForce 
                                     + cohesion * DeerInfo.CohesionForce;
            }

            return deerGroupVelocity;
        }
        private bool DeerNearby(out DeerInfo[] deerInfos)
        {
            var deerInfoList = new List<DeerInfo>(DeerInfo.DeerGroup.DeerInfos);
            
            for (int i = deerInfoList.Count - 1; i >= 0; i--)
            {
                if (deerInfoList[i].Transform == null)
                {
                    deerInfoList.RemoveAt(i);
                    DeerInfo.DeerGroup.DeerInfos.RemoveAt(i);
                }
            }

            for (int i = deerInfoList.Count - 1; i >= 0; i--)
            {
                if (deerInfoList[i] == DeerInfo)
                {
                    deerInfoList.RemoveAt(i);
                }
            }

            deerInfos = deerInfoList.ToArray();
            return deerInfoList.Any();
        }
        private Vector2 ComputeSeparation(IEnumerable<DeerInfo> deerInfos)
        {
            Vector2 separation = Vector2.zero;
            foreach (DeerInfo neighbourDeer in deerInfos)
            {
                separation += neighbourDeer.Position - DeerInfo.Position;
            }

            separation *= -1;
            return separation.normalized;
        }
        private Vector2 ComputeAlignment(DeerInfo[] deerInfos)
        {
            Vector2 alignment = Vector2.zero;
            foreach (DeerInfo neighbourDeer in deerInfos)
            {
                alignment += neighbourDeer.Transform.GetComponent<Rigidbody2D>().velocity;
            }
            alignment /= deerInfos.Length;

            return alignment.normalized;
        }
        private Vector2 ComputeCohesion(DeerInfo[] deerInfos)
        {
            Vector2 cohesion = Vector2.zero;
            foreach (DeerInfo neighbourDeer in deerInfos)
            {
                cohesion += neighbourDeer.Position;
            }
            cohesion /= deerInfos.Count();
            cohesion -= DeerInfo.Position;

            return cohesion.normalized;
        }
        
        protected void AvoidBorders()
        {
            while (!DeerInfo.Field.Contains(PredictPosition(CurrentVelocity.normalized, DeerInfo.BorderAvoidingStartDistance)))
            {
                CurrentVelocity = Quaternion.Euler(0, 0, 15) * CurrentVelocity;
            }
        }
        private Vector2 PredictPosition(Vector2 currentVelocity, float distanceFromCurrentPosition)
        {
            return DeerInfo.Position + currentVelocity * distanceFromCurrentPosition;
        }
    }
}
