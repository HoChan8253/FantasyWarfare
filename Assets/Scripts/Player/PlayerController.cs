using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigid;
    public Vector2 _moveInput;
    private Animator _anim;
    private SpriteRenderer _sprite;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

        bool isMoving = _moveInput.sqrMagnitude > 0.01f;
        _anim.SetBool("IsMoving", isMoving);

        if(_moveInput.x < 0)
        {
            _sprite.flipX = true;
        }
        else if(_moveInput.x > 0)
        {
            _sprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _moveInput * _moveSpeed;
    }
}
