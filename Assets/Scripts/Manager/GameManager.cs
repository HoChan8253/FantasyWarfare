using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField] private GameObject _gameOverPanel;

    [Header("Player")]
    public PlayerController _player;
    public PlayerHP _playerHP;
    public PlayerExp _playerExp;
    public int _KillCount { get; private set; }

    [Header("Manager")]
    public ObjectPoolManager _objPool;

    public float _gameTime;
    //public float _maxGameTime = 2 * 10f;

    private void Awake()
    {
        _KillCount = 0;

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

        if (_player != null)
        {
            _playerHP = _player.GetComponent<PlayerHP>();
            _playerExp = _player.GetComponent<PlayerExp>();
        }
        else
        {
            Debug.Log("GameManager 의 PlayerController 가 등록되지 않았습니다.");
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if(_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }
    }

    public void OnClickGoTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void AddKill()
    {
        _KillCount++;
    }
    private void Update()
    {
        _gameTime += Time.deltaTime;
    }
}
