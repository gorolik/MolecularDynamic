using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    public class TemperatureSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TemperatureComponent, MomentumComponent, WeightComponent> _temperatureFilter = null;
        private readonly SimulationSettings _settings = null;

        private const float _boltzmannConstant = 1.38e-23f;
        
        public void Run()
        {
            foreach (int i in _temperatureFilter)
            {
                ref var temperatureComponent = ref _temperatureFilter.Get1(i);
                ref var settedTemperature = ref temperatureComponent.Temperature;
                
                /*ref var impulseComponent = ref _temperatureFilter.Get2(i);
                ref var impulse = ref impulseComponent.Impulse;*/
                
                ref var momentumComponent = ref _temperatureFilter.Get2(i);
                ref var momentum = ref momentumComponent.Momentum;
                
                ref var weightComponent = ref _temperatureFilter.Get3(i);
                ref var weight = ref weightComponent._weight;

                if (_settings.Temperature != settedTemperature)
                {
                    var direction = Vector3.zero;
                    
                    if (momentum.magnitude < 1)
                        direction = (new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f))).normalized;
                    else
                        direction = momentum.normalized;

                    var v = Mathf.Sqrt((8.0f * _boltzmannConstant * _settings.Temperature) / (Mathf.PI * weight));
                    momentum = direction * v;

                    settedTemperature = _settings.Temperature;
                }
            }
        }
        
                    /*var direction = new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f));
                    direction.Normalize();

                    var v = Mathf.Sqrt((8.0f * _boltzmannConstant * _settings.Temperature) / (Mathf.PI * weight));

                    direction *= v;
                    impulse += direction;*/ // единоразовое применение температуры
    }
}