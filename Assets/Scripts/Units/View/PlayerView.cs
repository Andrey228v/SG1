using Assets.Scripts.Utilites;
using ECM2;
using System;
using UnityEngine;
using Zenject;

public class PlayerView : ITickable, IPause
{
    public Character CharacterView { get; private set; }

    private Vector3 _moveDirection = Vector3.zero;
    private float _rotateSpeed;
    private Vector3 _currentMovment;
    private IAudioService _audioService;

    public event Action<Vector3> OnDirectionChanged;
    public event Action<Vector3> OnForceChanged;
    public event Action<bool> OnIsGround;
    public event Action<bool> OnIsFall;
    public event Action<bool> OnIsJumping;

    public bool IsPause = false;

    public PlayerView(Character character, IAudioService audioService)
    {
        CharacterView = character;
        _audioService = audioService;
    }

    public void Tick()
    {
        if (IsPause == false)
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
    }

    public void UpdatePosititon()
    {
        CharacterView.SetMovementDirection(_moveDirection);
    }

    public void Jump()
    {
        OnIsJumping?.Invoke(true);
        _audioService.PlaySound(SoundType.PlayerJump);
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

    public void Pause()
    {
        _moveDirection = Vector3.zero;
        CharacterView.Pause(true);
        IsPause = true;
    }

    public void Continue()
    {
        _moveDirection = Vector3.zero;
        CharacterView.Pause(false);
        IsPause = false;
    }
}
