using Assets.Scripts.Services.Audio;
using Assets.Scripts.Services.Save;
using Assets.Scripts.Utilites;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioService : IAudioService, IDisposable
{
    private readonly AudioSettingsSO _settings;
    private readonly AudioMixer _audioMixer;
    private readonly Dictionary<SoundType, AudioClip> _soundMap;

    private SaveLoadSettingsService _saveLoadSettingsService;
    private AudioSource _musicSource;
    private AudioSource _uiSource;
    //private List<AudioSource> _sfxSourcesPool;

    private float _musicVolume;
    private float _sfxVolume;
    private float _uiVolume;

    private bool _musicMuted;
    private bool _sfxMuted;
    private bool _uiMuted;

    private SoundType _currentMusicType;

    public event Action<float> OnMusicVolumeChanged;
    public event Action<float> OnSFXVolumeChanged;
    public event Action<float> OnUIVolumeChanged;

    public AudioService(AudioSettingsSO settings, AudioMixer audioMixer, SaveLoadSettingsService saveLoadSettingsService)
    {
        _settings = settings;
        _audioMixer = audioMixer;
        _soundMap = new Dictionary<SoundType, AudioClip>();
        _saveLoadSettingsService = saveLoadSettingsService;
        //_sfxSourcesPool = new List<AudioSource>();

        InitializeSoundMap();
        InitializeAudioSources();
        LoadVolumes();
    }

    public void Dispose()
    {
        //PlayerPrefs.Save();
    }

    private void InitializeSoundMap()
    {
        // Музыка
        _soundMap[SoundType.MenuMusic] = _settings.menuMusic;
        _soundMap[SoundType.GameMusic] = _settings.gameMusic;

        // UI звуки
        _soundMap[SoundType.ButtonClick] = _settings.buttonClickSound;
        _soundMap[SoundType.ButtonHover] = _settings.buttonHoverSound;
        _soundMap[SoundType.MenuOpen] = _settings.menuOpenSound;
        _soundMap[SoundType.MenuClose] = _settings.menuCloseSound;

        // Игровые звуки
        _soundMap[SoundType.PlayerJump] = _settings.playerJumpSound;
        _soundMap[SoundType.PlayerLand] = _settings.playerLandSound;
        _soundMap[SoundType.PlayerDeath] = _settings.playerDeathSound;
        _soundMap[SoundType.CollectItem] = _settings.collectItemSound;

        // Дополнительные
        _soundMap[SoundType.Pause] = _settings.pauseSound;
        _soundMap[SoundType.Unpause] = _settings.unpauseSound;
        _soundMap[SoundType.GameOver] = _settings.gameOverSound;
        _soundMap[SoundType.Victory] = _settings.victorySound;
    }

    private void InitializeAudioSources()
    {
        var audioManager = new GameObject("[AudioManager]");
        UnityEngine.Object.DontDestroyOnLoad(audioManager);

        // Music source
        _musicSource = audioManager.AddComponent<AudioSource>();
        _musicSource.loop = true;
        _musicSource.playOnAwake = false;
        _musicSource.outputAudioMixerGroup = _settings.musicMixerGroup;

        // UI source
        _uiSource = audioManager.AddComponent<AudioSource>();
        _uiSource.loop = false;
        _uiSource.playOnAwake = false;
        _uiSource.outputAudioMixerGroup = _settings.uiMixerGroup;


        // Create SFX sources pool
        //for (int i = 0; i < 10; i++)
        //{
        //    var sfxSource = audioManager.AddComponent<AudioSource>();
        //    sfxSource.loop = false;
        //    sfxSource.playOnAwake = false;
        //    sfxSource.outputAudioMixerGroup = _settings.sfxMixerGroup;
        //    _sfxSourcesPool.Add(sfxSource);
        //}
    }

    private void LoadVolumes()
    {
        if (_saveLoadSettingsService.HasSave() == false)
        {
            _musicVolume = _settings.defaultMusicVolume;
            _sfxVolume = _settings.defaultSFXVolume;
            _uiVolume = _settings.defaultUIVolume;
        }
        else
        {
            SettingsSaveData load = _saveLoadSettingsService.LoadSettings();
            _musicVolume = load.MusicVolume;
            _sfxVolume = load.SFXVilume;
            _uiVolume = load.UIVolume;
        }

        ApplyVolumes();
    }

    private void ApplyVolumes()
    {
        if (_audioMixer != null)
        {
            _audioMixer.SetFloat("MusicVolume", Mathf.Log10(_musicVolume) * 20);
            _audioMixer.SetFloat("SFXVolume", Mathf.Log10(_sfxVolume) * 20);
            _audioMixer.SetFloat("UIVolume", Mathf.Log10(_uiVolume) * 20);
        }
        else
        {
            //_musicSource.volume = _musicVolume;
            //_uiSource.volume = _uiVolume;

            //foreach (var source in _sfxSourcesPool)
            //{
            //    source.volume = _sfxVolume;
            //}
        }
    }

    //private AudioSource GetAvailableSFXSource()
    //{
    //    foreach (var source in _sfxSourcesPool)
    //    {
    //        if (!source.isPlaying)
    //            return source;
    //    }

    //    // Если все заняты, используем первый и останавливаем его
    //    var firstSource = _sfxSourcesPool[0];
    //    firstSource.Stop();
    //    return firstSource;
    //}

    public void PlayMusic(SoundType type, bool loop = true)
    {
        if (!_soundMap.ContainsKey(type))
        {
            Debug.LogWarning($"Music clip not found for type: {type}");
            return;
        }

        if (_currentMusicType == type && _musicSource.isPlaying && !_musicMuted)
            return;

        _musicSource.clip = _soundMap[type];
        _musicSource.loop = loop;

        if (!_musicMuted)
            _musicSource.Play();

        _currentMusicType = type;
    }

    public void PlaySound(SoundType type)
    {
        if (_sfxMuted) return;

        if (!_soundMap.ContainsKey(type))
        {
            Debug.LogWarning($"Sound clip not found for type: {type}");
            return;
        }

        //var source = GetAvailableSFXSource();
        //source.clip = _soundMap[type];
        //source.Play();
    }

    public void PlaySoundAtPosition(SoundType type, Vector3 position)
    {
        if (_sfxMuted) return;

        if (!_soundMap.ContainsKey(type))
        {
            Debug.LogWarning($"Sound clip not found for type: {type}");
            return;
        }

        AudioSource.PlayClipAtPoint(_soundMap[type], position, _sfxVolume);
    }

    public void PlayUISound(SoundType type)
    {
        if (_uiMuted) return;

        if (!_soundMap.ContainsKey(type))
        {
            Debug.LogWarning($"UI sound clip not found for type: {type}");
            return;
        }

        _uiSource.PlayOneShot(_soundMap[type]);
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PauseMusic()
    {
        _musicSource.Pause();
    }

    public void ResumeMusic()
    {
        if (!_musicMuted && _musicSource.clip != null)
            _musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        _musicVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("MusicVolume", _musicVolume);
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(_musicVolume) * 20);
        _musicSource.volume = _musicVolume;
        OnMusicVolumeChanged?.Invoke(_musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("SFXVolume", _sfxVolume);
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(_sfxVolume) * 20);

        //_sfxVolume.volume = _sfxVolume;

        OnSFXVolumeChanged?.Invoke(_sfxVolume);
    }

    public void SetUIVolume(float volume)
    {
        _uiVolume = Mathf.Clamp01(volume);
        PlayerPrefs.SetFloat("UIVolume", _uiVolume);
        _audioMixer.SetFloat("UIVolume", Mathf.Log10(_uiVolume) * 20);
        _uiSource.volume = _uiVolume;
        OnUIVolumeChanged?.Invoke(_uiVolume);
    }

    public float GetMusicVolume() => _musicVolume;
    public float GetSFXVolume() => _sfxVolume;
    public float GetUIVolume() => _uiVolume;

    public void MuteAll(bool mute)
    {
        MuteMusic(mute);
        MuteSFX(mute);
        MuteUI(mute);
    }

    public void MuteMusic(bool mute)
    {
        _musicMuted = mute;
        if (mute)
            _musicSource.Pause();
        else if (_musicSource.clip != null)
            _musicSource.Play();
    }

    public void MuteSFX(bool mute)
    {
        _sfxMuted = mute;
    }

    public void MuteUI(bool mute)
    {
        _uiMuted = mute;
    }
}
