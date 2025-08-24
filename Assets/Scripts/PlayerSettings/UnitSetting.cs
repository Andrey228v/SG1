using UnityEngine;

namespace Assets.Scripts.PlayerSettings
{
    [CreateAssetMenu(fileName = "PlayerType", menuName = "ScriptableObjects/PlayerType")]
    public class UnitSetting : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _runSpeed = 75f;
        [SerializeField] private float _rotateSpeed = 500f;

        [Header("Jump")]
        [SerializeField] private float _jumpForce = 5f;

        [Header("Drags")]
        [SerializeField] private float _groundDragMovement = 5f;
        [SerializeField] private float _groundDragStay = 200f;

        [Header("Gravity Control")]
        [SerializeField] private float _fallMultiplier = 2.5f;
        [SerializeField] private float _lowJumpMultiplier = 2f;

        [Header("Slope")]
        [SerializeField] private float _maxSlopeAngle = 75f;
        [SerializeField] private float _slideSpeed = 150f;

    }
}
