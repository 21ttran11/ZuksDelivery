using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvisibleCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = true;
        }
    }
}