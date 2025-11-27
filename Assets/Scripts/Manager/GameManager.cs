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

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (_instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            InitGameScene();
        }
        else if (scene.name == "TitleScene")
        {
            Time.timeScale = 1f;
        }
    }

    private void InitGameScene()
    {
        _KillCount = 0;
        _gameTime = 0f;

        // Player 찾기
        _player = FindAnyObjectByType<PlayerController>();

        if (_player != null)
        {
            _playerHP = _player.GetComponent<PlayerHP>();
            _playerExp = _player.GetComponent<PlayerExp>();
        }
        else
        {
            _playerHP = null;
            _playerExp = null;
        }

        // 오브젝트 풀매니저 찾기
        _objPool = FindAnyObjectByType<ObjectPoolManager>();

        // GameOver 패널 찾기
        if(_gameOverPanel == null || !_gameOverPanel.scene.IsValid())
        {
            GameObject[] allObject = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach(GameObject go in allObject)
            {
                if(go.name == "GameOverPanel")
                {
                    _gameOverPanel = go;
                    break;
                }
            }
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        if (_gameOverPanel == null)
        {
            return;
        }
        _gameOverPanel.SetActive(true);
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
