using UnityEngine;

namespace Assets.Quternion
{
    public class RotatorLookRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis = Vector3.zero;
        [SerializeField] private Vector3 _offset = new Vector3(65, -18, -25);

        private void Update()
        {

            //Creates a rotation with the specified forward and upwards directions.
            //Z axis will be aligned with forward, X axis aligned with cross product between forward and upwards, and Y axis aligned with cross product between Z and X.
            // the second argument, upwards, defaults to Vector3.up
            Quaternion adjustedDirection = Quaternion.LookRotation(transform.position - _offset, _axis);
            transform.rotation = adjustedDirection;
            Debug.DrawRay(transform.position, _axis * 10f, Color.yellow);
            Debug.DrawRay(transform.position, (transform.position - _offset) * 10f, Color.blue);
        }

    }
}
