using Leopotam.Ecs;
using Sources.Ecs.Components;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Записывает позицию перед её изменением для дальнейшей интерполяции
    /// </summary>
    public class InterpolationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, InterpolationComponent> _interpolationFilter = null;
        
        public void Run()
        {
            foreach (int i in _interpolationFilter)
            {
                ref var positionComponent = ref _interpolationFilter.Get1(i);
                ref var position = ref positionComponent.Position;
                
                ref var interpolationComponent = ref _interpolationFilter.Get2(i);
                ref var previousPosition = ref interpolationComponent.PreviousPosition;
                ref var delta = ref interpolationComponent.Delta;

                delta = 0;
                previousPosition = position;
            }
        }
    }
}