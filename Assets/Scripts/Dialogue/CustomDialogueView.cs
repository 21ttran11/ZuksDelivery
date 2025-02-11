using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Yarn.Unity;

public class CustomDialogueView : DialogueViewBase
{
    [SerializeField]
    public Dictionary<string, DialogueRunner> dialogueRunnersInScene = new Dictionary<string, DialogueRunner>();

    public void Awake()
    {
        dialogueRunnersInScene = FindObjectsOfType<DialogueRunner>().ToDictionary(runner => runner.name, runner => runner);
    }
    public override void RunLine(LocalizedLine dialogueLine, System.Action onDialogueLineFinished)
    {
        string lineText = dialogueLine.Text.Text;
        string speakerName = GetSpeakerNameFromLine(lineText);
        Debug.Log($"Speaker: {speakerName}, Line: {lineText}");

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
}

