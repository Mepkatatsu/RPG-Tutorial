using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateMachineBehaviour_New : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AttackStateController_New>()?.OnStartOfAttackState();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<AttackStateController_New>()?.OnEndOfAttackState();
    }
}
