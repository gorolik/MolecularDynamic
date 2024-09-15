using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Задает новую позицию обьекта, применяя интерполяцию, согласно настройкам
    /// </summary>
    internal sealed class TranslateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TransformComponent, PositionComponent, InterpolationComponent> _translationFilter = null;
        private readonly SimulationSettings _settings = null;
        
        public void Run()
        {
            foreach (int i in _translationFilter)
            {
                ref var transformComponent = ref _translationFilter.Get1(i);
                ref var transform = ref transformComponent._transform;
                
                ref var positionComponent = ref _translationFilter.Get2(i);
                ref var position = ref positionComponent.Position;
                
                ref var interpolationComponent = ref _translationFilter.Get3(i);
                ref var previousPosition = ref interpolationComponent.PreviousPosition;
                ref var delta = ref interpolationComponent.Delta;

                if (_settings.Interpolation)
                {
                    if(_settings.DeltaTime != 0)
                        delta += 1 / _settings.DeltaTime * Time.deltaTime;
                    
                    transform.position = Vector3.Lerp(previousPosition, position, delta);
                }
                else
                    transform.position = position;
            }
        }
    }
}