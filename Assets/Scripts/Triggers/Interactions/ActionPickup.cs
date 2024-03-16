using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPickup : SceneAction
{
    public override void Interact()
    {
        /* We need like a player inventory or something to keep track
         of the delivery the player has or a quest checker or something idk */

        Debug.Log("Delivery has been picked up");
    }
}
