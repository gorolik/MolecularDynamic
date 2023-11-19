using System;

namespace Sources.Ecs.Components
{
    [Serializable]
    public struct RepulsiveComponent
    {
        public float _radius;
        public float _power;
    }
}