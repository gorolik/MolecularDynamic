using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Ecs.Systems
{
    [Obsolete("Используйте LennardJonesSystem")]
    public class ParticleInteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, ImpulseComponent, WeightComponent> _atomicFilter = null;
        private readonly SimulationSettings _settings = null;

        public void Run()
        {
            foreach (int i in _atomicFilter)
            {
                ref var selfPositionComponent = ref _atomicFilter.Get1(i);
                ref var selfPosition = ref selfPositionComponent.Position;

                ref var selfImpulseComponent = ref _atomicFilter.Get2(i);
                ref var selfImpulse = ref selfImpulseComponent.Impulse;
                
                ref var selfWeightComponent = ref _atomicFilter.Get3(i);
                ref var selfWeight = ref selfWeightComponent.Weight;
                
                foreach (int j in _atomicFilter)
                {
                    if (j == i)
                        continue;

                    ref var otherPositionComponent = ref _atomicFilter.Get1(j);
                    ref var otherPosition = ref otherPositionComponent.Position;
                    
                    var distance = Vector3.Distance(selfPosition, otherPosition);
                    distance *= 1 / _settings.Scale;
                    
                    var direction = GetDirection(otherPosition, selfPosition);
                    
                    var power = 1 / Mathf.Pow(distance, 6) - 1 / Mathf.Pow(distance, 12);

                    var addingImpulse = power / selfWeight * direction;
                    
                    selfImpulse += addingImpulse;
                }
            }
        }

        private static Vector3 GetDirection(Vector3 otherPosition, Vector3 selfPosition)
        {
            var direction = (otherPosition - selfPosition).normalized;

            if (direction == Vector3.zero)
                direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2));
            
            return direction;
        }
    }
}