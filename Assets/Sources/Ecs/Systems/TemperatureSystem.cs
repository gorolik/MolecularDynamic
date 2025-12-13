using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;
using Assets.Sources;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Устанавливает скорость только что созданным молекулам в зависимости от температуры системы
    /// (сейчас температура системы представляет собой величину, котнтролируемую извне)
    /// ps в будущем можно сделать, чтобы эта система устанавливала скорость всем молекулам 
    /// взависимости от выставленной температуры по нажатии на кнопку
    /// </summary>
    public class TemperatureSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TemperatureComponent, MomentumComponent, WeightComponent> _temperatureFilter = null;
        private readonly SimulationSettings _settings = null;

        public void Run() 
        {
            foreach (int i in _temperatureFilter)
            {
                ref var temperatureComponent = ref _temperatureFilter.Get1(i);
                ref var settedTemperature = ref temperatureComponent.Temperature;
                
                ref var momentumComponent = ref _temperatureFilter.Get2(i);
                ref var momentum = ref momentumComponent.Momentum;
                
                ref var weightComponent = ref _temperatureFilter.Get3(i);
                var weight = weightComponent.Weight * Constants.ConvertFromAEMtoKg;

                if (_settings.Temperature != settedTemperature)
                {
                    var direction = Vector3.zero;
                    
                    if (momentum.magnitude < 1)
                        direction = (new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f))).normalized;
                    else
                        direction = momentum.normalized;

                    var v = Mathf.Sqrt((float)((8.0f * Constants.BoltzmannConstant * _settings.Temperature) / (Mathf.PI * (weight))));
                    momentum = direction * v;
                    // выставляем в моментум, чтобы при ручном изменении температуры скорость устанавливалась
                    // точно по формуле, ниже закоментирован вариант записи движения в импульс,
                    // но его нужно ещё проверять, а ещё, если во время применения импульса будет
                    // рассчет по массе, то нужно именно моментум дергать

                    settedTemperature = _settings.Temperature;
                }
            }
        }
        
                    /*var direction = new Vector3(Random.Range(-1f, 2f), Random.Range(-1f, 2f), Random.Range(-1f, 2f));
                    direction.Normalize();

                    var v = Mathf.Sqrt((8.0f * _boltzmannConstant * _settings.Temperature) / (Mathf.PI * weight)); массу перевести в кг

                    direction *= v;
                    impulse += direction;*/ // единоразовое применение температуры
    }
}