using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoachHandler : MonoBehaviour
{
    private Camera mainCamera;
    private GameObject itemClicked;
    private GameObject roach;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        itemClicked = rayHit.collider.gameObject;
        CheckClicked();
    }

    private void CheckClicked()
    {
        if (itemClicked.CompareTag("roach"))
        {
            roach = itemClicked;
            Debug.Log("Roach clicked!!");
        }
        else return;
    }
}
