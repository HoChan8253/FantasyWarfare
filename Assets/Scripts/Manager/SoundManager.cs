using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    [Header("BGM")]
    [SerializeField] private AudioSource _bgmSource;

    [Header("SFX")]
    [SerializeField] private AudioSource _sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float _bgmVolume = 0.2f;
    [Range(0f, 1f)] public float _sfxVolume = 0.2f;

    // PalyerPrefs Key
    private const string BGM_KEY = "BGM_VOLUME";
    private const string SFX_KEY = "SFX_VOLUME";

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LoadVolume()
    {
        _bgmVolume = PlayerPrefs.GetFloat(BGM_KEY, _bgmVolume);
        _sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, _sfxVolume);

        if (_bgmSource != null)
        {
            _bgmSource.volume = _bgmVolume;
        }

        if(_sfxSource != null)
        {
            _sfxSource.volume = _sfxVolume;
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if(clip == null || _bgmSource == null)
        {
            return;
        }

        _bgmSource.clip = clip;
        _bgmSource.loop = true;
        _bgmSource.volume = _bgmVolume;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        if(_bgmSource.isPlaying)
        {
            _bgmSource.Stop();
        }
    }

    public void SetBGMVolume(float volume)
    {
        _bgmVolume = Mathf.Clamp01(volume);

        if(_bgmSource != null)
        {
            _bgmSource.volume = _bgmVolume;
        }

        SaveVolume(); // 값 바뀔 때마다 저장
    }
    
    public void SetSFXVolume(float volume)
    {
        _sfxVolume = Mathf.Clamp01(volume);

        if(_sfxSource != null)
        {
            _sfxSource.volume = _sfxVolume;
        }

        SaveVolume(); // 값 바뀔 때마다 저장
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(BGM_KEY, _bgmVolume);
        PlayerPrefs.SetFloat(SFX_KEY, _sfxVolume);
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip == null || _sfxSource == null)
        {
            //Debug.LogWarning("[SoundManager] PlaySFX 실패");
            return;
        }

        _sfxSource.volume = _sfxVolume;
        _sfxSource.PlayOneShot(clip);
    }
}
