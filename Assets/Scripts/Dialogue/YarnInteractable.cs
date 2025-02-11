using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    private Dictionary<string, DialogueRunner> dialogueRunnersInScene = new Dictionary<string, DialogueRunner>();

    private bool interactable = true;
    private bool isCurrentConversation = false;

    public YarnProject yarnProject;

    public void Start()
    {
        dialogueRunnersInScene = FindObjectsOfType<DialogueRunner>().ToDictionary(runner => runner.name, runner => runner);
        foreach (var runner in dialogueRunnersInScene.Values)
        {
            var customView = runner.gameObject.AddComponent<CustomDialogueView>();
            runner.onDialogueComplete.AddListener(EndConversation);
        }
    }

    public void Update()
    {
        if (interactable)
        {
            EventBus.Publish(new InteractionEventData(true, this.gameObject));
            foreach (DialogueRunner runner in dialogueRunnersInScene.Values)
            {
                if (!runner.IsDialogueRunning)
                {
                    StartConversation(runner);
                }
            }
        }
    }

    private void StartConversation(DialogueRunner runner)
    {
        isCurrentConversation = true;
        Debug.Log($"Starting conversation on runner '{runner.name}' with node '{conversationStartNode}'.");
        runner.StartDialogue(conversationStartNode);
    }
    private void EndConversation()
    {
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

