using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDialogueEvent : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
