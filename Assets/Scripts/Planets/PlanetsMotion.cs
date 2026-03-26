using UnityEngine;

namespace Planets
{
    public class PlanetsMotion : MonoBehaviour
    {
        [SerializeField] private Transform centralStar;
        [SerializeField] private float translationSpeed;
        [SerializeField] private float rotSpeed;


        private float _semiMajorAxis;
        private float _semiMinorAxis;
        private bool _goLeft;

        private void OnEnable()
        {

            if (centralStar == null) return;
        
            var initPos = transform.position;

            var b = initPos.x * initPos.x + initPos.z * initPos.z - centralStar.position.x * centralStar.position.x;
            var kA = (-b - Mathf.Sqrt(b * b + 4 * initPos.z * initPos.z * centralStar.position.x * centralStar.position.x)) /
                     -2;
            var kB = (-b + Mathf.Sqrt(b * b + 4 * initPos.z * initPos.z * centralStar.position.x * centralStar.position.x)) /
                     -2;

            if (kA < 0)
            {
                if (kB < 0)
                {
                    Debug.LogError("No solution for given values");
                    return;
                }

                _semiMinorAxis = kB;
            }
            else
            {
                _semiMinorAxis = kA;
            }

            _semiMajorAxis = Mathf.Sqrt(centralStar.position.x * centralStar.position.x + _semiMinorAxis * _semiMinorAxis);
        }

        private void Update()
        {
            float newX, newZ;

            if (transform.position.x > _semiMajorAxis)
            {
                _goLeft = true;
            }
            else if (transform.position.x < -_semiMajorAxis)
            {
                _goLeft = false;
            }

            if (_goLeft)
            {
                newX = transform.position.x - Time.deltaTime * translationSpeed;
            }
            else
            {
                newX = transform.position.x + Time.deltaTime * translationSpeed;
            }

            newZ = Mathf.Sqrt(_semiMinorAxis * _semiMinorAxis * (1 - newX * newX / (_semiMajorAxis * _semiMajorAxis)));

            transform.position = new Vector3(newX, transform.position.y, newZ);
        
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
        }
    }
}