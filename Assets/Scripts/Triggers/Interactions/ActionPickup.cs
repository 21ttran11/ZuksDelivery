using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionPickUp : ActionDisplay
{
    [SerializeField]
    private List<GameObject> progressionObjects;
    [SerializeField]
    private List<GameObject> pastObjects;

    private int opened = 0;

    public override void Interact()
    {
        if(opened == 0 && interactable)
        {
            EventBus.Publish(new EventData("Deactivate", pastObjects));
            EventBus.Publish(new EventData("Activate", progressionObjects));
            opened++;
        }

        EventBus.Publish(new InteractionEventData(true, this.gameObject));
        EventBus.Publish(new EventData("Activate", objectToDisplay));
        Display();
    }
}