using System.Collections;
using UnityEngine;
using DG.Tweening;

public class ObjectActivator : MonoBehaviour
{
    private Vector3 originalScale;
    private GameObject childObject;
    private bool isDeactivating = false;

    private void Start()
    {
        childObject = transform.GetChild(0).gameObject;
        originalScale = childObject.transform.localScale;
        childObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDeactivating)
        {
            childObject.SetActive(true);
            childObject.transform.DOScale(originalScale, 0.5f)
                .SetEase(Ease.OutBack);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isDeactivating && childObject.activeSelf)
        {
            isDeactivating = true;
            childObject.transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => {
                    if (childObject != null)
                    {
                        childObject.SetActive(false);
                    }
                    isDeactivating = false;
                });
        }
    }


    private IEnumerator DeactivateAfterAnimation()
    {
        isDeactivating = true;

        childObject.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        Debug.Log("Object Activator: Starting scale down animation.");

        yield return new WaitForSeconds(0.6f);

        if (childObject.activeSelf)
        {
            childObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("child object was already inactive when trying to deactivate.");
        }

        isDeactivating = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

}
