using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private float _speed  = 10.0f;

    private Vector3 _moveDirection = Vector3.zero;

    private void OnEnable()
    {

    }

    private void Update()
    {
        Move();
    }


    private void OnDestroy()
    {
        
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, _speed * Time.deltaTime);

        if(_moveDirection.magnitude > 0)
        {
            Rotate(_moveDirection);
        }

    }

    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void Rotate(Vector3 direction)
    {
        Debug.DrawRay(transform.position, direction * 5f, Color.white, 1f);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 250f * Time.deltaTime);
    }
}
