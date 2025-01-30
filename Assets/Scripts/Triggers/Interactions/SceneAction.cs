using UnityEngine;

public abstract class SceneAction : MonoBehaviour
{
    [SerializeField] private Sprite actionIcon = null;
    public virtual bool interactable { get; set; } = true;
    public virtual GameObject currentlyActivated { get; set; }

    private void OnEnable()
    {
        EventBus.Subscribe("InteractionEvent", OnInteractionEvent);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe("InteractionEvent", OnInteractionEvent);
    }

    private void OnInteractionEvent(EventData eventData)
    {
        if (eventData is InteractionEventData interactionData)
        {
            if (interactionData.Source != this.gameObject && !this.transform.IsChildOf(interactionData.Source.transform))
            {
                interactable = !interactionData.IsInteracting;
            }
        }
    }

    public abstract void Interact();

    public Sprite GetActionIcon()
    {
        return actionIcon;
    }

}