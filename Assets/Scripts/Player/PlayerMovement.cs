using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] float speed = 5;
    [SerializeField] float jumpForce = 18;

    [Header("Components")]
    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] SpriteRenderer spriteRenderer;
    private Animator anim;

    [Header("Controllers")]
    private float xInput;
    [SerializeField]private bool canDoubleJump = true;
    public bool canSwitch = true;
    
    public bool isFacingRight = true;
    [SerializeField]private bool isWallSliding;
    private float wallSlideSpeed = 2f;

    [Header("Wall Jump")]
    private bool isWallJumping;
    private float wallJumpDirection;
    private float wallJumpTime = 0.2f;
    private float wallJumpCounter;
    private float wallJumpDuration = 0.4f;
    [SerializeField] Vector2 wallJumpPower = new(7, 18);

    [Header("Dash")]
    private bool canDash = true;
    [SerializeField] float dashForce = 5;
    [SerializeField] float dashCooldown = 1;
    private float dashCounter;
    public bool isDashing = false;
    readonly float dashDuration = 0.2f;

    [Header("Climb")]
    [SerializeField]private bool canClimb;
    private bool isClimbing;
    [SerializeField] float climbSpeed = 3;

    [Header("Camera")]
    [SerializeField] private GameObject cameraFollowGO;
    private float fallSpeedYDampingChangeThreshold;

    private CameraFollowObject cameraFollowObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cameraFollowObject = cameraFollowGO.GetComponent<CameraFollowObject>();
        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingChangeThreshold;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        WallSlide();
        if (!isDashing)
        {
            isClimbing = false;
            if (!isWallJumping)
            {
                Move();
                CheckFlip();
            }
            if (Input.GetKeyDown(KeyCode.W) && !isWallSliding)
            {
                Jump();
            }
            else if(canClimb && Input.GetKey(KeyCode.Space)){
                Climb();
            }
            else
            {
                rb.gravityScale = 5;
                WallJump();
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Dash();
            }
        }

        if(!CameraManager.instance.IsLerpingYDamping)
        {
            if(rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.LerpedFromPlayerFalling)
                CameraManager.instance.LerpYDamping(true);
            else if(CameraManager.instance.LerpedFromPlayerFalling)
            {
                CameraManager.instance.LerpedFromPlayerFalling = false;
                CameraManager.instance.LerpYDamping(false);
            }
        }
        dashCounter += Time.deltaTime;
    }

    private void Move()
    {
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
    }

    #region Jump

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

    #endregion

    #region WallJump

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = isFacingRight ? -1 : 1;
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
            Flip();
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

    private bool IsWalled()
    {
        Collider2D wall = Physics2D.OverlapBox(wallCheck.position, new Vector2(0.2f, spriteRenderer.bounds.size.y - 0.1f), 0, wallLayer);
        if(wall?.gameObject.layer == LayerMask.NameToLayer("ClimbableWall") && gameObject.layer == LayerMask.NameToLayer("Flora")){
            canClimb = true;
        }else{
            canClimb = false;
        }
        return wall;
    }

    #endregion

    #region Dash
    private void Dash()
    {
        if (!canDash) return;

        Vector2 dashDirection = new Vector2(isFacingRight ? 1 : -1, 0) * dashForce;
        
        if(gameObject.layer == LayerMask.NameToLayer("Nami")){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dashDirection.normalized, dashDuration * dashForce, wallLayer);
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("BreakableWall"))
                {
                    if (hit.collider.TryGetComponent(out BreakableWall _))
                    {
                        hit.collider.isTrigger = true;
                    }
                }
            }
        }
        
        if (dashCounter > dashCooldown)
        {
            if (isWallSliding || isClimbing)
            {
                dashDirection.x *= -1;
                Flip();
            }
            anim.SetTrigger("Dash");
            float initialGravityScale = rb.gravityScale;
            rb.gravityScale = 0;
            isDashing = true;
            canSwitch = false;
            canDash = false;
            rb.velocity = dashDirection;
            dashCounter = 0;
            StartCoroutine(DisableDash(initialGravityScale));
        }
    }

    IEnumerator DisableDash(float g)
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        canSwitch = true;
        if(IsGrounded()) canDash = true;
        rb.gravityScale = g;
    }

    IEnumerator RestoreBreakableWall(RaycastHit2D hit){
        yield return new WaitForSeconds(dashDuration);
        hit.collider.GetComponent<BreakableWall>().Deactivate();
    }
    #endregion

    #region Flip

    private void CheckFlip()
    {
        if(isFacingRight && xInput < 0 || !isFacingRight && xInput > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(new Vector3(0, isFacingRight ? 180 : -180, 0));
        cameraFollowObject.CallTurn();
    }

    #endregion

    #region Climb

    private void Climb(){
        float yInput = Input.GetAxisRaw("Vertical");
        isClimbing = true;
        rb.gravityScale = 0;
        if (yInput != 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, yInput * climbSpeed);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
    }

    #endregion
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(spriteRenderer.bounds.size.x - 0.1f, 0.2f), 0, groundLayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (IsGrounded())
            {
                canDoubleJump = true;
                canDash = true;
            }
        }
    }
}
