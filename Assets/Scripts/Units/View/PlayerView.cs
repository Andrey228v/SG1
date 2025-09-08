using Assets.Scripts;
using Assets.Scripts.DetectorProperties.GroundCheckerStrategy;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

//[RequireComponent(typeof(Rigidbody))]
public class PlayerView : MonoBehaviour
{
    //private Rigidbody _rb;
    private CharacterController _characterController;

    private bool _isMovement;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed = 500f;

    private bool _isGround;
    private bool _isEventCheckMovmentSent = false;
    private float _jumpForce = 0;
    private float _speed = 0;
    private float _gravity = 0;
    private float _verticalVelocity = 0;
    private bool _isFall;
    private Vector3 _currentMovment;

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
        if (_moveDirection.magnitude > 0)
        {
            Rotate(_moveDirection, _rotateSpeed);
            _isMovement = true;
        }

        UpdatePosititon();
        
        OnForceChanged?.Invoke(_currentMovment);
        _characterController.Move(_currentMovment * Time.deltaTime);

        HandleGravity();
        //HandleJump();
    }

    public void UpdatePosititon()
    {
        //Vector3 position = new Vector3(_moveDirection.x * _speed, _moveDirection.y * _speed, _moveDirection.z * _speed);
        //position = position * Time.deltaTime;

        //Vector3 postionGravity = new Vector3(position.x, position.y + _verticalVelocity, position.z);
        //_moveDirection.y * _speed + _verticalVelocity
        //_currentMovment = new Vector3(_moveDirection.x * _speed, _moveDirection.y * _speed, _moveDirection.z * _speed);

        _currentMovment.x = _moveDirection.x * _speed;
        _currentMovment.y = _verticalVelocity;
        _currentMovment.z = _moveDirection.z * _speed;

        OnSpeedChanged?.Invoke(Mathf.Sqrt(Mathf.Pow(_currentMovment.x,2) + Mathf.Pow(_currentMovment.z,2)));
        
        Debug.DrawRay(transform.position, _currentMovment, Color.red, 1f);
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

    private void HandleJump()
    {
        //_verticalVelocity = _jumpForce;
    }

    private void HandleGravity()
    {

        if (GetIsGrounded())
        {
            _verticalVelocity = _gravity; // тут надо исправить.
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

        //if (GetIsGrounded())
        //{
        //    if (_verticalVelocity < 0f)
        //    {
        //        _verticalVelocity = -_gravity;
        //    }
        //}

        OnGravityCurrentChanged?.Invoke(_verticalVelocity);
    }

    public bool GetIsFall()
    {
        return _isFall;
    }
}
