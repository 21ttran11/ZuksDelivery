using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] lockersArray;
    public GameObject[] dialogueArray;
    public GameObject[] stealableItemsArray;
    public GameObject[] stealDialogueLines;

    private GameObject itemClicked;
    private GameObject dialogue;
    private GameObject stealDialouge;

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
                CheckLocker(i);
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

        for (int i = 0; i < stealableItemsArray.Length; i++)
        {
            if (itemClicked == stealableItemsArray[i] && itemClicked.CompareTag("steal"))
            {
                Debug.Log("Stealable item clicked: " + stealableItemsArray[i].name);
                itemClicked.SetActive(false);
                stealDialouge = stealDialogueLines[i];
                if (isDialogueActive && currentDialogueCoroutine != null)
                {
                    StopCoroutine(currentDialogueCoroutine);
                    SetAllDialoguesInactive();
                }
                StartCoroutine(DelayShowStealDialouge(stealDialouge));
                break;
            }
        }
    }

    private void CheckLocker(int index)
    {
        if(index == 0)
            stealableItemsArray[0].gameObject.SetActive(true);
        if (index == 1)
            stealableItemsArray[1].gameObject.SetActive(true);
        if(index == 3)
            stealableItemsArray[2].gameObject.SetActive(true);
        if(index == 5)
            stealableItemsArray[3].gameObject.SetActive(true);
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

    IEnumerator DelayShowStealDialouge(GameObject dialogueToShow)
    {
        isDialogueActive = true;
        dialogueToShow.SetActive(true);
        yield return new WaitForSeconds(1f);
        dialogueToShow.SetActive(false);
        isDialogueActive = false;
    }

}
