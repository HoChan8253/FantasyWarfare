using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigid;
    private Vector2 _moveInput;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _rigid.linearVelocity = _moveInput * _moveSpeed;
    }
}
