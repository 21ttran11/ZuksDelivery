using System.Collections;
using UnityEngine;

public class RaccoonAnimation : MonoBehaviour
{
    private Animator animator;
    private Coroutine talkingCoroutine;
    public float talkingDuration = 5f; // Duration for talking in seconds.

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTalkingAnimation()
    {
        animator.SetBool("Talking", true);

        // If there's already a talking coroutine, stop it before starting a new one.
        if (talkingCoroutine != null)
        {
            StopCoroutine(talkingCoroutine);
        }

        talkingCoroutine = StartCoroutine(EndTalkingAnimationAfterTime(talkingDuration));
    }

    private IEnumerator EndTalkingAnimationAfterTime(float duration)
    {
        // Wait for the specified duration.
        yield return new WaitForSeconds(duration);

        // Stop the talking animation.
        EndTalkingAnimation();
    }

    public void EndTalkingAnimation()
    {
        // Ensure that we don't stop the animation if it's already stopped.
        if (animator.GetBool("Talking"))
        {
            animator.SetBool("Talking", false);
        }

        // Optionally, you can also stop the coroutine here if you want to ensure it doesn't run again.
        if (talkingCoroutine != null)
        {
            StopCoroutine(talkingCoroutine);
            talkingCoroutine = null;
        }
    }
}
