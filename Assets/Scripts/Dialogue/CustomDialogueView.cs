using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CustomDialogueView : DialogueViewBase
{
    public Dictionary<string, DialogueRunner> dialogueRunnersInScene;

    public override void RunLine(LocalizedLine dialogueLine, System.Action onDialogueLineFinished)
    {
        string lineText = dialogueLine.Text.Text;
        string speakerName = GetSpeakerNameFromLine(lineText);

        if (!string.IsNullOrEmpty(speakerName) && dialogueRunnersInScene.TryGetValue(speakerName, out DialogueRunner runner))
        {
            Debug.Log($"DialogueRunner '{runner.name}' selected for speaker '{speakerName}'.");
        }
        else
        {
            Debug.LogWarning($"No DialogueRunner found for speaker '{speakerName}'. Using default.");
        }

        onDialogueLineFinished?.Invoke();
    }

    private string GetSpeakerNameFromLine(string lineText)
    {
        if (lineText.Contains(":"))
        {
            return lineText.Split(':')[0].Trim();
        }

        return null;
    }


    public override void DialogueStarted()
    {
        Debug.Log("Dialogue started!");
    }

    public override void DialogueComplete()
    {
        Debug.Log("Dialogue complete!");
    }
}

