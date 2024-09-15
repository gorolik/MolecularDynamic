using Sources.Ecs.Infrastructure;
using TMPro;
using UnityEngine;

public class StatsDisplayer : MonoBehaviour
{
    private const double _boltsmanConstant = 1.38e-23;

    [Header("UI")] 
    [SerializeField] private string _pressurePrefix = "Давление: ";
    [SerializeField] private string _pressurePostfix = " Па";
    [SerializeField] private TMP_Text _pressure;
    [SerializeField] private string _molecularSpeedPrefix = "Ср. скорость молекул: ";
    [SerializeField] private string _molecularSpeedPostfix = " м/с";
    [SerializeField] private TMP_Text _molecularSpeed;
    [Header("Links")]
    [SerializeField] private ParticleCreator _particleCreator;
    [SerializeField] private SimulationData _simulationData;
    [SerializeField] private Transform _volumeBox;
    [SerializeField] private float _moleculeWeight;

    private SimulationSettings _settings;

    public void Init(SimulationSettings settings) =>
        _settings = settings;
    
    private void Update()
    {
        float volume = _volumeBox.localScale.x * _volumeBox.localScale.y * _volumeBox.localScale.z;
        double pressure = (_particleCreator.ParticlesCount * _boltsmanConstant * _settings.Temperature) / volume;
        _pressure.text = _pressurePrefix + pressure + _pressurePostfix;

        _molecularSpeed.text = 
            _molecularSpeedPrefix + 
            _simulationData.AverageParticlesMovementSpeed + 
            _molecularSpeedPostfix;
    }
}
