using UnityEngine;

namespace Assets.Quternion
{
    public class RotatorAngleAxis : MonoBehaviour
    {
        [SerializeField] private Vector3 _axis = Vector3.up;
        [SerializeField] private float _angle = 45f;

        private void Update()
        {
            // Sets the transform's current rotation to a new rotation that rotates 30 degrees around the y-axis(Vector3.up)
            var adjustedDirection = Quaternion.AngleAxis(_angle, _axis);
            transform.rotation = adjustedDirection;
            Debug.DrawRay(transform.position, _axis * 10f, Color.yellow);
        }

    }
}
