using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : SceneAction
{
    [SerializeField] private string conversationStartNode;
    private bool dialogue;

    [SerializeField]
    private bool move;

    public override void Interact()
    {
        Debug.Log("Dialogue is triggered");
    }

    [SerializeField]
    public DialogueRunner dialogueRunner;

    [SerializeField]
    private GameObject deactivate;
    [SerializeField]
    private GameObject deactivateDialogue;

    private bool interactable = true;
    private bool isCurrentConversation = false;
    private float defaultIndicatorIntensity;
    private bool played = false;

    private Move playerMovement;
    private float orgSpeed = 0f;

    public void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        playerMovement = FindObjectOfType<Move>();
        orgSpeed = playerMovement.maxSpeed;
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

        if (move == false)
        {
            playerMovement.maxSpeed = 0;
        }

    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            if (deactivate != null)
            {
                deactivate.SetActive(false);
                deactivateDialogue.SetActive(false);
            }
            else return;
        }

        playerMovement.maxSpeed = orgSpeed;
    }

    //    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

}