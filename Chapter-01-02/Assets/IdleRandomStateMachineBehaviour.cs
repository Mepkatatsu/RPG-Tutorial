using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    #region Variables
    public int _numberOfStates = 2;
    public float _minNormalTime = 0f;
    public float _maxNormalTime = 5f;

    public float _randomNormalTime;

    readonly int _hashRandomIdle = Animator.StringToHash("RandomIdle");
    #endregion Variable
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Randomly decide a time at which to transition.
        _randomNormalTime = Random.Range(_minNormalTime, _maxNormalTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.IsInTransition(0) -> 현재 Base Layer에 있을 때

        // If transitioning away from this state reset the random idle parameter to -1.
        if (animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash == stateInfo.fullPathHash)
        {
            animator.SetInteger(_hashRandomIdle, -1);
        }

        // If the state is beyond the randomly decide normalised time and not yet transitioning
        if (stateInfo.normalizedTime > _randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(_hashRandomIdle, Random.Range(0, _numberOfStates + 1));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
