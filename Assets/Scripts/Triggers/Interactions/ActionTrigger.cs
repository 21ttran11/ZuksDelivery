using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField] private SceneAction sceneAction = null;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sceneAction.Interact();

            AnimateDeactivation();
        }
    }

    private void OnDisable()
    {
        transform.localScale = Vector3.zero;
    }

    private void AnimateDeactivation()
    {
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => gameObject.SetActive(false));
    }
}
