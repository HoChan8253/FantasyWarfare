using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [Header("Player")]
    public PlayerController _player;
    public PlayerHP _playerHP;
    public PlayerExp _playerExp;

    [Header("Manager")]
    public ObjectPoolManager _objPool;

    public float _gameTime;
    public float _maxGameTime = 2 * 10f;

    private void Awake()
    {
        _instance = this;

        if (_player != null)
        {
            _playerHP = _player.GetComponent<PlayerHP>();
            _playerExp = _player.GetComponent<PlayerExp>();
        }
    }

    private void Update()
    {
        _gameTime += Time.deltaTime;

        if(_gameTime > _maxGameTime)
        {
            _gameTime = _maxGameTime;
        }
    }
}
