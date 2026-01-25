using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game/Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Уровни")]
        [SerializeField] private int _maxLevels = 10;
        [SerializeField] private int _firstLevelBuildIndex = 2;

        [Header("Награды")]
        [SerializeField] private int _baseRewardPerLevel = 100;
        [SerializeField] private int _perfectLevelBonus = 50;

        [Header("Системные")]
        [SerializeField] private bool _enableDebugLogs = true;
        [SerializeField] private float _autoSaveInterval = 300f; // 5 минут

        public int MaxLevels => _maxLevels;
        public int FirstLevelBuildIndex => _firstLevelBuildIndex;
        public int BaseRewardPerLevel => _baseRewardPerLevel;
        public int PerfectLevelBonus => _perfectLevelBonus;
        public bool EnableDebugLogs => _enableDebugLogs;
        public float AutoSaveInterval => _autoSaveInterval;
    }
}
