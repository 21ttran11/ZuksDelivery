using UnityEngine;

public class ActionDialogue : SceneAction
{
    public override void Interact()
    {
        Debug.Log("Dialogue Action");
        ButtonManager.canTravel = true;
    }


}
