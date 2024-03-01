using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    [SerializeField] private SceneAction sceneAction = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            sceneAction.Interact();
            this.gameObject.SetActive(false);
        }

    }
}
