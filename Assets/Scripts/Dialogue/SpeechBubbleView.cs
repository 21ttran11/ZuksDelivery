using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TMPro;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class SpeechBubbleView : LineView
{
    public GameObject speechBubble;
    public Vector3 bubbleOffset;
    public TextMeshProUGUI dialogue;
    public string textLine;

    public Image bubbleImage;
    public Image pointerImage;
    public string bubbleColorCode;

    public string speakerName;
    public Character[] characters;
    public Transform speakerTransform = null;

    public void Awake()
    {
        characters = FindObjectsOfType<Character>();
    }

    private void Update()
    {
        if (speakerTransform != null && bubbleOffset != null)
        {
            speechBubble.transform.position = speakerTransform.position + bubbleOffset;
        }

        else
        {
            bubbleOffset = Vector3.zero;
        }

        if (ColorUtility.TryParseHtmlString(bubbleColorCode, out Color newColor))
        {
            if (bubbleImage != null)
            {
                bubbleImage.color = newColor;
            }
            if (pointerImage != null)
            {
                pointerImage.color = newColor;
            }
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        textLine = dialogueLine.Text.Text;
        speakerName = ParseName(textLine);

        updateBubble();

        dialogue.text = dialogueLine.Text.ToString();
        speechBubble.SetActive(true);

        base.RunLine(dialogueLine, onDialogueLineFinished);
    }

    public void updateBubble()
    {
        if (speakerName != null)
        {
            foreach (Character c in characters)
            {
                if (c.characterName == speakerName)
                {
                    speakerTransform = c.transform;
                    bubbleOffset = c.offset;
                    bubbleColorCode = c.color;
                }
            }
        }
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
