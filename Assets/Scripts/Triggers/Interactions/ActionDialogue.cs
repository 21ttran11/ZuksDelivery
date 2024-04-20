using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDialogue : SceneAction
{
    private Transform dialogue;
    public override void Interact()
    {
        dialogue = this.gameObject.transform.GetChild(0);
    }
}
