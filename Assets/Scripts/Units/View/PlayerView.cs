using Assets.Scripts;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(GroundChecker))]
public class PlayerView : MonoBehaviour
{
    //[SerializeField] private float _speed  = 75.0f;
    //[SerializeField] private float _jumpForce = 5f;
    //[SerializeField] private float _rotateSpeed = 500f;

    //[Header("Drags")]
    //[SerializeField] private float _groundDragMovement = 5f;
    //[SerializeField] private float _groundDragStay = 200f;

    //[Header("Gravity Control")]
    //[SerializeField] private float _fallMultiplier = 2.5f;
    //[SerializeField] private float _lowJumpMultiplier = 2f;

    private Rigidbody _rb;
    
    private bool _isMovement;
    private Vector3 _moveDirection = Vector3.zero;

    private bool _isEventCheckMovmentSent = false;

    public event Action<bool> OnMovment;
    public event Action<bool> OnGravity;

    public bool IsGrounded { get; private set; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        IsGrounded = true;
    }

    public void Move(float speed, float rotateSpeed)
    {
        _rb.AddForce(_moveDirection * speed, ForceMode.Force);

        if (_moveDirection.magnitude > 0)
        {
            Rotate(_moveDirection, rotateSpeed);
            _isMovement = true;
        }
    }


    public void Jump(float jumpForce)
    {
        if (IsGrounded) 
        {
            //_rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z); // Не знаю нужно ли - надо тестить. Сброс ускорения перед прыжком.
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

    }

    public void SetMoveDirection(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public void Rotate(Vector3 direction, float rotateSpeed)
    {
        Debug.DrawRay(transform.position, direction * 5f, Color.white, 1f);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void SetIsGround(bool isGround)
    {
        IsGrounded = isGround;
    }

    public float GetSpeed()
    {
        return _rb.linearVelocity.magnitude;
    }

    public void SetDrag(float drag)
    {
        _rb.linearDamping = drag;
    }

    public Vector3 GetVelosity()
    {        
        return _rb.linearVelocity;
    }

    //public void CheckDrag(float groundDragStay, float groundDragMovment)
    //{
    //    if (IsGrounded && _isMovement)
    //    {
    //        _rb.linearDamping = groundDragMovment;
    //    }
    //    else if (IsGrounded && _isMovement == false)
    //    {
    //        _rb.linearDamping = groundDragStay;
    //    }
    //    else
    //    {
    //        _rb.linearDamping = 0;
    //    }
    //}

    //private void CheckRigidbodyMovement()
    //{
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

    //}

    //private void SpeedControl()
    //{
    //    Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);

    //    if(flatVel.magnitude > _speed)
    //    {
    //        Vector3 limitedVel = flatVel.normalized * _speed;
    //        _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
    //    }
    //}

    //private void ApplyBetterGravity()
    //{
    //    //Debug.Log($"_rb.linearVelocity.y: {_rb.linearVelocity.y}");

    //    if (_rb.linearVelocity.y < 0)
    //    {
    //        _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
    //        //OnGravity?.Invoke(true);
    //    }
    //    else if (_rb.linearVelocity.y > 0)
    //    {
    //        _rb.linearVelocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
    //        //OnGravity?.Invoke(false);
    //    }
    //}
}
