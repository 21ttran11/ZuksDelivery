using UnityEngine;

public class StateMachineBehaviourManager : MonoBehaviour
{
    public GameObject arm;
    public GameObject foot;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        InitializeStateMachineBehaviours();
    }

    void InitializeStateMachineBehaviours()
    {
        foreach (var behaviour in animator.GetBehaviours<activate>())
        {
            behaviour.Initialize(arm, foot);
        }
    }
}
