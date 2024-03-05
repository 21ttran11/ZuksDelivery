using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Image buttonImage;
    private bool isPressed = false;
    public Sprite defaultImage;
    public Sprite pressedImage;
    public KeyCode keyToPress;
    private Coroutine blinkingCoroutine;

    public bool IsBlinking { get; private set; }

    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void PressButton()
    {
        buttonImage.sprite = pressedImage;
        isPressed = true;
        if (IsBlinking)
        {
            UnhighlightButton(); 
        }
    }


    public void ReleaseButton()
    {
        buttonImage.sprite = defaultImage;
        isPressed = false;
    }

    public bool IsPressed()
    {
        return isPressed;
    }

    public void HighlightButton()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
        }
        blinkingCoroutine = StartCoroutine(BlinkButtonEffect());
    }

    IEnumerator BlinkButtonEffect()
    {
        IsBlinking = true;

        buttonImage.sprite = pressedImage;

        while (true)
        {
            // Calculate the current alpha value between 0.2 and 1.0
            float alphaValue = Mathf.PingPong(Time.time * 2, 0.8f) + 0.2f;
            Color currentColor = buttonImage.color;
            currentColor.a = alphaValue;
            buttonImage.color = currentColor;

            yield return null;
        }
    }

    public void UnhighlightButton()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = null;
        }
        IsBlinking = false;
        buttonImage.color = Color.white;
        buttonImage.sprite = defaultImage;
    }

    public void ResetToDefaultState()
    {
        buttonImage.sprite = defaultImage;
        isPressed = false;
    }
}

