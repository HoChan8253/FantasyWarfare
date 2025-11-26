using UnityEngine;
using UnityEngine.UI;

public class VolumeUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        // 시작할때 옵션 패널 비활성화
        if(_optionPanel != null)
        {
            _optionPanel.SetActive(false);
        }

        if(SoundManager._instance == null)
        {
            Debug.LogWarning("[VolumeUI] SoundManager 인스턴스 없음");
            return;
        }

        // 현재 볼륨값 슬라이더에 반영
        _bgmSlider.value = SoundManager._instance._bgmVolume;
        _sfxSlider.value = SoundManager._instance._sfxVolume;

        // 슬라이더 값이 바뀔 때마다 SoundManager에게 전달
        _bgmSlider.onValueChanged.AddListener(OnBGMChanged);
        _sfxSlider.onValueChanged.AddListener(OnSFXChanged);
    }

    public void OpenOptionPanel()
    {
        if(_optionPanel != null)
        {
            _optionPanel.SetActive(true);
        }
    }

    public void CloseOptionPanel()
    {
        if(_optionPanel != null)
        {
            _optionPanel.SetActive(false);
        }
    }

    private void OnBGMChanged(float value)
    {
        if(SoundManager._instance != null)
        {
            SoundManager._instance.SetBGMVolume(value);
        }
    }

    private void OnSFXChanged(float value)
    {
        if(SoundManager._instance != null)
        {
            SoundManager._instance.SetSFXVolume(value);
        }
    }
}
