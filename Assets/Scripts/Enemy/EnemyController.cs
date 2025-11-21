using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField] private float _moveSpeed = 3f;

    [Tooltip("추적 대상")]
    [SerializeField] private Rigidbody2D _target;

    [Header("Replace Settings")]
    [Tooltip("이 거리보다 멀어지면 플레이어 근처로 재배치")]
    [SerializeField][Range(10f, 25f)] private float _repoDistance = 11f;
    
    [Tooltip("플레이어 주변 재배치")]
    [SerializeField][Range(1f, 15f)] private float _respawnRadius = 8f;

    private bool _isAlive;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _sprite;

    private void MoveToTarget()
    {
        Vector2 _dir = _target.position - _rigid.position; // 이동할 방향
        Vector2 _moveDir = _dir.normalized;
        Vector2 _newVelocity = _moveDir * _moveSpeed;
        _rigid.linearVelocity = _newVelocity;

        bool _isMoving = _rigid.linearVelocity.sqrMagnitude > 0.01f;
        _anim.SetBool("IsMoving", _isMoving);

        if (_rigid.linearVelocity.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (_rigid.linearVelocity.x > 0)
        {
            _sprite.flipX = false;
        }
    }

    private void RespawnEnemy()
    {
        float _dist = Vector2.Distance(_rigid.position, _target.position);

        if(_dist > _repoDistance)
        {
            Vector2 _center = _target.position;
            Vector2 _offset = Random.insideUnitCircle.normalized * _respawnRadius;

            _rigid.position = _center + _offset;
            _rigid.linearVelocity = Vector2.zero;
        }
    }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //if(!_isAlive)
        //{
        //    _rigid.linearVelocity = Vector2.zero;
        //    return;
        //}

        if (_target == null)
        {
            return;
        }
        RespawnEnemy();

        MoveToTarget();
    }
}
