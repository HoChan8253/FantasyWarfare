using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private PlayerExp _playerExp;
    [SerializeField] private Slider _expSlider;
    [SerializeField] private TMP_Text _levelText;

    private void Start()
    {
        if(_playerExp == null & GameManager._instance != null && GameManager._instance._player != null)
        {
            _playerExp = GameManager._instance._player.GetComponent<PlayerExp>();
        }
    }

    private void Update()
    {
        if(_playerExp == null)
        {
            return;
        }

        _expSlider.maxValue = _playerExp.ExpToNextLevel;
        _expSlider.value = _playerExp.CurrentExp;

        _levelText.text = $"Level : {_playerExp.Level}";
    }
}
