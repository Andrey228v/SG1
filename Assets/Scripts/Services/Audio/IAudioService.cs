using System;

namespace Assets.Scripts.Utilites
{
    public interface IAudioService
    {
        void PlayMusic(SoundType type, bool loop = true);
        void PlaySound(SoundType type);
        void PlaySoundAtPosition(SoundType type, UnityEngine.Vector3 position);
        void PlayUISound(SoundType type);

        void StopMusic();
        void PauseMusic();
        void ResumeMusic();

        void SetMusicVolume(float volume);
        void SetSFXVolume(float volume);
        void SetUIVolume(float volume);

        float GetMusicVolume();
        float GetSFXVolume();
        float GetUIVolume();

        void MuteAll(bool mute);
        void MuteMusic(bool mute);
        void MuteSFX(bool mute);
        void MuteUI(bool mute);

        event Action<float> OnMusicVolumeChanged;
        event Action<float> OnSFXVolumeChanged;
        event Action<float> OnUIVolumeChanged;
    }
}
