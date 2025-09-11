using UnityEngine;

public class RotateAround : MonoBehaviour
{
    [SerializeField] private Transform _center;

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.LookAt(_center);
        transform.RotateAround(_center.position, Vector3.up, 30 * Time.deltaTime);

        Quaternion angle = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up);

        _center.rotation = angle;

        Debug.DrawRay(_center.position, transform.position - _center.position, Color.yellow);
        Debug.DrawRay(_center.position, -_center.right * 5f, Color.red);
        Debug.DrawRay(_center.position, _center.forward, Color.blue);
    }
}
