using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSceneChange : SceneAction
{
    [SerializeField]
    private string sceneName;
    [SerializeField]
    private float delayInSeconds = 0.5f;

    public Animator animator;
    public string animationStringName;

    public override void Interact()
    {
        StartCoroutine(DelayedSceneLoad(sceneName, delayInSeconds));
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        if (animator != null)
            animator.Play(animationStringName);

        if (AudioManager.instance != null)
        {

            AudioManager.StopMusic();
            AudioManager.PlaySFX("s_exit", 1.0f);
        }

        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
