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
        AudioManager.PlaySFX("s_click0" + Random.Range(0, 2), 1.0f);
        StartCoroutine(LoadSceneAfterAnimation());
    }

    private IEnumerator LoadSceneAfterAnimation()
    {
        if (animator == null)
        {
            Debug.LogError("Animator component not set on the object", this);
            yield break;
        }

        if(AudioManager.instance != null)
        {
            AudioManager.StopMusic();
            AudioManager.PlaySFX("s_exit", 1.0f);
        }

        animator.Play(animationStringName);

        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene(sceneName);
    }

}