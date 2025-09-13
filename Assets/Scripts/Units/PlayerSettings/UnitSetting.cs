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
        [SerializeField] private float _deleyTimeFall = 0.5f; 
        [SerializeField] private float _cayoteTime = 0.5f;
        [SerializeField] private float _maxJumpTime = 0.5f;
        [SerializeField] private float _maxJumpHeight = 10f;
        [SerializeField] private int _countJump = 2;


        [Header("Drags")]
        [SerializeField] private float _groundDragMovement = 5;
        [SerializeField] private float _groundDragStay = 200;
        [SerializeField] private float _dragJump = 0;
        [SerializeField] private float _dragFall = 0;

        [Header("Gravity Control")]
        [SerializeField] private float _gravity = 9.8f;
        [SerializeField] private float _gravityGround = 0.5f;
        [SerializeField] private float _fallMultiplier = 2.5f;
        [SerializeField] private float _lowJumpMultiplier = 1.5f;

        [Header("Slope")]
        [SerializeField] private float _maxSlopeAngle = 75;
        [SerializeField] private float _slideSpeed = 150;

        public float StaySpeed => _staySpeed;
        public float RunSpeed => _runSpeed;
        public float RotateSpeed => _rotateSpeed;
        public AGroundCheckerStrategy AGroundChecker => _aGroundChecker;
        public float DeleyTimeFall => _deleyTimeFall;
        public float CayoteTime => _cayoteTime;
        public float MaxJumpTime => _maxJumpTime;
        public float MaxJumpHeight => _maxJumpHeight;
        public int CountJump => _countJump;
        public float GroundDragMovement => _groundDragMovement;
        public float GroundDragStay => _groundDragStay;
        public float DragJump => _dragJump;
        public float DragFall => _dragFall;
        public float Gravity => _gravity;
        public float GravityGround => _gravityGround;
        public float FallMultiplier => _fallMultiplier;
        public float LowJumpMultiplier => _lowJumpMultiplier;
        public float MaxSlopeAngle => _maxSlopeAngle;
        public float SlideSpeed => _slideSpeed;

    }
}
