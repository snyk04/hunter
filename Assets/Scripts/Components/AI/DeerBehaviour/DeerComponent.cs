﻿using Hunter.AI.Common;
using Hunter.Creatures.Common;
using UnityEngine;

namespace Hunter.AI.DeerBehaviour
{
    [RequireComponent(typeof(MoverComponent))]
    public class DeerComponent : MonoBehaviour
    {
        [Header("Wandering")] 
        [SerializeField] private float _wanderingSpeed;

        [Header("Flee")] 
        [SerializeField] private float _fleeSpeed;
        [SerializeField] private float _fleeStartDistance;
        [SerializeField] private float _fleeStopDistance;
        
        [Header("Boids settings")]
        [SerializeField] private float _separationForce;
        [SerializeField] private float _alignmentForce;
        [SerializeField] private float _cohesionForce;
        
        [Header("Other")]
        [SerializeField] private float _borderAvoidingStartDistance;

        public Deer Deer { get; private set; }
        
        private void Update()
        {
            Deer.Update();
        }

        public void Initialize(Field field)
        {
            Mover mover = GetComponent<MoverComponent>().Mover;
            var rb2D = GetComponent<Rigidbody2D>();

            Deer = new Deer(
                transform, 
                mover, 
                rb2D, 
                field,
                _borderAvoidingStartDistance,
                _wanderingSpeed,
                _fleeSpeed, 
                _fleeStartDistance, 
                _fleeStopDistance,
                _separationForce,
                _alignmentForce,
                _cohesionForce
                );
        }
    }
}
