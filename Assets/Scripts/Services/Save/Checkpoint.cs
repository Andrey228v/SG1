using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Save
{
    public class Checkpoint : MonoBehaviour
    {
        [Inject] private readonly SignalBus _signalBus;

        [SerializeField] private string _checkpointId;
        [SerializeField] private ParticleSystem _activationEffect;
        [SerializeField] private Light _activationLight;

        private bool _isActivated;

        private void OnTriggerEnter(Collider other)
        {
            if (_isActivated) return;
            if (!other.CompareTag("Player")) return;

            Activate();
        }

        private void Activate()
        {
            _isActivated = true;

            // Визуальные эффекты
            if (_activationEffect != null) _activationEffect.Play();
            if (_activationLight != null) _activationLight.color = Color.green;

            // Отправляем сигнал
            _signalBus.Fire(new CheckpointActivatedSignal(_checkpointId, transform.position));
        }

        public void Deactivate()
        {
            _isActivated = false;
            if (_activationLight != null) _activationLight.color = Color.red;
        }
    }
}
