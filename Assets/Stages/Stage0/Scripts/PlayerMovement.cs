using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool _isOnGround;

    private InputAction _moveAction;
    private InputAction _jumpAction;

    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions.FindAction("Player/Move");
        _jumpAction = InputSystem.actions.FindAction("Player/Jump");
    }

    private void Update()
    {
        float moveInput = _moveAction.ReadValue<Vector2>().x;
        _rb.linearVelocityX = moveInput * moveSpeed;

        bool jumpInput = _jumpAction.IsPressed();

        if (jumpInput && _isOnGround)
        {
            _rb.linearVelocityY = jumpForce;
        }
    }

    private void FixedUpdate()
    {
        _isOnGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
