using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.Services.Audio
{
    [RequireComponent(typeof(Slider))]
    public class SliderVolumeMusic : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixerGroup;
        [SerializeField] private string _audioMixerName;

        private Slider _volumeSlider;
        private int coefficient = 20;
        private float _startVolume = 0.5f;

        private void Awake()
        {
            _volumeSlider = GetComponent<Slider>();
            _volumeSlider.onValueChanged.AddListener(ChangeValue);
        }

        private void Start()
        {
            _volumeSlider.value = _startVolume;
            ChangeValue(_startVolume);
        }

        private void OnDestroy()
        {
            _volumeSlider.onValueChanged.RemoveListener(ChangeValue);
        }

        private void ChangeValue(float volume)
        {
            _audioMixerGroup.audioMixer.SetFloat(_audioMixerName, Mathf.Log10(volume) * coefficient);
        }
    }
}
