using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : SceneAction
{
    [SerializeField] private string conversationStartNode;
    private bool dialogue;

    [SerializeField]
    private bool move;

    [SerializeField]
    private GameObject activate;

    public override void Interact()
    {
        played = false;
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
        if (played == false)
        {
            if (interactable && !dialogueRunner.IsDialogueRunning)
            {
                StartConversation();
            }
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
        played = true;
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            if (activate != null)
            {
                activate.SetActive(true);
            }
            else return;
        }
    }

    //    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

}