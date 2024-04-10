using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeelieEnd : MonoBehaviour
{
    [SerializeField]
    private GameObject locker;

    private int active;
    private void Update()
    {
        active = 0;
        ChildrenDeactivated(locker);
        CheckEnd();
    }

    public void ChildrenDeactivated(GameObject locker)
    {
        foreach (Transform child in locker.transform)
        {
            if (child.gameObject.activeSelf)
            {
                active += 1;
            }
        }
    }

    public void CheckEnd()
    {
        if (active == 0)
        {
            Transform child = transform.GetChild(0);
            child.gameObject.SetActive(true);
        }
    }
}
