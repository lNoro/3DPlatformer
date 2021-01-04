using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : StateMachineBehaviour
{
    private Rigidbody m_Rigidbody;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Rigidbody = animator.GetComponentInParent<Rigidbody>();
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.enabled = false;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
    }
}
