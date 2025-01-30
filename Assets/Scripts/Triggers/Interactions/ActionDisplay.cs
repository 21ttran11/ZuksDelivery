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
        if (interactable)
        {
            EventBus.Publish(new InteractionEventData(true, this.gameObject));
            EventBus.Publish(new EventData("Activate", objectToDisplay));
        }
    }

    public void Display()
    {
        objectToDisplay.transform.DOScale(originalScale, 0.3f)
            .SetEase(Ease.OutBack);

        displaying = true;
    }
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && displaying == true)
        {
            objectToDisplay.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => EventBus.Publish(new EventData("Deactivate", objectToDisplay)));
            displaying = false;
            EventBus.Publish(new InteractionEventData(false, this.gameObject));
        }
    }
}
