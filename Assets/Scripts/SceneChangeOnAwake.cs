using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnAwake : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public Animator animator;

    public string animationStringName = "";

    private void Awake()
    {
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

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        SceneManager.LoadScene(sceneName);
    }
}
