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

    private bool dialogueDone = false;

    private int active;

    private void OnEnable()
    {
        heelies.SetActive(true);
    }
    private void Update()
    {
        ChildrenDeactivated(locker);
        ChooseDialogue();
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

    private void ChooseDialogue()
    {
        if (active > 0 && dialogueDone == false)
        {
            dialogue1.SetActive(true);
            dialogueDone = true;
        }

        if (active == 0)
        {
            dialogueFinal.SetActive(true);
        }

        else
        {
            return;
        }
    }
}
