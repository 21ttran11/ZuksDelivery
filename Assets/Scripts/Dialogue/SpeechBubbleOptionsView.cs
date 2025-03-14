using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Yarn.Unity;
using UnityEngine.UI;

public class SpeechBubbleOptionsView : SpeechBubbleView
{
    [Header("Options Configuration")]
    [SerializeField] private bool showUnavailableOptions = false;

    private DialogueOption[] currentOptions;
    private int currentOptionIndex = 0;
    private Action<int> onOptionSelected;

    private void Update()
    {
        // Handle input for navigating and selecting options
        if (currentOptions != null && currentOptions.Length > 0)
        {
            HandleInput();
        }

        if (speakerTransform != null && bubbleOffset != null)
        {
            speechBubble.transform.position = speakerTransform.position + bubbleOffset;
        }
    }


    public override void RunOptions(DialogueOption[] dialogueOptions, Action<int> onOptionSelected)
    {
        Debug.Log("RunOptions called with " + dialogueOptions.Length + " options.");
        this.currentOptions = dialogueOptions;
        this.onOptionSelected = onOptionSelected;

        currentOptionIndex = 0;
        ShowCurrentOption();
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        onDialogueLineFinished();
    }

    public override void DismissLine(Action onDismissalComplete)
    {
        base.DismissLine(onDismissalComplete);
    }

    private void ShowCurrentOption()
    {
        if (currentOptions == null || currentOptions.Length == 0)
        {
            speechBubble.SetActive(false);
            return;
        }

        var currentOption = currentOptions[currentOptionIndex];

        if (!currentOption.IsAvailable && !showUnavailableOptions)
        {
            MoveToNextOption();
            return;
        }

        speakerName = ParseName(currentOption.Line.Text.Text);
        if (speakerName != null && characters != null)
        {
            base.updateBubble();
        }
        else
        {
            Debug.LogWarning("speakerName is null or characters array is not populated.");
        }

        dialogue.text = currentOption.Line.TextWithoutCharacterName.Text;

        speechBubble.SetActive(true);
    }

    private void HandleInput()
    {
        var keyboard = Keyboard.current;

        if (keyboard.rightArrowKey.wasPressedThisFrame)
        {
            MoveToNextOption();
        }
        else if (keyboard.leftArrowKey.wasPressedThisFrame)
        {
            MoveToPreviousOption();
        }
        else if (keyboard.qKey.wasPressedThisFrame)
        {
            SelectCurrentOption();
        }
    }

    private void MoveToNextOption()
    {
        currentOptionIndex = (currentOptionIndex + 1) % currentOptions.Length;
        ShowCurrentOption();
    }

    private void MoveToPreviousOption()
    {
        currentOptionIndex = (currentOptionIndex - 1 + currentOptions.Length) % currentOptions.Length;
        ShowCurrentOption();
    }

    private void SelectCurrentOption()
    {
        onOptionSelected?.Invoke(currentOptionIndex);

        currentOptions = null;
        currentOptionIndex = 0;
        speechBubble.SetActive(false);
    }
}