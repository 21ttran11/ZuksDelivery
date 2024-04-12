using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoachHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject[] roachArray;
    [SerializeField]
    public GameObject heelies;

    [SerializeField]
    public Animator[] roachAnimation;
    private bool paused = false;
    private float speed;
    private Animator roach;

    [SerializeField]
    public Sprite deadSprite;

    [SerializeField]
    private SpriteRenderer[] spriteRenderer;
    private Sprite currentRoach;

    private GameObject dialogue;

    private Camera mainCamera;

    private GameObject itemClicked;

    private int roachesKilled = 0;

    private void Awake()
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
        for (int i = 0; i < roachArray.Length; i++)
        {
            if (itemClicked == roachArray[i])
            {
                Debug.Log("RoachHandler" + roachArray[i].name);
                roach = roachAnimation[i];
                spriteRenderer[i].sprite = deadSprite;
                Roach();
                break;
            }
        }
    }

    private void Roach()
    {
        PauseAnimation();
        roachesKilled++;
        CheckKilled();
    }

    private void PauseAnimation()
    {
        speed = roach.speed;
        roach.speed = 0;
    }


    private void CheckKilled()
    {
        if (roachesKilled == 4)
        {
            heelies.SetActive(true);
        }
    }
}
