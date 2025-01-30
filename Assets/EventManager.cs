using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.Subscribe("Activate", Activate);
        EventBus.Subscribe("Deactivate", Deactivate);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("Activate", Activate);
        EventBus.Unsubscribe("Deactivate", Deactivate);
    }

    private void Activate(EventData eventData)
    {
        if (eventData.Data is GameObject singleObject)
        {
            Debug.Log(singleObject + " Activated");
            singleObject.SetActive(true);
        }

        if (eventData.Data is List<GameObject> objectList)
        {
            foreach (var obj in objectList)
            {
                Debug.Log(obj + " Activated");
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
}
