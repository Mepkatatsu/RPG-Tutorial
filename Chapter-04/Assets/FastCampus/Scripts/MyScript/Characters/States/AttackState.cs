using FastCampus.AI;
using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Characters
{
    public class AttackState : State<EnemyController_New>
    {
        private Animator _animator;

        private int _hashAttack = Animator.StringToHash("Attack");

        public override void OnInitialized()
        {
            _animator = _context.GetComponent<Animator>();
        }

        public override void OnEnter()
        {
            if (_context.IsAvailableAttack)
            {
                _animator?.SetTrigger(_hashAttack);
            }
            else
            {
                _stateMachine.ChangeState<IdleState>();
            }
        }

        public override void Update(float deltaTime)
        {
        }
    }
}