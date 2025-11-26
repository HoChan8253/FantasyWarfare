using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private string _gameSceneName = "GameScene";

    public void OnClickStart()
    {
        SceneManager.LoadScene(_gameSceneName);
    }
}
