using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] interactablesInScene;
    private GameObject activeObject;

    [SerializeField]
    List<GameObject> currentActives = new List<GameObject>();

    [SerializeField]
    private bool withinRange;

    void Update()
    {
        foreach (GameObject obj in interactablesInScene)
        {
            ObjectActivator script = obj.GetComponent<ObjectActivator>();
            withinRange = script.withinRange;
            if (script.withinRange == true)
            {
                activeObject = obj;
                DeactivateInteractables();
                StartCoroutine(waitForInteractable(script));
                ActivateInteractables();
                currentActives.Clear();
            }
        }
    }

    private void DeactivateInteractables()
    {
        foreach(GameObject obj in interactablesInScene)
        {
            if(obj != activeObject)
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

    private void ActivateInteractables()
    {
        foreach (GameObject obj in currentActives)
        {
            obj.SetActive(true);
        }
    }

    private IEnumerator waitForInteractable(ObjectActivator script)
    {
        yield return new WaitUntil(() => script.withinRange == false);
    }


}
