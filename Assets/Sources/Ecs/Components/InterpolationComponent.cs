using UnityEngine;

namespace Sources.Ecs.Components
{
    public struct InterpolationComponent
    {
        public Vector3 PreviousPosition;
        public float Delta;
    }
}