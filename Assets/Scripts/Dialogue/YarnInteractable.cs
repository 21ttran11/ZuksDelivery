using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> progressionObjects;
    [SerializeField]
    private List<GameObject> pastObjects;

    [SerializeField]
    private string conversationStartNode;

    [SerializeField]
    public DialogueRunner dialogueRunner;

    private bool interactable = true;
    private bool isCurrentConversation = false;

    public void Start()
    {
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    public void Update()
    {
        if (interactable && !dialogueRunner.IsDialogueRunning)
        {
            EventBus.Publish(new InteractionEventData(true, this.gameObject));
            StartConversation();
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
        Debug.Log("Conversation ended");
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
            if (progressionObjects != null)
            {
                EventBus.Publish(new EventData("Activate", progressionObjects));
            }
            if (pastObjects != null)
            {
                EventBus.Publish(new EventData("Deactivate", pastObjects));
            }
            EventBus.Publish(new InteractionEventData(false, this.gameObject));
            this.gameObject.SetActive(false);
        }
    }

    //    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }

}