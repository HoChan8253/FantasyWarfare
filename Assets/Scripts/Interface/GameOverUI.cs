using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void OnClickGoTitle()
    {
        if(GameManager._instance != null)
        {
            GameManager._instance.OnClickGoTitle();
        }
        else
        {
            Debug.LogWarning("[GameOverUI] GameManager 인스턴스 없음");
        }
    }
}
