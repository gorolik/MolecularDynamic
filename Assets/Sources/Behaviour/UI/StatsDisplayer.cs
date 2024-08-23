using Sources.Ecs.Infrastructure;
using TMPro;
using UnityEngine;

public class StatsDisplayer : MonoBehaviour
{
    private const float _gasConstant = 8.314f;
    private const float _moleculeCountH2 = 6.022e23f;
    private const float _boltzmannConstant = 1.38e-23f;

    [Header("UI")] 
    [SerializeField] private string _pressurePrefix = "Давление: ";
    [SerializeField] private string _pressurePostfix = " Па";
    [SerializeField] private TMP_Text _pressure;
    [SerializeField] private string _molecularSpeedPrefix = "Скорость молекул: ";
    [SerializeField] private string _molecularSpeedPostfix = " м/с";
    [SerializeField] private TMP_Text _molecularSpeed;
    [Header("Links")]
    [SerializeField] private ParticleCreator _particleCreator;
    [SerializeField] private Transform _volumeBox;
    [SerializeField] private float _moleculeWeight;

    private SimulationSettings _settings;

    public void Init(SimulationSettings settings) =>
        _settings = settings;
    
    private void Update()
    {
        float moles = _particleCreator.ParticlesCount / _moleculeCountH2;
        float volume = _volumeBox.localScale.x * _volumeBox.localScale.y * _volumeBox.localScale.z;
        
        float pressure = (moles * _gasConstant * _settings.Temperature) / volume;
        _pressure.text = _pressurePrefix + pressure + _pressurePostfix;
        
        var v = Mathf.Sqrt((8.0f * _boltzmannConstant * _settings.Temperature) / (Mathf.PI * _moleculeWeight));
        _molecularSpeed.text = _molecularSpeedPrefix + v + _molecularSpeedPostfix;
    }
}
