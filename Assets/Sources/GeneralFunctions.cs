using Sources.Ecs.Infrastructure;
using UnityEngine;

namespace Assets.Sources
{
    public static class GeneralFunctions
    {
        private static SimulationSettings _simulationSettings;

        public static void Init(SimulationSettings simulationSettings) => 
            _simulationSettings = simulationSettings;

        public static float GetDistance(Vector3 first, Vector3 second) =>
            Vector3.Distance(first, second) / _simulationSettings.Scale;
    }
}