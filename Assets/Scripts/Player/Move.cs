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

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    private Animator animator;
    private float orgSpeed;

    public float sprintSpeed = 6f;
    public bool canSprint = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        orgSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = input.RetrieveMoveInput(gameObject);
        desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

        if (direction.x != 0f && Mathf.Sign(transform.localScale.x) != Mathf.Sign(direction.x))
        {
            FlipCharacter();
        }

        if(Input.GetKey(KeyCode.LeftShift) && canSprint)
        {
            maxSpeed = sprintSpeed;
        }
        else
        {
            maxSpeed = orgSpeed;
        }
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
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

        // Flip the character by scaling
        Vector3 flippedScale = transform.localScale;
        flippedScale.x = -flippedScale.x;
        transform.localScale = flippedScale;
    }



}
