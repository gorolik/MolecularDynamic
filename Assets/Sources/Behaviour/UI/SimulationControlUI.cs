using System;
using Sources.Ecs.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Behaviour.UI
{
    public class SimulationControlUI : MonoBehaviour
    {
        [Header("Start Stop Simulation")]
        [SerializeField] private TMP_Text _startStopButtonText;
        [SerializeField] private string _startSimulation = "Запустить симуляцию";
        [SerializeField] private string _stopSimulation = "Остановить симуляцию";
        [Header("Particles Control")]
        [SerializeField] private Slider _particlesCount;
        [SerializeField] private TMP_Text _particlesCountText;
        [SerializeField] private string _particlesCountPrefix = "Количество частиц: ";
        [Header("Temperature")]
        [SerializeField] private Slider _temperature;
        [Header("Simulation Speed")]
        [SerializeField] private Slider _simulationSpeed;

        private Simulation _simulation;
        private ParticleCreator _particleCreator;
        private SimulationSettings _settings;

        public void Init(Simulation simulation, ParticleCreator particleCreator, SimulationSettings settings)
        {
            _simulation = simulation;
            _particleCreator = particleCreator;
            _settings = settings;

            ValidateViewData();
        }

        private void OnEnable()
        {
            _temperature.onValueChanged.AddListener(ChangeTemperature);
            _simulationSpeed.onValueChanged.AddListener(ChangeSimulationSpeed);
        }

        private void OnDisable()
        {
            _temperature.onValueChanged.RemoveListener(ChangeTemperature);
            _simulationSpeed.onValueChanged.RemoveListener(ChangeSimulationSpeed);
        }

        private void ChangeSimulationSpeed(float value) => 
            _settings.SetSimulationSpeed(value);

        public void SwitchSimulationState()
        {
            if(_simulation.Simulating)
                _simulation.Pause();
            else
                _simulation.Continue();
            
            ValidateViewData();
        }

        public void ChangeTemperature(float value) => 
            _settings.SetTemperature(value);

        public void CreateParticles()
        {
            int count = Mathf.RoundToInt(_particlesCount.value);
            _particleCreator.CreateParticles(count);
            
            ValidateViewData();
        }

        public void ClearParticles()
        {
            _particleCreator.DestroyParticles();
            
            ValidateViewData();
        }

        private void ValidateViewData()
        {
            if (_simulation.Simulating)
                _startStopButtonText.text = _stopSimulation;
            else
                _startStopButtonText.text = _startSimulation;

            _particlesCountText.text = _particlesCountPrefix + _particleCreator.ParticlesCount;

            _temperature.value = _settings.Temperature;
            _simulationSpeed.value = _settings.SimulationSpeed;
        }
    }
}