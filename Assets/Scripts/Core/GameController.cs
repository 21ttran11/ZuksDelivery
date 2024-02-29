using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { FreeRoam, Dialog, Interact }
public class GameController : MonoBehaviour
{
    // controller informs game state
    [SerializeField] PlayerController playerController;
}
