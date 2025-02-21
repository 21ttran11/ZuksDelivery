using UnityEngine;

public class Ground : MonoBehaviour
{
    [Header("Ground Detection Settings")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField, Range(0f, 1f)] private float slopeLimit = 0.7f;
    [SerializeField] private int groundRayCount = 3; 
    
    [Header("Ground Check Origins")]
    [SerializeField] private Vector2 groundCheckOffset = new Vector2(0f, -0.5f);
    [SerializeField] private float groundCheckWidth = 0.8f;
    
    [Header("Debug")]
    [SerializeField] private bool showDebugGizmos = true;
    
    private bool isGrounded;
    private float currentGroundNormal;
    private float groundFriction;
    private Vector2 groundVelocity;
    private Collider2D currentGroundCollider;
    
    private void Update()
    {
        CheckGround();
    }
    
    private void CheckGround()
    {
        Vector2 position = (Vector2)transform.position + groundCheckOffset;
        isGrounded = false;
        groundFriction = 0f;
        currentGroundNormal = Vector2.up.y;
        currentGroundCollider = null;
        
        RaycastHit2D sphereHit = Physics2D.CircleCast(
            position + Vector2.up * groundCheckRadius,
            groundCheckRadius,
            Vector2.down,
            groundCheckDistance + groundCheckRadius,
            groundLayer
        );
        
        if (sphereHit)
        {
            float startX = position.x - groundCheckWidth / 2;
            float endX = position.x + groundCheckWidth / 2;
            
            for (int i = 0; i < groundRayCount; i++)
            {
                float t = i / (float)(groundRayCount - 1);
                Vector2 rayStart = new Vector2(Mathf.Lerp(startX, endX, t), position.y + groundCheckRadius);
                RaycastHit2D rayHit = Physics2D.Raycast(rayStart, Vector2.down, groundCheckDistance + groundCheckRadius, groundLayer);
                
                if (rayHit)
                {
                    if (rayHit.normal.y >= slopeLimit)
                    {
                        isGrounded = true;
                        currentGroundNormal = rayHit.normal.y;
                        currentGroundCollider = rayHit.collider;
                        
                        groundFriction = Mathf.Lerp(1, 0, (1 - rayHit.normal.y) / (1 - slopeLimit));
                        
                        if (currentGroundCollider.attachedRigidbody != null)
                        {
                            groundVelocity = currentGroundCollider.attachedRigidbody.velocity;
                        }
                        else
                        {
                            groundVelocity = Vector2.zero;
                        }
                    }
                }
            }
        }
    }
    
    #region Public Accessors
    public bool IsGrounded() => isGrounded;
    public float GetGroundNormal() => currentGroundNormal;
    public float GetGroundFriction() => groundFriction;
    public Vector2 GetGroundVelocity() => groundVelocity;
    public Collider2D GetGroundCollider() => currentGroundCollider;
    #endregion
    
    #region Debug Visualization
    private void OnDrawGizmos()
    {
        if (!showDebugGizmos) return;
        
        Vector2 position = (Vector2)transform.position + groundCheckOffset;
        
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(position, groundCheckRadius);
        
        float startX = position.x - groundCheckWidth / 2;
        float endX = position.x + groundCheckWidth / 2;
        
        for (int i = 0; i < groundRayCount; i++)
        {
            float t = i / (float)(groundRayCount - 1);
            Vector2 rayStart = new Vector2(Mathf.Lerp(startX, endX, t), position.y + groundCheckRadius);
            Gizmos.DrawLine(rayStart, rayStart + Vector2.down * (groundCheckDistance + groundCheckRadius));
        }
    }
    #endregion
}