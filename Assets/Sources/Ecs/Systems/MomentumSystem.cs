using Leopotam.Ecs;
using Sources.Ecs.Components;
using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    public class MomentumSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ImpulseComponent, MomentumComponent> _momentumFilter = null;
        //private readonly SimulationSettings _settings = null;
        
        public void Run()
        {
            foreach (int i in _momentumFilter)
            {
                ref var impulseComponent = ref _momentumFilter.Get1(i);
                ref var impulse = ref impulseComponent.Impulse;
                
                ref var momentumComponent = ref _momentumFilter.Get2(i);
                ref var momentum = ref momentumComponent.Momentum;
                
                momentum += impulse; // тут был импульс умножить на дельта тайм, убрал тк это мешало детерминированности импульса при разных скоростях симуляции
                
                impulse = Vector3.zero;
            }
        }
    }
}