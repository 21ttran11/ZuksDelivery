using UnityEngine;

public class ActionDialogueTravel : SceneAction
{
    public override void Interact()
    {
        Debug.Log("Dialogue Action");
        ButtonManager.canTravel = true;
    }
}
