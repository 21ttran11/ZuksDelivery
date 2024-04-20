using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDialogue : SceneAction
{
    [SerializeField]
    private GameObject dialogue;
    public override void Interact()
    {
        dialogue.SetActive(true);
    }
}
