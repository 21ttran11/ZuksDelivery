using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionSceneChange : SceneAction
{
    [SerializeField]
    private string sceneName;
    public override void Interact()
    {
        LoadScene(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
