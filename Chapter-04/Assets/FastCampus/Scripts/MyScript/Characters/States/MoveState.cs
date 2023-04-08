using FastCampus.AI;
using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FastCampus.Characters
{
    public class MoveState : State<EnemyController_New>
    {
        private Animator _animator;
        private CharacterController _controller;
        private NavMeshAgent _agent;

        private int _hashMove = Animator.StringToHash("Move");
        private int _hashMoveSpeed = Animator.StringToHash("MoveSpeed");

        public override void OnInitialized()
        {
            _animator = _context.GetComponent<Animator>();
            _controller = _context.GetComponent<CharacterController>();
            _agent = _context.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            _agent?.SetDestination(_context._Target.position);
            _animator?.SetBool(_hashMove, true);
        }

        public override void Update(float deltaTime)
        {
            Transform enemy = _context.SearchEnemy();
            if(enemy)
            {
                _agent.SetDestination(_context._Target.position);

                if (_agent.remainingDistance > _agent.stoppingDistance)
                {
                    _controller.Move(_agent.velocity * deltaTime);
                    _animator.SetFloat(_hashMoveSpeed, _agent.velocity.magnitude / _agent.speed, 1f, deltaTime);
                    return;
                }
            }

            if (!enemy || _agent.remainingDistance <= _agent.stoppingDistance)
            {
                _stateMachine.ChangeState<IdleState>();
            }
        }

        public override void OnExit()
        {
            _animator?.SetBool(_hashMove, false);
            _animator?.SetFloat(_hashMoveSpeed, 0f);
            _agent.ResetPath();
        }
    }
}