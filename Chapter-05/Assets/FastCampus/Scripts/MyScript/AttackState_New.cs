using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.AI
{
    public class AttackState_New : State<EnemyController>
    {
        private Animator animator;
        private AttackStateController_New _attackStateController;
        private IAttackable_New _attackable;

        protected int _attackTriggerHash = Animator.StringToHash("AttackTrigger");
        protected int _attackIndexHash = Animator.StringToHash("AttackIndex");

        public override void OnInitialized()
        {
            animator = context.GetComponent<Animator>();
            _attackStateController = context.GetComponent<AttackStateController_New>();
            _attackable = context.GetComponent<IAttackable_New>();
        }

        public override void OnEnter()
        {
            if (_attackable == null || _attackable._CurrentAttackBehaviour == null)
            {
                stateMachine.ChangeState<IdleState>();
            }

            // _attackStateController._enterAttackStateHandler += OnEnterAttackState();
            // _attackStateController._exitAttackStateHandler += OnExitAttackState();

            animator?.SetInteger(_attackIndexHash, _attackable._CurrentAttackBehaviour._animationIndex);
            animator?.SetTrigger(_attackTriggerHash);
        }

        public void OnEnterAttackState()
        {

        }

        public void OnExitAttackState() 
        {
            stateMachine.ChangeState<IdleState>();
        }

        public override void Update(float deltaTime)
        {
        }
    }
}
