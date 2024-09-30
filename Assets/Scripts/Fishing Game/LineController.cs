using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField]
    private float duration = 0.7f;

    private float scaleFactor = 0.5f;

    private Vector2 minSize;

    private void Start()
    {
        minSize = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currentScale = transform.localScale;

        //scaling from one side (bottom half of y)
        //scale y up/down, position moves half as much as scaling
        if (Input.GetKey(KeyCode.DownArrow))
        {
            StartCoroutine(FishingLineExtend(duration, currentScale));
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if(transform.localScale.y > minSize.y)
            {
                StartCoroutine(FishingLineShorten(duration, currentScale));
            }
        }


    }

    private IEnumerator FishingLineShorten(float duration, Vector2 currentScale)
    {
        var elapsed = 0f;
        var startScale = transform.localScale;
        currentScale.y -= scaleFactor;
        var endScale = currentScale;

        while(elapsed < duration)
        {
            var t = elapsed / duration;
            transform.localScale = Vector2.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }

    private IEnumerator FishingLineExtend(float duration, Vector2 currentScale)
    {
        var elapsed = 0f;
        var startScale = transform.localScale;
        currentScale.y += scaleFactor;
        var endScale = currentScale;

        while(elapsed < duration)
        {
            var t = elapsed / duration;
            transform.localScale = Vector2.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localScale = endScale;
    }

}