using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0f, 5)] private int maxAirJumps = 0;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;
    public bool IsJumpBufferActive => jumpBufferTimer > 0;
    [SerializeField] private float jumpBufferDuration = 0.4f;
    private float jumpBufferTimer = 0f;
    [SerializeField] private float landingBufferTime = 0.1f;
    private float landingTimer = 0f;
    [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 2.5f;
    

    private Rigidbody2D body;
    private Ground ground;
    private Vector2 velocity;

    private int jumpPhase;
    private float defaultGravityScale;

    private bool desiredJump;
    private bool onGround;
    private bool isJumping;
    private bool isJumpReset;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        isJumpReset = true;
        defaultGravityScale = 1f;
    }

    void Update()
    {
        desiredJump = input.RetrieveJumpInput(gameObject);
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;
        onGround = ground.IsGrounded();

        if (jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.fixedDeltaTime;
        }
        if (onGround && velocity.y <= 0 && jumpBufferTimer <= 0)
        {
            landingTimer += Time.fixedDeltaTime;
            if (landingTimer >= landingBufferTime)
            {
                if (isJumping)
                {
                    isJumping = false;
                    animator.SetBool("Jump", false);
                }
                jumpPhase = 0;
            }
        }
        else
        {
            landingTimer = 0f;
        }

         if (velocity.y > 0)
    {
        body.gravityScale = upwardMovementMultiplier;
    }
    else if (velocity.y < 0) // Added for falling
    {
        body.gravityScale = downwardMovementMultiplier;
    }
    else
    {
        body.gravityScale = defaultGravityScale;
    }

        if (desiredJump && isJumpReset)
        {
            isJumpReset = false;
            desiredJump = false;
            JumpAction();
        }
        else if (!desiredJump)
        {
            isJumpReset = true;
        }

        if (velocity.y > 0)
        {
            body.gravityScale = upwardMovementMultiplier;
        }
        else if (velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if (onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            isJumping = true;
            animator.SetBool("Jump", true);
            jumpBufferTimer = jumpBufferDuration;

            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed;
        }
    }
}
