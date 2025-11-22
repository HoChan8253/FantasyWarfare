using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public PlayerController _player;
    public ObjectPoolManager _objPool;

    private void Awake()
    {
        _instance = this;
    }
}
