using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.Services.Audio
{
    [CreateAssetMenu(fileName = "AudioSettings", menuName = "Game/Audio Settings")]
    public class AudioSettingsSO : ScriptableObject
    {
        [Header("Audio Mixers")]
        public AudioMixerGroup musicMixerGroup;
        public AudioMixerGroup sfxMixerGroup;
        public AudioMixerGroup uiMixerGroup;

        [Header("Music Tracks")]
        public AudioClip menuMusic;
        public AudioClip gameMusic;

        [Header("UI Sounds")]
        public AudioClip buttonClickSound;
        public AudioClip buttonHoverSound;
        public AudioClip menuOpenSound;
        public AudioClip menuCloseSound;

        [Header("Game Sounds")]
        public AudioClip playerJumpSound;
        public AudioClip playerLandSound;
        public AudioClip playerDamageSound;
        public AudioClip playerDeathSound;
        public AudioClip collectItemSound;

        [Header("Additional Sounds")]
        public AudioClip pauseSound;
        public AudioClip unpauseSound;
        public AudioClip gameOverSound;
        public AudioClip victorySound;

        [Header("Settings")]
        [Range(0f, 1f)] public float defaultMusicVolume = 0.5f;
        [Range(0f, 1f)] public float defaultSFXVolume = 0.8f;
        [Range(0f, 1f)] public float defaultUIVolume = 0.7f;
    }
}
