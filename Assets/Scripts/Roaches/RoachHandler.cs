using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoachHandler : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField]
    private GameObject dialogue;
    private GameObject itemClicked;
    private GameObject roach;

    [SerializeField]
    private GameObject heelies;
    private int roachKilled = 0;

    public Sprite deadRoach;
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;

        Debug.Log("roach!");
        itemClicked = rayHit.collider.gameObject;
        CheckClicked();
    }

    private void CheckClicked()
    {
        if (itemClicked.CompareTag("roach"))
        {
            roach = itemClicked;
            Debug.Log("Roach clicked!!");
            dialogue.SetActive(true);
            StopRoach();
            ChangeRoachSprite();
            roachKilled += 1;
            CheckHeelies();
        }
        else return;
    }

    private void ChangeRoachSprite()
    {
        if (roach != null && deadRoach != null)
        {
            SpriteRenderer roachSpriteRender = roach.GetComponent<SpriteRenderer>();
            if (roachSpriteRender != null)
            {
                roachSpriteRender.sprite = deadRoach;
            }
        }
    }

    private void StopRoach()
    {
        if ( roach != null)
        {
            Animator animator = roach.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }
        }
    } 

    private void CheckHeelies()
    {
        if (roachKilled == 4)
        {
            heelies.SetActive(true);
        }
    }
}
