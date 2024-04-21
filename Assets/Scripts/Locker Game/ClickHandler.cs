using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lockersArray;
    public GameObject[] dialogueArray;
    public GameObject[] stealableItemsArray;

    private GameObject itemClicked;
    private GameObject dialogue;

    private Camera mainCamera;
    private bool isDialogueActive = false;
    private Coroutine currentDialogueCoroutine;

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
            if (itemClicked == lockersArray[i])
            {
                Debug.Log("Locker matched: " + lockersArray[i].name);
                dialogue = dialogueArray[i];
                if (isDialogueActive && currentDialogueCoroutine != null)
                {
                    StopCoroutine(currentDialogueCoroutine);
                    SetAllDialoguesInactive();
                }
                DeactivateLocker();
                break;
            }
        }
    }

    private void DeactivateLocker()
    {
        itemClicked.SetActive(false);
        Debug.Log("Opening Locker");
        currentDialogueCoroutine = StartCoroutine(DelayShowDialogue(dialogue));
    }

    private void SetAllDialoguesInactive()
    {
        foreach (var dlg in dialogueArray)
        {
            dlg.SetActive(false);
        }
    }

    IEnumerator DelayShowDialogue(GameObject dialogueToShow)
    {
        isDialogueActive = true;
        dialogueToShow.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogueToShow.SetActive(false);
        isDialogueActive = false;
    }

}
