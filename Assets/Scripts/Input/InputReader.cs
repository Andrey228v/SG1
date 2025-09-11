using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInput;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private PlayerInput _playerInput;
    private bool _onDownJump = false;
    //public Vector3 DirectionMove => _playerInput.Player.Move.ReadValue<Vector2>();

    //public Vector3 DirectionMove {  get; private set; }

    public event UnityAction<Vector2> OnDirectionMoveChandged;
    public event Action OnJumped;
    public event Action<bool> OnJumpButtonDown;
    public event Action OnJumpedCanceled;
    public event Action OnMoved;
    public event Action OnStoped;
    public event UnityAction<Vector2> OnMoveStoped;
    public event UnityAction<Vector2, bool> OnLooked;
    public event UnityAction EnableMouseControlCamera;
    public event UnityAction DisableMouseControlCamera;

    private void OnEnable()
    {
        if(_playerInput == null)
        {
            _playerInput = new PlayerInput();
            _playerInput.Player.SetCallbacks(this);
            OnJumpButtonDown?.Invoke(true);
        }

        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnDestroy()
    {
        _playerInput.Dispose();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //OnJumped?.Invoke();
            OnJumpButtonDown?.Invoke(true);
        }
        else if (context.canceled)
        {
            OnJumpButtonDown?.Invoke(false);
        }


        //if (context.started == true)
        //{
        //    OnJumped?.Invoke();
        //    OnJumpButtonDown?.Invoke(true);


            //    //if (context.ReadValueAsButton() == true)
            //    //{
            //    //    OnJumpButtonDown?.Invoke(true);
            //    //}
            //}
            ////else if (context.performed)
            ////{
            ////    Debug.Log(context.ReadValueAsButton());
            ////    OnJumpButtonDown?.Invoke(false);
            ////    //OnJumpButtonDown?.Invoke(false);
            ////    //if (context.ReadValueAsButton() == true)
            ////    //{

            ////    //}
            ////}
            //else if (context.canceled == true)
            //{
            //    Debug.Log(context.ReadValueAsButton());

            //    OnJumpButtonDown?.Invoke(false);
            //    OnJumpedCanceled?.Invoke();
            //}
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLooked?.Invoke(context.ReadValue<Vector2>(), IsUseMouse(context));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.started == true)
        {
            OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
            OnMoved?.Invoke();
        }
        else if (context.performed == true)
        {
            OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
        }
        else if (context.canceled == true)
        {
            OnDirectionMoveChandged?.Invoke(context.ReadValue<Vector2>());
            OnStoped?.Invoke();
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        
    }

    private bool IsUseMouse(InputAction.CallbackContext context)
    {
        return context.control.device.name == "Mouse";
    }

    public void OnMouseControllCamera(InputAction.CallbackContext context)
    {
        
    }
}
