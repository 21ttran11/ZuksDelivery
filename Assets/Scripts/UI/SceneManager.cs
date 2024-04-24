using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public Animator animator;

    public string animationStringName = "";

    public void LoadSceneWithDelay()
    {
        Debug.Log("button clicekd");
        StartCoroutine(LoadSceneAfterAnimation());
    }

    private IEnumerator LoadSceneAfterAnimation()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not set on the object", this);
            yield break;
        }

        animator.Play(animationStringName);

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene(sceneName);
    }
}