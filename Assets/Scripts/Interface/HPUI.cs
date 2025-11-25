using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private PlayerHP _playerHP;

    private void Start()
    {
        if(_playerHP == null)
        {
            _playerHP = GameManager._instance._playerHP;
        }

        _slider.maxValue = _playerHP.MaxHP;
        _slider.value = _playerHP.CurrentHP;

        _playerHP.OnHealthChanged += UpdateHPBar;
    }

    private void UpdateHPBar(int current, int max)
    {
        _slider.maxValue = max;
        _slider.value = current;
    }
}
