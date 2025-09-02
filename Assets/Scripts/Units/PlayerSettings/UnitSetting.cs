using Assets.Scripts.DetectorProperties.GroundCheckerStrategy;
using UnityEngine;

namespace Assets.Scripts.PlayerSettings
{
    [CreateAssetMenu(fileName = "UnitSetting", menuName = "ScriptableObjects/UnitSetting")]
    public class UnitSetting : ScriptableObject
    {
        [Header("Movement Settings")]
        [SerializeField] private float _staySpeed = 0;
        [SerializeField] private float _runSpeed = 75;
        [SerializeField] private float _rotateSpeed = 500;

        [Header("GroundChecker")]
        [SerializeField] private AGroundCheckerStrategy _aGroundChecker;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 20;
        [SerializeField] private float _jumpSpeedMove = 20;

        [Header("Drags")]
        [SerializeField] private float _groundDragMovement = 5;
        [SerializeField] private float _groundDragStay = 200;
        [SerializeField] private float _dragJump = 0;
        [SerializeField] private float _dragFall = 0;

        [Header("Gravity Control")]
        [SerializeField] private float _fallMultiplier = 2.5f;
        [SerializeField] private float _lowJumpMultiplier = 1.5f;

        [Header("Slope")]
        [SerializeField] private float _maxSlopeAngle = 75;
        [SerializeField] private float _slideSpeed = 150;

        public float StaySpeed => _staySpeed;
        public float RunSpeed => _runSpeed;
        public float RotateSpeed => _rotateSpeed;
        public AGroundCheckerStrategy AGroundChecker => _aGroundChecker;
        public float JumpForce => _jumpForce;
        public float JumpSpeedMove => _jumpSpeedMove;
        public float GroundDragMovement => _groundDragMovement;
        public float GroundDragStay => _groundDragStay;
        public float DragJump => _dragJump;
        public float DragFall => _dragFall;
        public float FallMultiplier => _fallMultiplier;
        public float LowJumpMultiplier => _lowJumpMultiplier;
        public float MaxSlopeAngle => _maxSlopeAngle;
        public float SlideSpeed => _slideSpeed;

    }
}
