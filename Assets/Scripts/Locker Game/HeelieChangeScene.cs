using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class HeelieChangeScene : MonoBehaviour
{
    public Animator heelieFall;
    public Animator cameraPan;

    private Camera mainCamera;

    [SerializeField]
    private GameObject heelies;

    [SerializeField]
    private GameObject roaches;
    private GameObject itemClicked;

    [SerializeField]
    private GameObject droppedDialogue;

    private bool fell = false;

    private void OnEnable()
    {
        Debug.Log("Heelies are now available");
    }
    private void Update()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        itemClicked = rayHit.collider.gameObject;
        CheckClicked();
    }

    private void CheckClicked()
    {
        if (itemClicked == heelies)
        {
            Debug.Log("Heelies clicked from heelies");
            Fall();
            roaches.SetActive(true);
            heelies.SetActive(false);
        }
        else return;
    }

    private void Fall()
    {
        if (fell == false)
        {
            heelieFall.SetBool("Falling", true);
            droppedDialogue.SetActive(true);
            fell = true;
            CameraPan();
        }

        else if (fell == true) 
        {
            SceneManager.LoadScene("PostOffice2");
        }
    }

    private void CameraPan()
    {
        cameraPan.SetBool("Panning", true);
    }
}
