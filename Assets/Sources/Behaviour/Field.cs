using UnityEngine;

namespace Sources.Behaviour
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _power;
        
        public float Radius => _radius;
        public float Power => _power;
    }
}
