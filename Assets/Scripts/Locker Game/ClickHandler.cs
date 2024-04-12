using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lockersArray;
    public GameObject[] dialogueArray;
    private GameObject itemClicked;
    private GameObject dialogue;

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
        itemClicked = rayHit.collider.gameObject;
        CheckClicked();
    }

    public void CheckClicked()
    {
        for (int i = 0; i < lockersArray.Length; i++)
        {
            if(itemClicked == lockersArray[i])
            {
                Debug.Log("Locker matched: " + lockersArray[i].name);
                dialogue = dialogueArray[i];
                DeactivateLocker();
                break;
            }
        }
    }

    private void DeactivateLocker()
    {
        itemClicked.SetActive(false);
        Debug.Log("Opening Locker");
        StartDelay();
    }

    private void StartDelay()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        ActivateDialogue();
    }

    private void ActivateDialogue()
    {
        if (dialogue == null) return;
        dialogue.SetActive(true);
    }
}
