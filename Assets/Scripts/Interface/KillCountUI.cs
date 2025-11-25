using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _killText;

    private void Update()
    {
        if(GameManager._instance == null)
        {
            return;
        }

        _killText.text = $"{GameManager._instance._KillCount} Kills";
    }
}
