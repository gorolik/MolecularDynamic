using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    internal sealed class RepulsiveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, ImpulseComponent, RepulsiveComponent> _repulsiveFilter = null;
        private readonly SimulationSettings _settings;
        
        public void Run()
        {
            foreach (int i in _repulsiveFilter)
            {
                ref var selfPositionComponent = ref _repulsiveFilter.Get1(i);
                ref var selfPosition = ref selfPositionComponent.Position;
                
                ref var selfRepulsiveComponent = ref _repulsiveFilter.Get3(i);
                ref var selfRadius = ref selfRepulsiveComponent._radius;

                foreach (int j in _repulsiveFilter)
                {
                    if(j == i)
                        continue;
                    
                    ref var otherPositionComponent = ref _repulsiveFilter.Get1(j);
                    ref var otherPosition = ref otherPositionComponent.Position;
                
                    ref var otherRepulsiveComponent = ref _repulsiveFilter.Get3(j);
                    ref var otherRadius = ref otherRepulsiveComponent._radius;

                    float sqrDistance = GetSqrDistance(selfPosition, otherPosition);
                    float sqrCumulativeRadius = (selfRadius + otherRadius) * (selfRadius + otherRadius);

                    if (sqrDistance <= sqrCumulativeRadius)
                    {
                        ref var selfImpulseComponent = ref _repulsiveFilter.Get2(i);
                        ref var selfImpulse = ref selfImpulseComponent.Impulse;

                        ref var selfPower = ref selfRepulsiveComponent._power;
                        
                        ref var otherPower = ref otherRepulsiveComponent._power;

                        var direction = (otherPosition - selfPosition).normalized;
                        var power = selfPower * otherPower;
                        var distanceRatio = 1 - Mathf.Sqrt(sqrDistance) / Mathf.Sqrt(sqrCumulativeRadius);

                        if (direction == Vector3.zero)
                            direction = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), Random.Range(-1, 2));

                        var addingImpulse = direction * distanceRatio * power;
                        selfImpulse -= addingImpulse;
                    }
                }
            }
        }
        
        private float GetSqrDistance(Vector3 pointA, Vector3 pointB)
        {
            Vector3 offset = pointB - pointA;
            return offset.x * offset.x + offset.y * offset.y + offset.z * offset.z;
        }
    }
}