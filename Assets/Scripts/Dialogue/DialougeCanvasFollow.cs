using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeCanvasFollow : MonoBehaviour
{
    public Transform playerTransform; 
    public Vector3 offset;

    private void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }
}
