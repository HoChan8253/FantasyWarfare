using UnityEngine;
using TMPro;

public class GameTimeUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    private void Update()
    {
        if(GameManager._instance == null)
        {
            return;
        }

        float time = GameManager._instance._gameTime;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        _timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
