using Assets.Sources;
using Assets.Sources.Ecs.Systems;
using Leopotam.Ecs;
using Sources.Behaviour.UI;
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
        [SerializeField] private SimulationData _simulationData;
        [SerializeField] private ParticleCreator _particleCreator;
        [SerializeField] private SimulationControlUI _simulationControlUI;
        [SerializeField] private StatsDisplayer _statsDisplayer;
        
        [Header("Dependencies")]
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _particlesParent;

        private EcsWorld _world;
        private EcsSystems _simulationSystems;
        private EcsSystems _updateSystems;

        private void Start() // мб поделить EcsBootstrapper и SimulationBootstrapper
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

            GeneralFunctions.Init(_simulationSettings);
            _particleCreator.Init(_particlesParent);
            _simulation.Init(_simulationSystems, _simulationSettings);
            _simulationControlUI.Init(_simulation, _particleCreator, _simulationSettings);
            _statsDisplayer.Init(_simulationSettings);
        }

        private void AddInjections()
        {
            _simulationSystems.Inject(_simulationSettings);

            _updateSystems.Inject(_simulationSettings);
            _updateSystems.Inject(_simulationData);
            _updateSystems.Inject(_camera);
        }

        private void AddOneFrames() { }

        // Системы, непосредственно участвующие в поведении симуляции
        private void AddSimulationSystems()
        {
            _simulationSystems.
                Add(new TemperatureSystem()).
                Add(new LennardJonesInteractionSystem()).

                Add(new MomentumSystem()).
                
                Add(new LimitingSpaceBoxSystem()).
                
                Add(new InterpolationSystem()).
                Add(new MovementSystem());
        }

        // Все вспомогательныее системы, выполняются после просчета симуляции
        private void AddUpdateSystems()
        {
            _updateSystems.
                Add(new TranslateSystem()).
                Add(new SpritesLookAtCameraSystem()).
                Add(new AverageMoleculeSpeedCalsSystem());
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