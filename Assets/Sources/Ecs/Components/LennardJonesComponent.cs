using System;
using System.Collections;
using UnityEngine;

namespace Assets.Sources.Ecs.Components
{
    [Serializable]
    public struct LennardJonesComponent
    {
        [Tooltip("Глубина потенциальной ямы в эВ")] 
        public double Epsilon;
        [Tooltip("Расстояние, при котором потенциал = 0 в А")] 
        public double Sigma;
    }
}