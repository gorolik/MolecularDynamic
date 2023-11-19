using UnityEngine;

namespace Sources.Behaviour
{
    public class FieldForce : MonoBehaviour
    {
        [SerializeField] private Field _selfField;

        private Field[] _fields;
        
        private void Start()
        {
            _fields = FindObjectsOfType<Field>();
        }

        private void FixedUpdate()
        {
            foreach (Field field in _fields)
            {
                if (field != _selfField)
                {
                    float distance = Vector3.Distance(transform.position, field.transform.position);
                    float cumulativeRadius = _selfField.Radius + field.Radius;
                    
                    if (distance <= cumulativeRadius)
                    {
                        Vector3 direction = field.transform.position - transform.position;
                        float power = _selfField.Power * field.Power;
                        float distanceRatio = 1 - (distance / cumulativeRadius);
                        
                        transform.position -= direction * (distanceRatio * power);
                    }
                }
            }
        }
    }
}
