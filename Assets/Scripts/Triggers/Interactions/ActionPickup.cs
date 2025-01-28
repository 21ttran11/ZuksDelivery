using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionPickUp : SceneAction
{
    public GameObject objectToDisplay;
    private Vector3 originalScale;

    [SerializeField]
    private List<GameObject> progressionObjects;
    [SerializeField]
    private List<GameObject> pastObjects;

    private int opened = 0;

    private void Start()
    {
        originalScale = objectToDisplay.transform.localScale;
        objectToDisplay.transform.localScale = Vector3.zero;
    }

    public override void Interact()
    {
        if(opened == 0)
        {
            EventBus.Publish(new EventData("Deactivate", pastObjects));
            EventBus.Publish(new EventData("Activate", progressionObjects));
            opened++;
        }

        EventBus.Publish(new EventData("Activate", objectToDisplay));
        objectToDisplay.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToDisplay.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => EventBus.Publish(new EventData("Deactivate", objectToDisplay)));
        }
    }
}