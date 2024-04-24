using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeCanvasFollow : MonoBehaviour
{
    public Transform playerTransform; // Assign this in the inspector to the player's transform
    public Vector3 offset; // Adjust this offset in the inspector to position the canvas above the player's head

    private void Update()
    {
        if (playerTransform != null)
        {
            // Follow the player's position with the specified offset
            transform.position = playerTransform.position + offset;
        }
    }
}
