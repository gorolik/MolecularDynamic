using System;
using UnityEngine;

namespace Sources.Ecs.Infrastructure
{
    [Serializable]
    public class SimulationSettings
    {
        [SerializeField] [Range(0.001f, 0.02f)] private float _timeStep = 0.01f;
        [SerializeField] [Range(0f, 2f)] private float _simulationSpeed = 1;
        [SerializeField] [Range(173f, 373f)] private float _temperature = 273;
        [SerializeField] private bool _interpolation = true;
        [SerializeField] private float _scale = 1;

        public float TimeStep => _timeStep;
        public float DeltaTime => _timeStep * _simulationSpeed;
        public float SimulationSpeed => _simulationSpeed;
        public float Temperature => _temperature;
        public bool Interpolation => _interpolation;
        public float Scale => _scale;
        
        public void SetTemperature(float value) => 
            _temperature = value;

        public void SetSimulationSpeed(float value) => 
            _simulationSpeed = value;
    }
}