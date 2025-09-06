using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    //private Rigidbody _rb;
    private CharacterController _characterController;

    private bool _isMovement;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed = 500f;

    private bool _isEventCheckMovmentSent = false;
    private float _jumpForce = 0;
    private float _speed = 0;
    private float _gravity = 0;
    private float _verticalVelocity = 0;

    public event Action<bool> OnMovment;
    public event Action<bool> OnGravity;

    public event Action<float> OnGravityAmountChanged;
    public event Action<float> OnGravityCurrentChanged;
    public event Action<float> OnJumpAmountChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<Vector3> OnForceChanged;
    public event Action<bool> OnIsGround;

    //public bool IsGrounded { get; private set; }

    private void Awake()
    {
        //_rb = GetComponent<Rigidbody>();
        //_rb.freezeRotation = true;
        _characterController = GetComponent<CharacterController>();
        //IsGrounded = true;
    }

    private void Update()
    {
        //SetGravity(1f, 10f);
        UpdatePosititon();
        HandleGravity();

        if (_moveDirection.magnitude > 0)
        {
            Rotate(_moveDirection, _rotateSpeed);
            _isMovement = true;
        }
    }

    public void UpdatePosititon()
    {
        Vector3 position = new Vector3(_moveDirection.x * _speed, _verticalVelocity, _moveDirection.z * _speed);
        _characterController.Move(position * Time.deltaTime);

        OnSpeedChanged?.Invoke(Mathf.Sqrt(Mathf.Pow(position.x,2) + Mathf.Pow(position.z,2)));
        OnForceChanged?.Invoke(position);
        Debug.DrawRay(transform.position, position, Color.red, 1f);
    }

    public void Move(float speed)
    {
        _speed = speed;
    }

    public bool GetIsGrounded()
    {
        bool isGround = _characterController.isGrounded;
        OnIsGround?.Invoke(isGround);
        return isGround;
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity = jumpForce;
        OnJumpAmountChanged.Invoke(jumpForce);
    }

    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void SetGravity(float gravity)
    {
        _gravity = gravity;
        OnGravityAmountChanged?.Invoke(_gravity);
    }

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        Debug.DrawRay(transform.position, direction * 5f, Color.white, 1f);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion q = new Quaternion(0f, targetRotation.y, 0f, targetRotation.w);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
    }

    public void SetDrag(float drag)
    {
    }

    private void HandleGravity()
    {
        _verticalVelocity -= _gravity;
        OnGravityCurrentChanged?.Invoke(_verticalVelocity);

        if (GetIsGrounded())
        {
            if (_verticalVelocity < 0f)
            {
                _verticalVelocity = -_gravity;
            }
        }
    }
}
