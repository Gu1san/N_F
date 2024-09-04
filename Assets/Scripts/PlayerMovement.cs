using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 5;

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Controllers")]
    private float xInput;
    [SerializeField]private bool canDoubleJump = true;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private float wallSlideSpeed = 2f;

    [Header("Wall Jump")]
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.4f;
    private Vector2 wallJumpPower = new(8, 16);


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        WallSlide();
        WallJump();
        if (!isWallJumping)
        {
            Move();
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.W) && !isWallSliding)
        {
            Jump();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
    }

    private void Jump()
    {
        bool grounded = IsGrounded();
        if (!grounded)
        {
            if (!canDoubleJump) return;
            canDoubleJump = false;
        }
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            wallJumpCounter = wallJumpTime;
            CancelInvoke(nameof(StopWallJump));
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.W) && wallJumpCounter > 0)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y);
            wallJumpCounter = 0;

            if(transform.localScale.x != wallJumpDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJump), wallJumpDuration);
        }
    }

    private void StopWallJump()
    {
        isWallJumping = false;
    }

    private void WallSlide()
    {
        if(IsWalled() && !IsGrounded())
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void Flip()
    {
        if(isFacingRight && xInput < 0 || !isFacingRight && xInput > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(spriteRenderer.bounds.size.x - 0.1f, 0.2f), 0, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, spriteRenderer.bounds.size.y - 0.1f), 0, wallLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(IsGrounded()) canDoubleJump = true;
        }
    }
}
