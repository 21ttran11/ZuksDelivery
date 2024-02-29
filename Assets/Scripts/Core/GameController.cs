using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Interact, Travel }
public class GameController : MonoBehaviour
{
    // controller informs game state
    [SerializeField] PlayerController playerController;

    GameState state;

    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            
        }
        else if (state == GameState.Interact)
        {

        }
        else if (state == GameState.Travel) { 
        }
    }
}
