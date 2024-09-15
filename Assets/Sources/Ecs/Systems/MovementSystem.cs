using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Обновляет виртуальную позицию согласно скорости и прошедшему времени между кадрами
    /// </summary>
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, MomentumComponent> _movableFilter = null;
        private readonly SimulationSettings _settings = null;
        
        public void Run()
        {
            foreach (int i in _movableFilter)
            {
                ref var positionComponent = ref _movableFilter.Get1(i);
                ref var position = ref positionComponent.Position;
                
                ref var momentumComponent = ref _movableFilter.Get2(i);
                ref var momentum = ref momentumComponent.Momentum;
                
                position += momentum * _settings.DeltaTime; 
                // ограничивать световую скорость тут или там (если её вообще надо ограничивать, тк симуляция не про это)
            }
        }
    }
}