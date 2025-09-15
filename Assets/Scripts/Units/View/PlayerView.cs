using ECM2;
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Character CharacterView { get; private set; }

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed;

    private bool _isGround;
    private float _gravity;
    private Vector3 _currentMovment;
    private int _jumpCountCurrent = 0;

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
    public event Action<bool> OnIsJumping;

    private void Awake()
    {
        CharacterView = GetComponent<Character>();
    }

    private void FixedUpdate()
    {
        if (_moveDirection.magnitude > 0)
        {
            CharacterView.RotateTowards(_moveDirection, _rotateSpeed);
        }

        UpdatePosititon();
        
        OnForceChanged?.Invoke(_currentMovment);
        OnIsGround?.Invoke(CharacterView.IsGrounded());

        HandleGravity();
    }

    public void UpdatePosititon()
    {
        CharacterView.SetMovementDirection(_moveDirection);
    }

    public bool GetIsGrounded()
    {
        _isGround = CharacterView.IsGrounded();

        OnIsGround?.Invoke(_isGround);
        return _isGround;
    }

    public void Jump()
    {
        OnIsJumping?.Invoke(true);
        CharacterView.Jump();
    }

    public void StopJump()
    {
        OnIsJumping?.Invoke(false);
        CharacterView.StopJumping();
    }

    public void SetMoveDirection(Vector3 direction)
    {
        OnDirectionChanged?.Invoke(direction);
        _moveDirection = direction;
    }

    public void SetGravity(float gravity)
    {
        OnGravityAmountChanged?.Invoke(_gravity);
    }

    private void HandleGravity()
    {
        OnIsFall?.Invoke(GetIsFall());
    }

    public bool GetIsFall()
    {
        return CharacterView.IsFalling() && CharacterView.velocity.y < 0;
    }

    public int GetJumpCount()
    {
        return _jumpCountCurrent;
    }

    public void AddJumpCount()
    {
        _jumpCountCurrent++;
    }

    public void ResetJumpCount()
    {
        _jumpCountCurrent = 0;
    }
}
