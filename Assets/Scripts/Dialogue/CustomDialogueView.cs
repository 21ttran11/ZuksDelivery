using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;


public class CustomDialogueView : DialogueViewBase
{
    [SerializeField]
    public Dictionary<string, DialogueRunner> dialogueRunnersInScene = new Dictionary<string, DialogueRunner>();

    public static event Action reactivate;
    public void Awake()
    {
        dialogueRunnersInScene = FindObjectsOfType<DialogueRunner>().ToDictionary(runner => runner.name, runner => runner);
    }

    public override void RunLine(LocalizedLine dialogueLine, System.Action onDialogueLineFinished)
    {
        string lineText = dialogueLine.Text.Text;
        string speakerName = GetSpeakerNameFromLine(lineText);

        if (!string.IsNullOrEmpty(speakerName) && dialogueRunnersInScene.TryGetValue(speakerName, out DialogueRunner activeRunner))
        {
            Debug.Log($"Speaker: {speakerName}, Line: {lineText}");

            foreach (var runner in dialogueRunnersInScene.Values)
            {
                if (runner.name != speakerName)
                {
                    runner.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogWarning($"No DialogueRunner found for speaker '{speakerName}'.");
        }

        reactivate.Invoke();
    }

    private string GetSpeakerNameFromLine(string lineText)
    {
        if (lineText.Contains(":"))
        {
            return lineText.Split(':')[0].Trim();
        }

        return null;
    }
}


