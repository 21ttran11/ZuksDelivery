using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] interactablesInScene;

    private List<GameObject> currentActives = new List<GameObject>();

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
            case "PauseObjects":
                PauseObjects(eventData);
                break;
            case "UnpauseObjects":
                UnpauseObjects(eventData);
                break;
        }
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

    private void PauseObjects(EventData eventData)
    {
        GameObject activeObject = eventData.Data as GameObject;
        foreach (GameObject obj in interactablesInScene)
        {
            if (obj != activeObject)
            {
                if (obj.activeInHierarchy)
                {
                    obj.SetActive(false);
                    if (!(currentActives.Contains(obj)))
                    {
                        currentActives.Add(obj);
                    }
                }
            }
        }
    }

    private void UnpauseObjects(EventData eventData)
    {
        foreach (GameObject obj in currentActives)
        {
            obj.SetActive(true);
        }
    }
}
