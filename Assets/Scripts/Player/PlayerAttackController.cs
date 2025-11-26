using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private ObjectPoolManager _poolManager;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ItemManager _itemManager;

    [Header("Attack")]
    [SerializeField] private int _projectileIndex = 0; // ObjectPoolManager.Prefabs
    [SerializeField] private float _baseAttackDelay = 1f;
    [SerializeField] private int _baseDamage = 1;

    private float _timer;

    private void Awake()
    {
        if(_firePoint == null)
        {
            _firePoint = transform;
        }

        if (_itemManager == null)
        {
            _itemManager = GetComponent<ItemManager>();
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        float attSpeedMulti = 1f;
        if(_itemManager != null)
        {
            attSpeedMulti = _itemManager._attackSpeedMultiplier;
        }

        float safeMulti = attSpeedMulti;
        if(safeMulti < 0.01f)
        {
            safeMulti = 0.01f;
        }

        float currentDelay = _baseAttackDelay / safeMulti;

        if(_timer >= currentDelay)
        {
            _timer = 0f;
            TryAttack();
        }
    }

    private void TryAttack()
    {
        // 가장 가까운 적 찾기
        EnemyController target = GetNearestEnemy();
        if(target == null)
        {
            return;
        }

        // 대미지 배율 계산
        float dmgMulti = 1f;

        if(_itemManager != null)
        {
            dmgMulti = _itemManager._damageMultiplier;
        }
        // 실제 대미지 = 기본 대미지 x 배율
        float rawDamage = _baseDamage / dmgMulti;
        // 반올림 후 int 변환
        int finalDamage = Mathf.RoundToInt(rawDamage);

        // 발사 개수
        int projectileCount = 1;
        if(_itemManager != null)
        {
            projectileCount = _itemManager._projectileCount;
        }
        
        if(projectileCount < 1)
        {
            projectileCount = 1;
        }

        // 기본 방향 계산
        Vector2 baseDir = (Vector2)target.transform.position - (Vector2)_firePoint.position;
        baseDir.Normalize();

        // 발사 개수만큼 반복해서 투사체 발사
        for(int i = 0; i < projectileCount; i++)
        {
            GameObject projObj = _poolManager.GetPoolObj(_projectileIndex);

            projObj.transform.position = _firePoint.position;

            Vector2 dir = baseDir;

            ProjectileController proj = projObj.GetComponent<ProjectileController>();

            if(proj != null)
            {
                proj.Init(dir, finalDamage);
            }
        }

    }

    private EnemyController GetNearestEnemy()
    {
        var enemise = EnemyController._ActiveEnemise; // 활성화 적 가져오기
        if(enemise == null || enemise.Count == 0)
        {
            return null;
        }

        EnemyController _nearest = null;
        float _minSqr = float.MaxValue; // 현재까지의 최소 거리 제곱값
        Vector2 _myPos = transform.position; // 플레이어 위치

        for(int i = 0; i < enemise.Count; i++)
        {
            EnemyController enemy = enemise[i];
            if(enemy == null || !enemy.gameObject.activeInHierarchy)
            {
                continue;
            }

            float _sqr = ((Vector2)enemy.transform.position - _myPos).sqrMagnitude;
            if(_sqr < _minSqr) // 현재 minSqr 보다 작으면 더 가까운 적 발견
            {
                _minSqr = _sqr;
                _nearest = enemy;
            }
        }
        return _nearest;
    }
}
