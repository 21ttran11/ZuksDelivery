using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeelieChangeScene : MonoBehaviour
{
    public Animator heelieFall;
    private bool fell = false;
    public ClickHandler clickHandler;

    private void Update()
    {
        clickHandler = FindObjectOfType<ClickHandler>();
    }

    private void OnEnable()
    {
        Debug.Log("Heelies are now available");
    }

    private void Fall()
    {
        if (fell == false)
        {
            heelieFall.SetBool("falling", true);
            fell = true;
        }
    }
}
