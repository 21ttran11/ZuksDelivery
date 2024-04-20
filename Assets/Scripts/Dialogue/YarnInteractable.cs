using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : SceneAction
{
    [SerializeField] private string conversationStartNode;
    private bool dialogue;
    public override void Interact()
    {
        Debug.Log("Dialogue is triggered");
    }

    [SerializeField]
    public DialogueRunner dialogueRunner;
    private bool interactable = true;
    private bool isCurrentConversation = false;
    private float defaultIndicatorIntensity;
    private bool played = false;

    public void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && played == false)
        {
            if (interactable && !dialogueRunner.IsDialogueRunning)
            {
                StartConversation();
            }
            played = true;
        }
    }

    private void StartConversation()
    {
        Debug.Log($"Started conversation with {name}.");
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            Debug.Log($"Started conversation with {name}.");
        }
    }

    //    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }
}