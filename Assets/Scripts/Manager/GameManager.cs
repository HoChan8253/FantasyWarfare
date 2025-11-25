using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

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
        _instance = this;
        _KillCount = 0;

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

    public void AddKill()
    {
        _KillCount++;
    }
    private void Update()
    {
        _gameTime += Time.deltaTime;

        //if(_gameTime > _maxGameTime)
        //{
        //    _gameTime = _maxGameTime;
        //}
    }
}
