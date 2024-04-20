using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private void OnEnable()
    {
        item.SetActive(true);
    }

}
