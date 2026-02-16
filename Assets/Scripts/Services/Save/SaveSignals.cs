using UnityEngine;

namespace Assets.Scripts.Services.Save
{
    public class GameSavedSignal { }
    public class GameLoadedSignal { }

    public class SettingsSavedSignal { }
    public class SettingsLoadedSignal { }


    public class CoinCollectedSignal
    {
        public string CoinId { get; }
        public int Value { get; }

        public CoinCollectedSignal(string coinId, int value = 1)
        {
            CoinId = coinId;
            Value = value;
        }
    }

    public class CheckpointActivatedSignal
    {
        public string CheckpointId { get; }
        public Vector3 Position { get; }

        public CheckpointActivatedSignal(string checkpointId, Vector3 position)
        {
            CheckpointId = checkpointId;
            Position = position;
        }
    }

    public class PlayerDiedSignal { }
    public class PlayerRespawnedSignal { }
}
