using Leopotam.Ecs;
using Sources.Ecs.Systems;
using UnityEngine;
using Voody.UniLeo;

namespace Sources.Ecs.Infrastructure
{
    public class SimulationBootstrapper : MonoBehaviour
    {
        [Header("Simulation Settings")] 
        [SerializeField] private SimulationSettings _simulationSettings;
        
        [Header("Links")]
        [SerializeField] private Simulation _simulation;
        
        [Header("Dependencies")]
        [SerializeField] private Transform _camera;

        private EcsWorld _world;
        private EcsSystems _simulationSystems;
        private EcsSystems _updateSystems;

        private void Start()
        {
            _world = new EcsWorld();
            _simulationSystems = new EcsSystems(_world);
            _updateSystems = new EcsSystems(_world);

            _simulationSystems.ConvertScene();
            _updateSystems.ConvertScene();

            AddInjections();
            AddOneFrames();
            
            AddSimulationSystems();
            AddUpdateSystems();

            _simulationSystems.Init();
            _updateSystems.Init();
            
            _simulation.Init(_simulationSystems, _simulationSettings);
        }

        private void AddInjections()
        {
            _simulationSystems.Inject(_simulationSettings);
            
            _updateSystems.Inject(_simulationSettings);
            _updateSystems.Inject(_camera);
        }

        private void AddOneFrames() { }

        private void AddSimulationSystems()
        {
            _simulationSystems.
                Add(new RepulsiveSystem()).
                //Add(new ParticleInteractionSystem()).
                
                Add(new MomentumSystem()).
                
                Add(new LimitingSpaceBoxSystem()).
                
                Add(new InterpolationSystem()).
                Add(new MovementSystem());
        }

        private void AddUpdateSystems()
        {
            _updateSystems.
                Add(new TranslateSystem()).
                Add(new SpritesLookAtCameraSystem());
        }

        private void Update() => 
            _updateSystems.Run();

        private void OnDestroy()
        {
            _simulationSystems.Destroy();
            _simulationSystems = null;
            _world.Destroy();
            _world = null;
        }
    }
}