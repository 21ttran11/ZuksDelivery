using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectActivator : MonoBehaviour
{
    private Vector3 originalScale;
    private GameObject childObject;

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
        originalScale = childObject.transform.localScale;
        childObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        childObject.SetActive(true);
        childObject.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBack);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartCoroutine(DeactivateAfterAnimation());
    }

    private IEnumerator DeactivateAfterAnimation()
    {
        childObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

        yield return new WaitForSeconds(0.5f);

        childObject.SetActive(false);
    }
}
