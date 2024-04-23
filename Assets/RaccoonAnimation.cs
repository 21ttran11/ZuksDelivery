using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonAnimation : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayTalkingAnimation()
    {
        animator.SetBool("Talking", true);
    }

    public void EndTalkingAnimation()
    {
        animator.SetBool("Talking", false);
    }
}
