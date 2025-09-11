using Assets.Scripts;
using Assets.Scripts.DetectorProperties.GroundCheckerStrategy;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    private CharacterController _characterController;

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed;

    private bool _isGround;
    private float _speed;
    private float _gravity;
    private float _verticalVelocity;
    private bool _isFall;
    private Vector3 _currentMovment;
    private bool _isJumping;

    public event Action<bool> OnMovment;
    public event Action<bool> OnGravity;

    public event Action<Vector3> OnDirectionChanged;
    public event Action<float> OnGravityAmountChanged;
    public event Action<float> OnGravityCurrentChanged;
    public event Action<float> OnJumpAmountChanged;
    public event Action<float> OnSpeedChanged;
    public event Action<Vector3> OnForceChanged;
    public event Action<bool> OnIsGround;
    public event Action<bool> OnIsFall;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        _isJumping = false;
    }

    private void Update()
    {
        if (_moveDirection.magnitude > 0)
        {
            Rotate(_moveDirection, _rotateSpeed);
        }

        UpdatePosititon();
        
        OnForceChanged?.Invoke(_currentMovment);
        _characterController.Move(_currentMovment * Time.deltaTime);

        HandleGravity();
    }

    public void UpdatePosititon()
    {
        _currentMovment.x = _moveDirection.x * _speed;
        _currentMovment.y = _verticalVelocity;
        _currentMovment.z = _moveDirection.z * _speed;

        OnSpeedChanged?.Invoke(Mathf.Sqrt(Mathf.Pow(_currentMovment.x,2) + Mathf.Pow(_currentMovment.z,2)));
    }

    public void Move(float speed)
    {
        _speed = speed;
    }

    public bool GetIsGrounded()
    {
        _isGround = _characterController.isGrounded;
        OnIsGround?.Invoke(_isGround);
        return _isGround;
    }

    public void SetIsGround(bool isGround)
    {
        //_isGround = isGround;
    }

    public void Jump(float jumpForce)
    {
        _verticalVelocity = jumpForce;
        OnJumpAmountChanged.Invoke(jumpForce);
    }

    public void SetMoveDirection(Vector3 direction)
    {
        OnDirectionChanged?.Invoke(direction);
        _moveDirection = direction;
    }

    public void SetGravity(float gravity)
    {
        _gravity = gravity;
        OnGravityAmountChanged?.Invoke(_gravity);
    }

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        Debug.DrawRay(transform.position, direction * 3f, Color.white, 1f);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Quaternion q = new Quaternion(0f, targetRotation.y, 0f, targetRotation.w);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotateSpeed * Time.deltaTime);
    }

    public void SetRotate(float rotateSpeed)
    {
        _rotateSpeed = rotateSpeed;
    }

    private void HandleGravity()
    {
        if (GetIsGrounded())
        {
            _verticalVelocity = _gravity;
        }
        else
        {
            _verticalVelocity += _gravity * Time.deltaTime;
        }


        if (_verticalVelocity < 0f && GetIsGrounded() == false)
        {
            _isFall = true;
        }
        else
        {
            _isFall = false;
        }

        OnIsFall?.Invoke(_isFall);

        OnGravityCurrentChanged?.Invoke(_verticalVelocity);
    }

    public bool GetIsFall()
    {
        return _isFall;
    }
}
