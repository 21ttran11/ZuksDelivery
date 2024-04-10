using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Timeline;

public class HeelieDialogue : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogue1;

    [SerializeField]
    private GameObject dialogueFinal;

    [SerializeField]
    private GameObject heelies;

    [SerializeField]
    private GameObject locker;

    private int active;
    private void Update()
    {
        ChildrenDeactivated(locker);
        ChooseDialogue();
    }

    private void ChildrenDeactivated(GameObject locker)
    {
        foreach (Transform child in locker.transform)
        {
            if (child.gameObject.activeSelf)
            {
                active += 1;
            }
        }
    }

    private void ChooseDialogue()
    {
        if (active > 0)
        {
            dialogue1.SetActive(true);
        }

        if (active == 0)
        {
            dialogueFinal.SetActive(true);
        }
    }
}
