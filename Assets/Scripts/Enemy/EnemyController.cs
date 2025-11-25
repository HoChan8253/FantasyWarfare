using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    private static readonly List<EnemyController> _activeEnemies = new List<EnemyController>();
    public static IReadOnlyList<EnemyController> _ActiveEnemise => _activeEnemies;

    [Header("Enemy Data")]
    [SerializeField] private EnemyData _data;

    [Header("Target")]
    [Tooltip("Chasing Target")]
    [SerializeField] private Rigidbody2D _target;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _sprite;

    private float _currentHP;
    private bool _isAlive;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        if(_data == null)
        {
            return;
        }

        _currentHP = _data._maxHP;
        _isAlive = true;

        if(_data._sprite != null)
        {
            _sprite.sprite = _data._sprite;
        }

        if(_data._animatorController != null)
        {
            _anim.runtimeAnimatorController = _data._animatorController;
        }
    }

    private void OnEnable()
    {
        _isAlive = true;
        _activeEnemies.Add(this);

        if(_data != null)
        {
            _currentHP = _data._maxHP;
            _rigid.linearVelocity = Vector2.zero;
        }

        if (GameManager._instance == null || GameManager._instance._player == null)
        {
            Debug.LogWarning("GameManager 또는 Player 가 아직 준비 안됬습니다.");
            return;
        }

        _target = GameManager._instance._player.GetComponent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        _activeEnemies.Remove(this);
    }

    private void FixedUpdate()
    {
        if(!_isAlive)
        {
            _rigid.linearVelocity = Vector2.zero;
            return;
        }

        if (_target == null || _data == null)
        {
            return;
        }

        RespawnEnemy();
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        float _moveSpeed = _data._moveSpeed;

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
        float _repoDistance = _data._repoDistance;
        float _respawnRadius = _data._respawnRadius;

        float _dist = Vector2.Distance(_rigid.position, _target.position);

        if(_dist > _repoDistance)
        {
            Vector2 _center = _target.position;
            Vector2 _offset = Random.insideUnitCircle.normalized * _respawnRadius;

            _rigid.position = _center + _offset;
            _rigid.linearVelocity = Vector2.zero;
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (!_isAlive || _data == null)
        {
            return;
        }

        _currentHP -= damage;

        if(_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _isAlive = false;
        _rigid.linearVelocity = Vector2.zero;

        if(_data != null && GameManager._instance != null && GameManager._instance._player != null)
        {
            PlayerExp playerExp = GameManager._instance._player.GetComponent<PlayerExp>();
            if(playerExp != null)
            {
                playerExp.GainExp(_data._exp);
            }
        }

        //if(_anim != null)
        //{
        //    _anim.SetTrigger("Die");
        //}

        if(_data.deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(_data.deathSFX, transform.position);
        }

        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }
}
