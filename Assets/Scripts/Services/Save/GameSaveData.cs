using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Services.Save
{
    [Serializable]
    public class GameSaveData
    {
        public string SaveVersion = "1.0";
        public DateTime SaveTime;

        public PlayerSaveData PlayerData = new PlayerSaveData();
        public CheckpointSaveData CheckpointData = new CheckpointSaveData();

        // Данные уровня
        //public LevelSaveData LevelData = new LevelSaveData();

        // Прогресс игры
        //public GameProgressData ProgressData = new GameProgressData();

        public void Restart()
        {
            SaveTime = DateTime.Now;
            PlayerData = new PlayerSaveData();
            CheckpointData = new CheckpointSaveData();
        }
    }

    [Serializable]
    public class PlayerSaveData
    {
        public Vector3Serializable Position;
        public Vector3Serializable Rotation;
        //public int Health = 100;
        //public int CoinsCollected;
        //public string CurrentCheckpointId;
        //public List<string> CollectedCoins = new List<string>();
    }

    [Serializable]
    public class LevelSaveData
    {
        //public string LevelName;
        //public List<CoinSaveData> Coins = new List<CoinSaveData>();
        //public List<CheckpointSaveData> Checkpoints = new List<CheckpointSaveData>();
    }

    [Serializable]
    public class CoinSaveData
    {
        //public string CoinId;
        //public bool IsCollected;
        //public Vector3Serializable Position;
    }

    [Serializable]
    public class CheckpointSaveData
    {
        //Сохранение текущего чекпоинта.
        public string CheckpointId;
        public Vector3Serializable Position;
        public bool IsActivated;
        public Dictionary<string, Checkpoint> CheckpointsDictionary = new Dictionary<string, Checkpoint>();
    }

    [Serializable]
    public class GameProgressData
    {
        //public int TotalCoins;
        //public int TotalDeaths;
        //public float PlayTime;
        //public List<string> CompletedLevels = new List<string>();
    }

    [Serializable]
    public struct Vector3Serializable
    {
        public float x;
        public float y;
        public float z;

        public Vector3Serializable(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }
}
