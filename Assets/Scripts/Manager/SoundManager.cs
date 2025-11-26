using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;

    [Header("BGM")]
    [SerializeField] private AudioSource _bgmSource;
    [SerializeField] private AudioClip _mainBGM;

    [Header("SFX")]
    [SerializeField] private AudioSource _sfxSource;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float _bgmVolume = 0.2f;
    [Range(0f, 1f)] public float _sfxVolume = 0.2f;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        PlayBGM(_mainBGM);
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
        _bgmVolume = volume;
        _bgmSource.volume = _bgmVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip == null || _sfxSource == null)
        {
            return;
        }

        _sfxSource.volume = _sfxVolume;
        _sfxSource.PlayOneShot(clip);
    }
}
