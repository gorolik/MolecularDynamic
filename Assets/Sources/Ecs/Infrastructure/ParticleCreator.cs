using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Ecs.Infrastructure
{
    public class ParticleCreator : MonoBehaviour
    {
        [Header("Particles Settings")]
        [SerializeField] private GameObject _particlePrefab;

        [Header("Links")] 
        [SerializeField] private Transform _simulationBox;
        [SerializeField] private Transform _simulationParent;
        [SerializeField] private Slider _particlesCount;
        [SerializeField] private TMP_Text _particlesCountDisplay;

        private readonly List<GameObject> _particles = new List<GameObject>();

        private void Start() => 
            DisplayParticlesCount();

        public void CreateParticles()
        {
            float count = _particlesCount.value;

            for (int i = 0; i < count; i++)
            {
                GameObject particle = Instantiate(_particlePrefab, _simulationParent);
                _particles.Add(particle);
            }

            DisplayParticlesCount();
        }

        public void DestroyParticles()
        {
            foreach (GameObject particle in _particles) 
                Destroy(particle);
            
            _particles.Clear();

            DisplayParticlesCount();
        }

        private void DisplayParticlesCount() => 
            _particlesCountDisplay.text = "Количество частиц: " + _particles.Count.ToString();

        private Vector3 GetRandomSpawnPosition()
        {
            return Vector3.zero;
        }
    }
}