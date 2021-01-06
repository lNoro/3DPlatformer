using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Reduce Players RunVelocity while Landing Animation is played 
 */
public class Landing : StateMachineBehaviour
{
    private float m_RunVelocityOrig;

    private float m_RunVelocityLanding;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_RunVelocityOrig = animator.GetComponentInParent<PlayerController>().MoveSetting.RunVelocity;
        m_RunVelocityLanding = m_RunVelocityOrig / 1.5f;
        animator.GetComponentInParent<PlayerController>().MoveSetting.RunVelocity = m_RunVelocityLanding;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.GetComponentInParent<PlayerController>().MoveSetting.RunVelocity = m_RunVelocityOrig;
    }
}
