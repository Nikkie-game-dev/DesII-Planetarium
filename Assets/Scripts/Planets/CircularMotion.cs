using UnityEngine;

namespace Planets
{
    public class CircularMotion : MonoBehaviour
    {
        [SerializeField] private Transform centralStar;
        [SerializeField] private float translationSpeed;
        [SerializeField] private float rotSpeed;

        private void Update()
        {
            transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
            transform.RotateAround(centralStar.transform.position, Vector3.up, translationSpeed * Time.deltaTime);
        }
    }
}
