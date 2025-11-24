using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Transform _player;

    [Header("Player 스폰 반경")]
    [SerializeField] private float _minRadius = 6f;
    [SerializeField] private float _maxRadius = 8f;

    private float _elapsedTime;
    private float _spawnTimer;

    //private Coroutine _spawnRoutine;

    private void Start()
    {
        _elapsedTime = 0f;
        _spawnTimer = 0f;
        //_spawnRoutine = StartCoroutine(SpawnLoop());
        if(_player == null && GameManager._instance != null && GameManager._instance._player != null)
        {
            _player = GameManager._instance._player.transform;
        }
    }

    private void Update()
    {
        if(_player == null)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;
        _spawnTimer += Time.deltaTime;

        float currentSpawnDelay = GetCurrentSpawnDelay();

        if(_spawnTimer >= currentSpawnDelay)
        {
            _spawnTimer = 0f;
            SpawnOne();
        }

        //_level = Mathf.FloorToInt(GameManager._instance._gameTime / 10f);
    }

    //private void OnEnable()
    //{
    //    _spawnRoutine = StartCoroutine(SpawnLoop());
    //}

    //private void OnDisable()
    //{
    //    if(_spawnRoutine != null)
    //    {
    //        StopCoroutine(_spawnRoutine);
    //        _spawnRoutine = null;
    //    }
    //}

    //private IEnumerator SpawnLoop()
    //{
    //    while(true)
    //    {
    //        SpawnOne();
    //        yield return new WaitForSeconds(_spawnDelay);
    //    }
    //}

    public void SpawnOne()
    {
        if(ObjectPoolManager.Instance == null)
        {
            Debug.LogWarning("ObjectPoolManager 인스턴스가 없습니다.");
            return;
        }

        Vector2 spawnPos = RandomPosition();

        int enemyIndex = GetEnemyIndex();
        GameObject enemy = _poolManager.GetPoolObj(enemyIndex);
        enemy.transform.position = spawnPos;
        Debug.Log("플레이어 주변 랜덤 소환");
    }

    private Vector2 RandomPosition()
    {
        Vector2 center = _player.position;

        // 최소 반경 ~ 최대 반경 사이 거리
        float distance = Random.Range(_minRadius, _maxRadius);

        // 0 ~ 360도 랜덤 방향
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // 방향 벡터 생성
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        // 위치 = 플레이어 + ( 방향 * 벡터 )
        return center + dir * distance;
    }

    [Header("Spawn 속도")]
    [SerializeField] private float _phase1Delay = 2f;
    [SerializeField] private float _phase2Delay = 1f;
    [SerializeField] private float _phase3Delay = 0.5f;

    [Header("난이도")]
    [SerializeField] private float _phase1EndTime = 30f;
    [SerializeField] private float _phase2EndTime = 60f;

    private float GetCurrentSpawnDelay()
    {
        if(_elapsedTime < _phase1EndTime)
        {
            return _phase1Delay;
        }
        else if(_elapsedTime < _phase2EndTime)
        {
            return _phase2Delay;
        }
        else
        {
            return _phase3Delay;
        }
    }

    private int GetEnemyIndex()
    {
        if(_elapsedTime < _phase1EndTime)
        {
            return 0;
        }
        else if(_elapsedTime < _phase2EndTime)
        {
            return Random.Range(0, 2);
        }
        else
        {
            return Random.Range(1, 3);
        }
    }
}
