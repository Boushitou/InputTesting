using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 10f;
    public float JumpForce = 10f;

    private Rigidbody2D _rb;

    private Transform _groundedFeet;

    private float _movementInput;
    private LayerMask _groundLayer;

    public float MovementInput
    {
        get { return _movementInput; }
        set { _movementInput = value; }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _groundedFeet = gameObject.transform.Find("Grounded");
        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        _rb.velocity = new Vector2(_movementInput * Speed, _rb.velocity.y);
    }

    public void Jump()
    {
        if (IsGrounded())
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(_groundedFeet.position, Vector2.down, 0.5f, _groundLayer);

        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }
}
