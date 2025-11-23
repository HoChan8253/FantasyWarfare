using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Status")]
    [SerializeField] private float _moveSpeed = 3f;

    private Rigidbody2D _rigid;
    public Vector2 _moveInput;
    private Animator _anim;
    private SpriteRenderer _sprite;

    [SerializeField] private EnemySpawner _enemySpawner;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();

        if(_enemySpawner == null)
        {
            _enemySpawner = GetComponentInChildren<EnemySpawner>();
        }
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

        bool _isMoving = _moveInput.sqrMagnitude > 0.01f;
        _anim.SetBool("IsMoving", _isMoving);

        if(_moveInput.x < 0)
        {
            _sprite.flipX = true;
        }
        else if(_moveInput.x > 0)
        {
            _sprite.flipX = false;
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        //if(!ctx.performed)
        //{
        //    return;
        //}
        //
        //Debug.Log("OnJump »£√‚µ ");
        //
        //if(_enemySpawner != null)
        //{
        //    _enemySpawner.SpawnOne();
        //}
        //else
        //{
        //    Debug.Log("π∫∞° «“¥Á æ»µ ");
        //}
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _moveInput * _moveSpeed;
    }
}
