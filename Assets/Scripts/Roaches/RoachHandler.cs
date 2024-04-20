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

    public HeelieChangeScene heelieChangeScene;

    public LayerMask clickableLayer;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Debug.Log(roachKilled);
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, clickableLayer);
        if (!rayHit.collider) return;

        Debug.Log("Clicked on object: " + rayHit.collider.gameObject.name + " with tag: " + rayHit.collider.gameObject.tag);
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
        if (roach != null)
        {
            Animator animator = roach.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            // Deactivate the collider component
            BoxCollider2D collider = roach.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    private void CheckHeelies()
    {
        if (roachKilled == 4)
        {
            heelieChangeScene.AllRoachesKilled();
        }
    }
}
