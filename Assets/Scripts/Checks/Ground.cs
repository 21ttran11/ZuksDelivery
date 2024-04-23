using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float maxGroundNormalY = 0.7f; // Adjust this to allow steeper slopes
    private bool onGround;
    private float friction;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
        friction = 0;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector2 normal = collision.GetContact(i).normal;
            onGround |= normal.y > maxGroundNormalY;

            // If this is a slope and not flat ground, calculate the friction.
            if (onGround && normal.y <= maxGroundNormalY)
            {
                // This is a simple example, you might want to replace this with a more complex calculation
                // that takes into account the steepness of the slope and the friction of the material.
                friction = Mathf.Lerp(1, 0, (1 - normal.y) / (1 - maxGroundNormalY));
            }
        }
    }

    public bool GetOnGround()
    {
        return onGround;
    }

    public float GetFriction()
    {
        return friction;
    }
}
