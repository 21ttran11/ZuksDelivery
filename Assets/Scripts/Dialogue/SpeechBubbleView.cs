using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using TMPro;
using UnityEngine;
using Yarn.Unity;

public class SpeechBubbleView : LineView
{
    public GameObject speechBubble;
    public Vector3 bubbleOffset;
    public TextMeshProUGUI dialogue;
    public string textLine;

    public string speakerName;
    public Material bubbleMaterial;
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
        if (bubbleMaterial != null)
        {
            Debug.Log("Changing speech bubble material");
            Renderer renderer = this.gameObject.GetComponentInChildren<Renderer>();
            renderer.material = bubbleMaterial;
        }
        else
        {
            bubbleOffset = Vector3.zero;
        }
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        textLine = dialogueLine.Text.Text;
        speakerName = ParseName(textLine);

        if (speakerName != null)
        {
            foreach(Character c in characters)
            {
                if(c.characterName == speakerName)
                {
                    speakerTransform = c.transform;
                    bubbleOffset = c.offset;
                    bubbleMaterial = c.characterMaterial;
                }
            }
        }

        dialogue.text = dialogueLine.Text.ToString();
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
