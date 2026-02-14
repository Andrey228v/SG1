using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Units
{
    public class PlayerProgress
    {
        public int CurrentLevel = 1;
        public List<int> CompletedLevels = new List<int>();
        public int TotalScore = 0;
        public List<string> UnlockedRewards = new List<string>();
        public StartGameSettings Settings = new StartGameSettings();
        public DateTime LastSaveTime = DateTime.Now;

        // Метод для проверки завершенности уровня
        public bool IsLevelCompleted(int levelId)
        {
            return CompletedLevels.Contains(levelId);
        }

        // Метод завершения уровня
        public void CompleteLevel(int levelId, int score, List<string> rewards = null)
        {
            if (!CompletedLevels.Contains(levelId))
            {
                CompletedLevels.Add(levelId);
            }

            TotalScore += score;

            if (rewards != null)
            {
                foreach (var reward in rewards)
                {
                    if (!UnlockedRewards.Contains(reward))
                    {
                        UnlockedRewards.Add(reward);
                    }
                }
            }

            LastSaveTime = DateTime.Now;
        }
    }

    [Serializable]
    public class StartGameSettings
    {
        public float MasterVolume = 0.8f;
        public float MusicVolume = 0.6f;
        public float SFXVolume = 0.7f;
        public string Language = "Russian";
        public int QualityLevel = 1;
        public bool IsFullscreen = true;
        public Resolution? TargetResolution = null;
        public bool VSyncEnabled = true;
        public int TargetFPS = 30;
    }
}
