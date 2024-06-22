using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisibleCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
    }
}