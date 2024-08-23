using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voody.UniLeo;

namespace Sources.Ecs.Infrastructure
{
    public class ParticleCreator : MonoBehaviour
    {
        [Header("Particles Settings")]
        [SerializeField] private GameObject _particlePrefab;
        
        private Transform _simulationParent;

        //private readonly Dictionary<GameObject, EcsEntity> _particles = new Dictionary<GameObject, EcsEntity>();

        private readonly List<GameObject> _particles = new List<GameObject>();
        
        public int ParticlesCount => _particles.Count;

        public void Init(Transform simulationParent) => 
            _simulationParent = simulationParent;

        public void CreateParticles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject particle = Instantiate(_particlePrefab, _simulationParent);
                _particles.Add(particle);
                /*ConvertToEntity particleConvert = particle.GetComponent<ConvertToEntity>();
                
                if (particleConvert.TryGetEntity().HasValue)
                {
                    EcsEntity entity = particleConvert.TryGetEntity().Value;
                    _particles.Add(particle, entity);
                }*/
            }
        }

        public void DestroyParticles()
        {
            /*foreach (var particle in _particles)
            {
                particle.Value.Destroy();
                Destroy(particle.Key);
            }

            _particles.Clear();*/

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}