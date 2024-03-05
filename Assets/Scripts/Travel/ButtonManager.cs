using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public List<ButtonController> buttons;
    public float timeBetweenNotes = 1f;
    private List<int> sequence;
    private int sequenceIndex;
    private bool isSequencePlaying;
    private bool isPlayerTurn;
    private bool isSequenceCompleted = false;


    IEnumerator PlaySequence()
    {
        isSequencePlaying = true;
        isPlayerTurn = false;
        sequence = GenerateRandomSequence(4);

        // Display the sequence
        foreach (int buttonIndex in sequence)
        {
            buttons[buttonIndex].PressButton();
            yield return new WaitForSeconds(timeBetweenNotes);
            buttons[buttonIndex].ReleaseButton();
            yield return new WaitForSeconds(timeBetweenNotes);
        }

        isSequencePlaying = false;
        isPlayerTurn = true;
        sequenceIndex = 0;
    }

    List<int> GenerateRandomSequence(int length)
    {
        List<int> newSequence = new List<int>();
        for (int i = 0; i < length; i++)
        {
            newSequence.Add(Random.Range(0, buttons.Count));
        }
        return newSequence;
    }

    void Update()
    {
        if (isSequenceCompleted)
        {
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (!isSequencePlaying && !isPlayerTurn && !isSequenceCompleted)
                StartCoroutine(PlaySequence());
        }
        else
        {
            if ((isPlayerTurn || isSequencePlaying) && !isSequenceCompleted)
            {
                Debug.Log("D key released. Ending the current sequence.");
                StopAllCoroutines();
                StopAllBlinking();
                isPlayerTurn = false;
                isSequencePlaying = false;

                sequenceIndex = 0;
            }
            return;
        }

        if (isSequencePlaying)
        {
            Debug.Log("Sequence is playing, wait for your turn.");
            return;
        }

        if (isPlayerTurn)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (Input.GetKeyDown(buttons[i].keyToPress))
                {
                    Debug.Log("You pressed a key corresponding to a button.");

                    buttons[i].PressButton();

                    if (sequence[sequenceIndex] == i)
                    {
                        Debug.Log($"Correct button! You pressed button {i}.");
                        sequenceIndex++;
                        if (sequenceIndex >= sequence.Count)
                        {
                            Debug.Log("Sequence complete. You've successfully completed the challenge!");
                            isPlayerTurn = false;
                            isSequenceCompleted = true;
                        }
                    }
                    else
                    {
                        Debug.Log($"Wrong button! Reminding you to press the correct button.");
                        // Highlight the correct button for the player
                        buttons[sequence[sequenceIndex]].HighlightButton();
                    }

                    StartCoroutine(ReleaseButtonAfterDelay(buttons[i]));
                }
            }
        }
    }

    private void StopAllBlinking()
    {
        foreach (var button in buttons)
        {
            if (button.IsBlinking)
            {
                button.UnhighlightButton();
            }
        }
    }


    IEnumerator ReleaseButtonAfterDelay(ButtonController button)
    {
        yield return new WaitForSeconds(timeBetweenNotes);
        button.ReleaseButton();
    }
}
