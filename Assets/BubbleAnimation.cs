using UnityEngine;

public class BubbleAnimation : MonoBehaviour
{
    public float wiggleSpeed = 5f;
    public float wiggleAmount = 0.05f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        transform.localScale = originalScale + new Vector3(wiggle, wiggle, 0);
    }
}