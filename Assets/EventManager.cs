using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.OnEventTriggered += HandleEvent; // subscribe event
    }

    private void OnDisable()
    {
        EventBus.OnEventTriggered -= HandleEvent; // unsubscribe event
    }

    private void HandleEvent(EventData eventData)
    {
        switch (eventData.EventName)
        {
            case "Activate":
                Activate(eventData);
                break;
            case "Deactivate":
                Deactivate(eventData);
                break;
            case "Travel":
                Travel(eventData);
                break;
        }
    }

    private void Activate(EventData eventData)
    {
        if (eventData.Data is GameObject singleObject)
        {
            singleObject.SetActive(true);
        }

        if (eventData.Data is List<GameObject> objectList)
        {
            foreach (var obj in objectList)
            {
                obj.SetActive(true);
            }
        }

        else
        {
            Debug.Log("No activatable");
        }
    }

    private void Deactivate(EventData eventData)
    {
        if (eventData.Data is GameObject singleObject)
        {
            singleObject.SetActive(false);
        }

        if (eventData.Data is List<GameObject> objectList)
        {
            foreach (var obj in objectList)
            {
                obj.SetActive(false);
            }
        }

        else
        {
            Debug.Log("No deactivatable");
        }
    }

    private void Travel(EventData eventData)
    {
        
    }
}
