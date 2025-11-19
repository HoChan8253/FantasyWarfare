using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigid;
    private Vector2 _moveInput;
    private Animator _anim;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

        bool isMoving = _moveInput.sqrMagnitude > 0.01f;
        _anim.SetBool("IsMoving", isMoving);
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _moveInput * _moveSpeed;
    }
}
