using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private static readonly List<EnemyController> _activeEnemies = new List<EnemyController>();
    public static IReadOnlyList<EnemyController> _ActiveEnemise => _activeEnemies;

    [Header("Enemy Data")]
    [SerializeField] private EnemyData _data;

    [Header("Target")]
    [Tooltip("Chasing Target")]
    [SerializeField] private Rigidbody2D _target;

    [Header("Contact Damage")]
    [SerializeField] private float _contactDamageDelay = 0.7f; // x 초마다 한번씩 대미지 입힘
    private float _laseContactDamageTime = -999f;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private float _hitFlashDuration = 0.1f;

    private Coroutine _hitRoutine;

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

        _originalColor = _sprite.color;

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
        TrySetTarget();
    }

    private void Start()
    {
        TrySetTarget();
    }

    private void TrySetTarget()
    {
        if (_target != null)
        {
            return;
        }

        if(GameManager._instance == null || GameManager._instance._player == null)
        {
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

        StartHitFlash(); // 피격시 연출

        if(_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        _isAlive = false;
        _rigid.linearVelocity = Vector2.zero;

        if(_data != null && GameManager._instance != null && GameManager._instance._playerExp != null)
        {
                GameManager._instance._playerExp.GainExp(_data._exp);
        }

        GameManager._instance.AddKill();

        if(_anim != null)
        {
            _anim.SetTrigger("Die");
        }

        if(_data.deathSFX != null)
        {
            AudioSource.PlayClipAtPoint(_data.deathSFX, transform.position);
        }
    }

    public void OnDieAnimationEnd()
    {
        //Debug.Log($"{name} OnDieAnimationEnd 호출");
        ObjectPoolManager.Instance.ReturnObj(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
        if(playerHP != null && _data != null)
        {
            //playerHP.TakeDamage(_data._damage);
            TryDealContactDamage(playerHP);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();
        if(playerHP != null && _data != null)
        {
            TryDealContactDamage(playerHP);
        }
    }

    private void StartHitFlash()
    {
        if(_hitRoutine != null)
        {
            StopCoroutine(_hitRoutine);
        }
        _hitRoutine = StartCoroutine(HitFlashRoutine());
    }

    private IEnumerator HitFlashRoutine()
    {
        _sprite.color = new Color(1f, 0.3f, 0.3f);

        yield return new WaitForSeconds(_hitFlashDuration);

        _sprite.color = _originalColor;
        _hitRoutine = null;
    }

    private void TryDealContactDamage(PlayerHP playerHP)
    {
        if(playerHP == null || _data == null || !_isAlive)
        {
            return;
        }

        if(Time.time - _laseContactDamageTime < _contactDamageDelay)
        {
            return;
        }

        _laseContactDamageTime = Time.time;
        playerHP.TakeDamage(_data._damage);
    }
}