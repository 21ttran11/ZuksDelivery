using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    private DialogueRunner dialogueRunner;
    [SerializeField] private string conversationStartNode;
    private bool interactable;
    private bool isCurrentConversation;

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
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }
    private void StartConversation()
    {
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
        }
    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }
}
