using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Transform _spawnPoint;

    private void Update()
    {
        
    }

    public void SpawnOne()
    {
        GameObject enemy = _poolManager.GetPoolObj(0);
        enemy.transform.position = _spawnPoint.position;
        Debug.Log("소환되었음");
    }
}
