using Leopotam.Ecs;
using Sources.Ecs.Components;
using Assets.Sources.Ecs.Components;
using System.Collections;
using UnityEngine;

namespace Assets.Sources.Ecs.Systems
{
    /// <summary>
    /// Учет потенциала Леннарда Джонсона
    /// </summary>
    public class LennardJonesInteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LennardJonesComponent, PositionComponent, ImpulseComponent> _moleculesFilter = null;

        public void Run()
        {
            foreach (int i in _moleculesFilter)
            {
                ref var selfLennardJonesComponent = ref _moleculesFilter.Get1(i);
                ref var selfEpsilon = ref selfLennardJonesComponent.Epsilon;
                ref var selfSigma = ref selfLennardJonesComponent.Sigma;

                ref var selfPositionComponent = ref _moleculesFilter.Get2(i);
                ref var selfPosition = ref selfPositionComponent.Position;

                ref var selfImpulseComponent = ref _moleculesFilter.Get3(i);
                ref var selfImpulse = ref selfImpulseComponent.Impulse;

                foreach (int j in _moleculesFilter)
                {
                    if (j == i)
                        continue;

                    ref var otherLennardJonesComponent = ref _moleculesFilter.Get1(j);
                    ref var otherEpsilon = ref otherLennardJonesComponent.Epsilon;
                    ref var otherSigma = ref otherLennardJonesComponent.Sigma;

                    ref var otherPositionComponent = ref _moleculesFilter.Get2(j);
                    ref var otherPosition = ref otherPositionComponent.Position;


                    Vector3 direction = (otherPosition - selfPosition).normalized;
                    float distance = GeneralFunctions.GetDistance(otherPosition, selfPosition);

                    if (distance == 0)
                        continue;

                    float epsilon = Mathf.Sqrt((float)(selfEpsilon * otherEpsilon));
                    float sigma = (float)(selfSigma + otherSigma) / 2f;

                    sigma *= 1e-10f; // в метрах
                    epsilon *= 1.602e-19f; // в джоулях

                    var forceMagnitude = LennardJonesForceMagnitude(distance, epsilon, sigma);

                    Vector3 force = direction * forceMagnitude;
                    selfImpulse += force;
                }
            }
        }

        private float LennardJonesForceMagnitude(float r, float epsilon, float sigma)
        {
            float sigmaOverR = sigma / r;
            float sigmaOverR6 = Mathf.Pow((float)sigmaOverR, 6);
            float sigmaOverR12 = sigmaOverR6 * sigmaOverR6;

            float force = 24 * epsilon * (2 * sigmaOverR12 - sigmaOverR6) / r;
            return force;
        }
    }
}