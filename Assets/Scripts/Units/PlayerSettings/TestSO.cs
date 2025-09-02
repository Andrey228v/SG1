using UnityEngine;

namespace Assets.Scripts.Units.PlayerSettings
{
    [CreateAssetMenu(fileName = "TestSO", menuName = "ScriptableObjects/TestSO")]
    public class TestSO : ScriptableObject
    {
        [Header("Basic Gravity")]
        [field: SerializeField] public float _baseGravity = 1;
        [field: SerializeField] public float _maxFallSpeed = 2;
        [field: SerializeField] public float _gravityMultiplier = 3;


        
    }
}
