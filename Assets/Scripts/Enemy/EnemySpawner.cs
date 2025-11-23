using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Transform _player;
    [SerializeField] private float _spawnDelay = 1.5f;

    [Header("Player 스폰 반경")]
    [SerializeField] private float _minRadius = 6f;
    [SerializeField] private float _maxRadius = 8f;

    private Coroutine _spawnRoutine;

    private void Start()
    {
        _spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void OnEnable()
    {
        //_spawnRoutine = StartCoroutine(SpawnLoop());
    }

    private void OnDisable()
    {
        if(_spawnRoutine != null)
        {
            StopCoroutine(_spawnRoutine);
            _spawnRoutine = null;
        }
    }

    private IEnumerator SpawnLoop()
    {
        while(true)
        {
            SpawnOne();
            yield return new WaitForSeconds(_spawnDelay);
        }
    }

    public void SpawnOne()
    {
        Vector2 spawnPos = RandomPosition();
        GameObject enemy = _poolManager.GetPoolObj(Random.Range(0,2));
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
}
