using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private string conversationStartNode;
    private bool interactable;

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if(interactable && !dialogueRunner.IsDialogueRunning)
            {
                StartConversation();
            }
        }
    }

    private void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
    }
    private void StartConversation()
    {
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        return;
    }

    private void DisableConversation()
    {
        return;
    }

}
