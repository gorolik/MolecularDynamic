using Assets.Sources;
using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    /// <summary>
    /// Применяет накопленный за кадр импульс молекулы к его инерции (постоянной скорости)
    /// и сбрасывает импульс
    /// </summary>
    public class MomentumSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ImpulseComponent, MomentumComponent, WeightComponent> _momentumFilter = null;
        private readonly SimulationSettings _simulationSettings = null;

        public void Run()
        {
            foreach (int i in _momentumFilter)
            {
                ref var impulseComponent = ref _momentumFilter.Get1(i);
                ref var impulse = ref impulseComponent.Impulse;
                
                ref var momentumComponent = ref _momentumFilter.Get2(i);
                ref var momentum = ref momentumComponent.Momentum;

                ref var weightComponent = ref _momentumFilter.Get3(i);
                var weight = weightComponent.Weight * Constants.ConvertFromAEMtoKg;

                momentum += (impulse / _simulationSettings.Scale) / (float)weight;
                // нужно ли учитывыать масштаб на скорости молекул?
                
                impulse = Vector3.zero;
            }
        }
    }
}