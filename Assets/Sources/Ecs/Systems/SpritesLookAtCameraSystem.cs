using Leopotam.Ecs;
using Sources.Ecs.Components;
using UnityEngine;

namespace Sources.Ecs.Systems
{
    public class SpritesLookAtCameraSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewComponent> _viewFilter = null;
        private readonly Transform _camera = null;

        public void Run()
        {
            foreach (int i in _viewFilter)
            {
                ref var viewComponent = ref _viewFilter.Get1(i);
                ref var view = ref viewComponent._view;
                
                view.LookAt(_camera);
            }
        }
    }
}