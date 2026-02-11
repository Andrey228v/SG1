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
        public PlayerSaveData playerSaveData = new PlayerSaveData();
        public CheckpointsSaveDataList checkpointsSaveData = new CheckpointsSaveDataList();

        public void RefreshSave()
        {

        }

    }

    [Serializable]
    public class PlayerSaveData
    {
        public Vector3Serializable Position;
        public Vector3Serializable Rotation;
    }

    [Serializable]
    public class CheckpointsSaveDataList
    {
        public List<CheckpointSaveData> checkpointsList = new List<CheckpointSaveData>();
    }

    [Serializable]
    public class CheckpointSaveData
    {
        public string checkpointId;
        public bool isActivated;
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
