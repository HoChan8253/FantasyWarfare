using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Status")]
    [SerializeField] private float _moveSpeed = 3f;

    [Tooltip("Chasing target")]
    [SerializeField] private Rigidbody2D _target;

    private bool _isAlive;

    private Rigidbody2D _rigid;
    private Animator _anim;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (_target == null)
        {
            return;
        }

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
}
