using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f;
    [SerializeField, Range(0f, 5)] private int maxAirJumps = 0;
//  [SerializeField, Range(0f, 5f)] private float downwardMovementMultiplier = 3f;
    [SerializeField, Range(0f, 5f)] private float upwardMovementMultiplier = 1.7f;

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

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();

        isJumpReset = true;
        defaultGravityScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump = input.RetrieveJumpInput(gameObject);
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if (onGround)
        {
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("Jump", false);
            }
            jumpPhase = 0;
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
        if (body.velocity.y > 0)
        {
            body.gravityScale = upwardMovementMultiplier;
        }
        else if (body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }

        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if(onGround || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            isJumping = true;
            animator.SetBool("Jump", true);
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if(velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }
            velocity.y += jumpSpeed; 
        }
    }
}
