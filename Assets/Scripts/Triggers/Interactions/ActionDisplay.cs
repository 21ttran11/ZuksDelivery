using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionDisplay : SceneAction
{
    public GameObject objectToDisplay;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = objectToDisplay.transform.localScale;
        EventBus.Publish(new EventData("Activate", objectToDisplay));
        objectToDisplay.transform.localScale = Vector3.zero;
    }

    public override void Interact()
    {
        EventBus.Publish(new EventData("PickupTriggered", objectToDisplay));
        objectToDisplay.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToDisplay.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => objectToDisplay.SetActive(false));
        }
    }
}
