using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Transform _firePoint;

    [Header("Attack")]
    [SerializeField] private int _projectileIndex = 0; // ObjectPoolManager.Prefabs
    [SerializeField] private float _attackDelay = 0.5f;
    [SerializeField] private int _damage = 1;

    private float _timer;

    private void Awake()
    {
        if(_firePoint == null)
        {
            _firePoint = transform;
        }    
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _attackDelay)
        {
            _timer = 0f;
            TryAttack();
        }
    }

    private void TryAttack()
    {
        EnemyController target = GetNearestEnemy();
        if(target == null)
        {
            return;
        }

        GameObject projObj = _poolManager.GetPoolObj(_projectileIndex);
        projObj.transform.position = _firePoint.position;

        Vector2 dir = (Vector2)target.transform.position - (Vector2)_firePoint.position;

        ProjectileController proj = projObj.GetComponent<ProjectileController>();
        if(proj != null)
        {
            proj.Init(dir, _damage);
        }
    }

    private EnemyController GetNearestEnemy()
    {
        var enemise = EnemyController._ActiveEnemise;
        if(enemise == null || enemise.Count == 0)
        {
            return null;
        }

        EnemyController _nearest = null;
        float _minSqr = float.MaxValue;
        Vector2 _myPos = transform.position;

        for(int i = 0; i < enemise.Count; i++)
        {
            EnemyController enemy = enemise[i];
            if(enemy == null || !enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            float _sqr = ((Vector2)enemy.transform.position - _myPos).sqrMagnitude;
            if(_sqr < _minSqr)
            {
                _minSqr = _sqr;
                _nearest = enemy;
            }
        }
        return _nearest;
    }
}
