using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    void OnEnable()
    {
        item.SetActive(false);
    }
}
