using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionPickUp : SceneAction
{
    public GameObject objectToDisplay;
    private Vector3 originalScale;


    [SerializeField]
    private GameObject unlockedInteraction;

    private void Start()
    {
        originalScale = objectToDisplay.transform.localScale;

        objectToDisplay.SetActive(false);
        objectToDisplay.transform.localScale = Vector3.zero;
    }

    public override void Interact()
    {
        objectToDisplay.SetActive(true);
        objectToDisplay.transform.DOScale(originalScale, 0.3f).SetEase(Ease.OutBack);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            objectToDisplay.transform.DOScale(Vector3.zero, 0.3f)
                .SetEase(Ease.InBack)
                .OnComplete(() => objectToDisplay.SetActive(false));
            unlockedInteraction.SetActive(true);
        }
    }
}