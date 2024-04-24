using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeOnAwake : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(sceneName);
    }
}
