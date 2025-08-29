using UnityEngine;

namespace Assets.Scripts.PlayerSettings
{
    [CreateAssetMenu(fileName = "UnitSetting", menuName = "ScriptableObjects/UnitSetting")]
    public class UnitSetting : ScriptableObject
    {
        [Header("Movement Settings")]
        [field: SerializeField] public float StaySpeed { get; private set; }
        [field: SerializeField] public float RunSpeed { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }

        [Header("Jump")]
        [field: SerializeField] public float JumpForce { get; private set; }

        [Header("Drags")]
        [field: SerializeField] public float GroundDragMovement { get; private set; }
        [field: SerializeField] public float GroundDragStay { get; private set; }

        [Header("Gravity Control")]
        [field: SerializeField] public float FallMultiplier { get; private set; }
        [field: SerializeField] public float LowJumpMultiplier { get; private set; }

        [Header("Slope")]
        [field: SerializeField] public float MaxSlopeAngle { get; private set; }
        [field: SerializeField] public float SlideSpeed { get; private set; }

    }
}
