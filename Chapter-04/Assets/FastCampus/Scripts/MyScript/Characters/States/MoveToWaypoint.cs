using FastCampus.AI;
using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FastCampus.Characters 
{
    public class MoveToWaypoint : State<EnemyController_New>
    {
        private Animator _animator;
        private CharacterController _controller;
        private NavMeshAgent _agent;

        protected int _hashMove = Animator.StringToHash("Move");
        protected int _hashMoveSpeed = Animator.StringToHash("MoveSpeed");

        public override void OnInitialized()
        {
            _animator = _context.GetComponent<Animator>();
            _controller = _context.GetComponent<CharacterController>();
            _agent = _context.GetComponent<NavMeshAgent>();
        }

        public override void OnEnter()
        {
            if (_context._targetWaypoint == null)
            {
                _context.FindNextWaypoint();
            }

            if (_context._targetWaypoint)
            {
                _agent.SetDestination(_context._targetWaypoint.position);
                _animator?.SetBool(_hashMove, true);
            }
        }

        public override void Update(float deltaTime)
        {
            Transform enemy = _context.SearchEnemy();
            if (enemy)
            {
                if (_context.IsAvailableAttack)
                {
                    _stateMachine.ChangeState<IdleState>();
                }
                else
                {
                    _stateMachine.ChangeState<IdleState>();
                }
            }
            else
            {
                if (!_agent.pathPending && (_agent.remainingDistance <= _agent.stoppingDistance))
                {
                    Transform nextDestination = _context.FindNextWaypoint();
                    if (nextDestination)
                    {
                        _agent.SetDestination(nextDestination.position);
                    }

                    _stateMachine.ChangeState<IdleState>();
                }
                else
                {
                    _controller.Move(_agent.velocity * Time.deltaTime);
                    _animator.SetFloat(_hashMoveSpeed, _agent.velocity.magnitude / _agent.speed, .1f, deltaTime);
                }
            }
        }

        public override void OnExit()
        {
            _animator?.SetBool(_hashMove, false);
            _agent.ResetPath();
        }
    }
}

