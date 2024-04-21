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

    public override void Interact()
    {
        StartCoroutine(DelayedSceneLoad(sceneName, delayInSeconds));
    }

    private IEnumerator DelayedSceneLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
