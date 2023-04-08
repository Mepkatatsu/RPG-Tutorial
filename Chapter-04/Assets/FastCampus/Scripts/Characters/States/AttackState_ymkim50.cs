using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

namespace FastCampus.AI
{
    public class AttackState_ymkim50 : State_ymkim50<EnemyController_ymkim50>
    {
        private Animator animator;

        protected int hashAttack = Animator.StringToHash("Attack");

        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            if (context.IsAvailableAttack)
            {
                animator?.SetTrigger(hashAttack);
            }
            else
            {
                stateMachine.ChangeState<IdleState_ymkim50>();
            }
        }

        public override void Update(float deltaTime)
        {
        }
    }
}