using Assets.Scripts.GameInstallers.Signals;
using Assets.Scripts.Utilites;
using ECM2;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class Checkpoint : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [field: SerializeField] public string CheckpointId { get; private set; } // А нужно ли это ??.....
        [SerializeField] private bool IsActivated = false;


        private void Awake()
        {
            IsActivated = false;

            // Генерируем уникальный ID если не установлен
            if (string.IsNullOrEmpty(CheckpointId))
            {
                CheckpointId = $"CheckPoint_{System.Guid.NewGuid().ToString().Substring(0, 8)}";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated)
            {
                return;
            }

            if (other.GetComponent<Player>() == false)
            {
                return;
            }

            Activate();
        }

        private void Activate()
        {
            IsActivated = true;
            gameObject.SetActive(false);
            _signalBus.Fire(new OnCheckPointActivated());
        }

        public void Deactivate()
        {
            IsActivated = false;
            gameObject.SetActive(true);
        }

        public CheckpointSaveData Save()
        {
            CheckpointSaveData checkpointSaveData = new CheckpointSaveData();

            try
            {
                checkpointSaveData.checkpointId = CheckpointId;
                checkpointSaveData.isActivated = IsActivated;
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"❌ Ошибка сохранения чекпоинта {CheckpointId}: {ex.Message}", this);
            }

            return checkpointSaveData;
        }

        public void Load(CheckpointSaveData checkpointSaveData)
        {
            CheckpointId = checkpointSaveData.checkpointId;
            IsActivated = checkpointSaveData.isActivated;
            
            if (IsActivated)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }
    }
}
