using ECM2;
using System;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Character CharacterView { get; private set; }

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed;
    private Vector3 _currentMovment;

    public event Action<Vector3> OnDirectionChanged;
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

    private void HandleGravity()
    {
        OnIsFall?.Invoke(GetIsFall());
    }

    public bool GetIsFall()
    {
        return CharacterView.IsFalling() && CharacterView.velocity.y < 0;
    }
}
