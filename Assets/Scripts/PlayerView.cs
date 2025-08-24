using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(GroundChecker))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private float _speed  = 75.0f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _rotateSpeed = 500f;

    [Header("Drags")]
    [SerializeField] private float _groundDragMovement = 5f;
    [SerializeField] private float _groundDragStay = 200f;

    [Header("Gravity Control")]
    [SerializeField] private float _fallMultiplier = 2.5f;
    [SerializeField] private float _lowJumpMultiplier = 2f;

    private Rigidbody _rb;
    private bool _isGrounded;
    private bool _isMovement;
    private Vector3 _moveDirection = Vector3.zero;

    private bool _isEventCheckMovmentSent = false;

    public event Action<bool> OnMovment;
    public event Action<bool> OnGravity;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void OnEnable()
    {

    }

    private void Update()
    {
        //ApplyBetterGravity();
        //CheckRigidbodyMovement();

        CheckDrag();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void OnDestroy()
    {
        
    }

    public void Move()
    {
        //transform.position = Vector3.MoveTowards(transform.position, transform.position + _moveDirection, _speed * Time.deltaTime);

        if (_isGrounded && _moveDirection.magnitude > 0)
        {
            _rb.AddForce(_moveDirection * _speed, ForceMode.Force);

            if (_moveDirection.magnitude > 0)
            {
                Rotate(_moveDirection);
                _isMovement = true;
            }
        }
        else if(_isGrounded && _moveDirection.magnitude == 0)
        {
            _isMovement = false;
            //_rb.AddForce(_moveDirection.normalized * _speed *  _speed / _lowJumpMultiplier, ForceMode.Force);
        }

    }


    public void Jump()
    {
        if (_isGrounded) 
        {
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z); // Не знаю нужно ли - надо тестить. Сброс ускорения перед прыжком.
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
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
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }

    public void SetIsGround(bool isGround)
    {
        _isGrounded = isGround;


    }

    public void CheckDrag()
    {
        if (_isGrounded && _isMovement)
        {
            _rb.linearDamping = _groundDragMovement;
        }
        else if (_isGrounded && _isMovement == false)
        {
            _rb.linearDamping = _groundDragStay;
        }
        else
        {
            _rb.linearDamping = 0;
        }
    }

    private void CheckRigidbodyMovement()
    {
        //Debug.Log($"_rb.velocity:{_rb.linearVelocity}");
        //Debug.Log($"_rb.velocity:{_rb.linearVelocity.y}");
        //Debug.Log($"_rb.velocity:{_rb.linearVelocity.x}");

        //if(_moveDirection.magnitude > 0 && _rb.linearVelocity.magnitude > 0)
        //{
        //    Debug.Log("NOT Static");
        //    OnMovment?.Invoke(false);
        //}
        //else
        //{
        //    Debug.Log("Move");
        //    OnMovment?.Invoke(true);
        //}

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

        if(flatVel.magnitude > _speed)
        {
            Vector3 limitedVel = flatVel.normalized * _speed;
            _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void ApplyBetterGravity()
    {
        //Debug.Log($"_rb.linearVelocity.y: {_rb.linearVelocity.y}");

        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            //OnGravity?.Invoke(true);
        }
        else if (_rb.linearVelocity.y > 0)
        {
            _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            //OnGravity?.Invoke(false);
        }
    }
}
