using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionDisplay : SceneAction
{
    public GameObject objectToDisplay;
    private Vector3 originalScale;
    private bool displaying = false;

    private void Start()
    {
        originalScale = objectToDisplay.transform.localScale;
        objectToDisplay.transform.localScale = Vector3.zero;
    }

    public override void Interact()
    {
        //EventBus.Publish(new EventData("PauseObjects", this.gameObject));
        EventBus.Publish(new EventData("Activate", objectToDisplay));
        objectToDisplay.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
        displaying = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && displaying == true)
        {
            objectToDisplay.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => EventBus.Publish(new EventData("Deactivate", objectToDisplay)));
            displaying = false;
            //EventBus.Publish(new EventData("UnpauseObjects", objectToDisplay));
        }
    }
}
