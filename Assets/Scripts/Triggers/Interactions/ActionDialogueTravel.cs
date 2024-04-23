using UnityEngine;

public class ActionDialogueTravel : SceneAction
{
    [SerializeField]
    private GameObject dialogue;
    public override void Interact()
    {
        Debug.Log("Dialogue Action");
        ButtonManager.canTravel = true;
        dialogue.SetActive(true);
    }
}
