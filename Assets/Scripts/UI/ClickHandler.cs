using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lockersArray;
    private GameObject lockerClicked;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log(rayHit.collider.gameObject.name);
        lockerClicked = rayHit.collider.gameObject;
        CheckLocker();
    }

    public void CheckLocker()
    {
        for (int i = 0; i < lockersArray.Length; i++)
        {
            if(lockerClicked == lockersArray[i])
            {
                Debug.Log("Locker matched: " + lockersArray[i].name);
                DeactivateLocker();
            }
        }
    }

    public void DeactivateLocker()
    {
        lockerClicked.SetActive(false);
        Debug.Log("Opening Locker");
    }
}
