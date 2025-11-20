using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public PlayerController _player;

    private void Awake()
    {
        _instance = this;
    }

}
