using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public PlayerController _player;
    public ObjectPoolManager _objPool;

    public float _gameTime;
    public float _maxGameTime = 2 * 10f;

    private void Awake()
    {
        _instance = this;
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
