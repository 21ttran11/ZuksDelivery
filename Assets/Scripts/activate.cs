using UnityEngine;

public class activate : StateMachineBehaviour
{
    private GameObject arm;
    private GameObject foot;

    public void Initialize(GameObject armObj, GameObject footObj)
    {
        arm = armObj;
        foot = footObj;
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (foot != null) foot.SetActive(true);
        if (arm != null) arm.SetActive(false);
    }
}