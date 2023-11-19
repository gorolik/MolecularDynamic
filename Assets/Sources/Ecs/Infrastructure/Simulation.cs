using Leopotam.Ecs;
using UnityEngine;

namespace Sources.Ecs.Infrastructure
{
    public class Simulation : MonoBehaviour
    {
        private SimulationSettings _settings;
        private EcsSystems _systems;
        private float _currentTime;
        private bool _simulating;

        public void Init(EcsSystems systems, SimulationSettings settings)
        {
            _settings = settings;
            _systems = systems;
        }

        public void Pause() => 
            _simulating = false;

        public void Continue() => 
            _simulating = true;

        private void Update()
        {
            if(_simulating == false)
                return;
            
            _currentTime += Time.deltaTime;
            
            while (_currentTime >= _settings.TimeStep)
            {
                _systems.Run();
                _currentTime -= _settings.TimeStep;
            }
        }
    }
}
