using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using Sources.Ecs.Tags;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Ограничение движение молекул внутри коробки
    /// </summary>
    internal sealed class LimitingSpaceBoxSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ParticleTag, PositionComponent, MomentumComponent> _movableFilter = null;
        private readonly EcsFilter<LimitingBoxTag, TransformComponent> _boxFilter = null;
        private readonly SimulationSettings _settings = null;

        public void Run()
        {
            foreach (int i in _movableFilter) // тут мб можно поменять местами циклы
            {
                ref var positionComponent = ref _movableFilter.Get2(i);
                ref var position = ref positionComponent.Position;
                
                ref var momentumComponent = ref _movableFilter.Get3(i);
                ref var momentum = ref momentumComponent.Momentum;

                foreach (int j in _boxFilter)
                {
                    ref var boxModelComponent = ref _boxFilter.Get2(j);
                    ref var boxTransform = ref boxModelComponent._transform;
                    
                    var boxPosition = boxTransform.position;
                    var boxScale = boxTransform.localScale;
                    
                    var boundsMin = boxPosition - boxScale / 2;
                    var boundsMax = boxPosition + boxScale / 2;

                    var nextDeltaPos = position + momentum * _settings.DeltaTime;
                    
                    // алгоритм учитывает момент столкновения и после этого перенаправляет оставшееся движение
                    for (int g = 0; g < 3; g++)
                    {
                        if (nextDeltaPos[g] > boundsMax[g] || nextDeltaPos[g] < boundsMin[g])
                        {
                            var bound = 0f;

                            if (nextDeltaPos[g] > boundsMax[g])
                                bound = boundsMax[g];
                            else if (nextDeltaPos[g] < boundsMin[g]) 
                                bound = boundsMin[g];

                            var nextPos = position + momentum;
                            
                            momentum[g] = position[g] - nextPos[g];

                            //var newPosG = position[g] + momentum[g];
                            
                            position[g] = bound;
                            
                            /*if (_settings.PushOutOfBoundsByForce)
                            {
                                if (newPosG > boundsMax[g])
                                    momentum[g] += boundsMax[g] - newPosG;
                                else if (newPosG < boundsMin[g])
                                    momentum[g] += boundsMin[g] - newPosG;
                            }*/
                        }
                    }
                }
            }
        }
    }
}