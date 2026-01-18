using Assets.Scripts.Units;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class Checkpoint : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [field: SerializeField] public string CheckpointId { get; private set; } // А нужно ли это ??.....
        [SerializeField] private bool IsActivated = false;
        [SerializeField] private ParticleSystem _activationEffect;
        [SerializeField] private Light _activationLight;


        private void Awake()
        {
            // Генерируем уникальный ID если не установлен
            if (string.IsNullOrEmpty(CheckpointId))
            {
                CheckpointId = $"CheckPoint_{System.Guid.NewGuid().ToString().Substring(0, 8)}";
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated) return;

            if (other.GetComponent<Unit>() == false)
            {
                return;
            }

            Activate();
        }

        private void Activate()
        {
            Debug.Log("ACTIVATE SAVE");

            IsActivated = true;

            // Визуальные эффекты
            if (_activationEffect != null) _activationEffect.Play();
            if (_activationLight != null) _activationLight.color = Color.green;

            // Отправляем сигнал
            _signalBus.Fire(new CheckpointActivatedSignal(CheckpointId, transform.position));

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            IsActivated = false;
            if (_activationLight != null) _activationLight.color = Color.red;
            gameObject.SetActive(true);
        }

        public void Save()
        {

        }

        public void Load(Checkpoint loadData)
        {
            CheckpointId = loadData.CheckpointId;
            IsActivated = loadData.IsActivated;

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
