using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private InputController input = null;
    [SerializeField, Range(0f, 100f)] public float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;
    private Jump jump; 

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    private Animator animator;
    private float orgSpeed;

    public float sprintSpeed = 6f;
    public bool canSprint = false;

    [SerializeField] private string footstepStringSfx = null;
    public float footstepVolume = 0.5f;
    public float footstepPitchVolume = 0.7f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        jump = GetComponent<Jump>(); 
        orgSpeed = maxSpeed;
    }

    private void Start()
    {
        StartCoroutine(PlayFootstepSounds());
    }

    void Update()
    {
        direction.x = input.RetrieveMoveInput(gameObject);
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetGroundFriction(), 0f);

        if (direction.x != 0f && Mathf.Sign(transform.localScale.x) != Mathf.Sign(direction.x))
        {
            FlipCharacter();
        }

        if (Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            maxSpeed = sprintSpeed;
            animator.SetBool("Skating", true);
        }
        else
        {
            maxSpeed = orgSpeed;
            animator.SetBool("Skating", false);
        }
    }

    private void FixedUpdate()
    {
        onGround = ground.IsGrounded() && !jump.IsJumpBufferActive;
        animator.SetBool("Grounded", onGround);
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;
        animator.SetFloat("IdleSpeed", Mathf.Abs(velocity.x));
    }

    private void FlipCharacter()
    {
        if (maxSpeed <= 0)
            return;
        Vector3 flippedScale = transform.localScale;
        flippedScale.x = -flippedScale.x;
        transform.localScale = flippedScale;
    }

    private IEnumerator PlayFootstepSounds()
    {
        bool isPlaying = false;

        while (true)
        {
            if (onGround && Mathf.Abs(velocity.x) > 0.1f)
            {
                if (!isPlaying)
                {
                    if (AudioManager.instance != null)
                        AudioManager.PlaySFX(footstepStringSfx, footstepVolume, true, footstepPitchVolume);
                    isPlaying = true;
                }
            }
            else
            {
                if (isPlaying)
                {
                    if (AudioManager.instance != null)
                        AudioManager.StopSFX();
                    isPlaying = false;
                }
            }

            yield return null;
        }
    }
}
