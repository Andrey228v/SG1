using Assets.Scripts.StateMachineUnit;
using Assets.Scripts.Units;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Debugs
{
    public class UnitDebug: MonoBehaviour
    {
        [SerializeField] private Unit _unit;
        [SerializeField] private PlayerStateMachine _playerStateMachine;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private TextMeshProUGUI _textUnitState;
        [SerializeField] private TextMeshProUGUI _textUnitGravity;
        [SerializeField] private TextMeshProUGUI _textVerticalVelocity;
        [SerializeField] private TextMeshProUGUI _textUnitJump;
        [SerializeField] private TextMeshProUGUI _textUnitSpeed;
        [SerializeField] private TextMeshProUGUI _textUnitForceX;
        [SerializeField] private TextMeshProUGUI _textUnitForceY;
        [SerializeField] private TextMeshProUGUI _textUnitForceZ;
        [SerializeField] private TextMeshProUGUI _textUnitIsGround;

        private void OnEnable()
        {
            _playerStateMachine.OnChangedState += SetTextUnitState;
            _playerView.OnGravityAmountChanged += SetGravityAmount;
            _playerView.OnGravityCurrentChanged += SetVerticalVelocity;
            _playerView.OnJumpAmountChanged += SetJumpAmount;
            _playerView.OnSpeedChanged += SetSpeed;
            _playerView.OnForceChanged += SetForce;
            _playerView.OnIsGround += SetIsGround;
        }

        private void OnDisable()
        {
            _playerStateMachine.OnChangedState -= SetTextUnitState;
            _playerView.OnGravityAmountChanged -= SetGravityAmount;
            _playerView.OnGravityCurrentChanged -= SetVerticalVelocity;
            _playerView.OnJumpAmountChanged -= SetJumpAmount;
            _playerView.OnSpeedChanged -= SetSpeed;
            _playerView.OnForceChanged -= SetForce;
            _playerView.OnIsGround -= SetIsGround;
        }

        private void SetTextUnitState(string state)
        {
            _textUnitState.text = state;
        }

        private void SetGravityAmount(float gravity)
        {
            gravity = Mathf.Round(gravity * 100f) / 100f;
            _textUnitGravity.text = gravity.ToString();
        }

        private void SetJumpAmount(float jump) 
        {
            jump = Mathf.Round(jump * 100f) / 100f;
            _textUnitJump.text = jump.ToString();
        }

        private void SetSpeed(float speed) 
        {
            _textUnitSpeed.text = speed.ToString();
        }

        private void SetForce(Vector3 force) 
        {
            _textUnitForceX.text = (Mathf.Round(force.x * 100f) / 100f).ToString();
            _textUnitForceY.text = (Mathf.Round(force.y * 100f) / 100f).ToString();
            _textUnitForceZ.text = (Mathf.Round(force.z * 100f) / 100f).ToString();
        }

        private void SetIsGround(bool isGround)
        {
            _textUnitIsGround.text = isGround.ToString();
        }

        private void SetVerticalVelocity(float gravity)
        {
            gravity = Mathf.Round(gravity * 100f) / 100f;
            _textVerticalVelocity.text = gravity.ToString();
        }
    }
}
