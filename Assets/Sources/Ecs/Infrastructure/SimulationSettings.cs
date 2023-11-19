using System;
using UnityEngine;

namespace Sources.Ecs.Infrastructure
{
    [Serializable]
    public class SimulationSettings
    {
        [SerializeField] [Range(0.01f, 0.1f)] private float _timeStep = 0.02f;
        [SerializeField] [Range(0f, 2f)] private float _simulationSpeed = 1;
        [SerializeField] private bool _interpolation = true;
        [SerializeField] private float _scale = 1;

        //[Header("Limiting Space Box Settings")] 
        //[SerializeField] private bool _pushOutOfBoundsByForce = false;

        public float TimeStep => _timeStep;
        public float DeltaTime => _timeStep * _simulationSpeed;
        public bool Interpolation => _interpolation;
        public float Scale => _scale;
        //public bool PushOutOfBoundsByForce => _pushOutOfBoundsByForce;
    }
}