using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class RaccoonAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    [YarnCommand("Talking")]
    public void PlayTalkingAnimation()
    {
        animator.SetBool("Talking", true);
    }


    [YarnCommand("DoneTalking")]
    public void EndTalkingAnimation()
    {
        if (animator.GetBool("Talking"))
        {
            animator.SetBool("Talking", false);
        }
    }
}
