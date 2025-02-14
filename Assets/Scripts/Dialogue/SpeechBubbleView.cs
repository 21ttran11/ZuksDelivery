using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class SpeechBubbleView : LineView
{
    public GameObject speechBubble;
    public Vector3 bubbleOffset;
    public TextMeshProUGUI dialogue;
    public string textLine;

    public List<Transform> characters = new List<Transform>();

    public string speakerName;
    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        textLine = dialogueLine.Text.Text;
        speakerName = ParseName(textLine);
        Transform speakerTransform = null;

        if (speakerName != null)
        {
            foreach(Transform t in characters)
            {
                if(t.name == speakerName)
                {
                    speakerTransform = t;
                }
            }
        }

        if (speakerTransform != null)
        {
            speechBubble.transform.position = speakerTransform.position + bubbleOffset;
            dialogue.text = dialogueLine.Text.ToString();
        }

        speechBubble.SetActive(true);
        base.RunLine(dialogueLine, onDialogueLineFinished);
    }

    public string ParseName(string lineText)
    {
        if (lineText.Contains(":"))
        {
            return lineText.Split(':')[0].Trim();
        }

        return null;
    }
}
