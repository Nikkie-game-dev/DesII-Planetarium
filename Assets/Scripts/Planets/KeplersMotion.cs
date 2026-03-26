using UnityEngine;

namespace Planets
{
    public class NewMonoBehaviourScript : MonoBehaviour
    {
        [SerializeField] private float semiMajorAxis;
        [SerializeField] private float semiMinorAxis;
        [SerializeField] private float period;
        [SerializeField] private float rotationAngle;

        private float _meanMotion;
        private float _eccentricity;
        private float _timeFromStart;

        private void OnEnable()
        {
            _eccentricity = Mathf.Sqrt(1.0f - semiMinorAxis * semiMinorAxis / (semiMajorAxis * semiMajorAxis));
        }

        private void Update()
        {
            _meanMotion = 2 * Mathf.PI / period;
            //Translate();
            transform.Rotate(Vector3.up, rotationAngle * Time.deltaTime);
            _timeFromStart += Time.deltaTime;
        }
    
        private void Translate()
        {
            // does not work 
            var meanAnomaly = _meanMotion * _timeFromStart;

            //since sin(E) = y/b 
            /*
         From kepler's eq:
         M = E - e sin(E)
         M + e sin(E) = E
         M + e (y/b) = E
         */
            var eccentricAnomaly = meanAnomaly + _eccentricity *
                (transform.position.z / semiMinorAxis);
            var x = semiMajorAxis * (Mathf.Cos(eccentricAnomaly) - _eccentricity);
            var z = semiMinorAxis * Mathf.Sin(eccentricAnomaly);

            transform.Translate(new Vector3(x, 0, z));
        }
    }
}
