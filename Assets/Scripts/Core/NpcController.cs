using DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    public void Interact()
    {
        DialogueHolder.Instance.ShowDialogue(dialogue);
    }
}
