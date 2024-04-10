using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelieChangeScene : MonoBehaviour
{
    public Animator heelieFall;
    private bool fell = false;
    public ClickHandler OnClick;

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
