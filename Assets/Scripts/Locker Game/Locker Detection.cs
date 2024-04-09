using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerDetection : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lockersArray;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject gameObjectHit = hit.transform.gameObject;

                foreach (GameObject obj in lockersArray)
                {
                    if (gameObjectHit == obj)
                    {
                        Debug.Log("Player clicked on " + obj.name);
                        break;
                    }
                }
            }
        }
    }
}
