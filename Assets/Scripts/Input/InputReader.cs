using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInput;

[CreateAssetMenu(fileName = "InputReader", menuName = "ScriptableObjects/InputReader")]
public class InputReader : ScriptableObject, IPlayerActions
{
    private PlayerInput _playerInput;

    //public Vector3 DirectionMove => _playerInput.Player.Move.ReadValue<Vector2>();

    public Vector3 DirectionMove {  get; private set; }

    public event UnityAction<Vector2> OnMoved;
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
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        OnLooked?.Invoke(context.ReadValue<Vector2>(), IsUseMouse(context));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        DirectionMove = _playerInput.Player.Move.ReadValue<Vector2>();

        OnMoved?.Invoke(context.ReadValue<Vector2>());

        //if(DirectionMove.magnitude > 0)
        //{
        //    OnMoved?.Invoke(context.ReadValue<Vector2>());
        //}
        //else
        //{
        //    OnMoveStoped?.Invoke(context.ReadValue<Vector2>());
        //}
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
