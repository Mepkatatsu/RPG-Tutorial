using FastCampus.AI;
using FastCampus.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Characters 
{
    public class IdleState : State<EnemyController_New>
    {
        public bool _isPatrol = true;
        private float _minIdleTime = 0.0f;
        private float _maxIdleTime = 3.0f;
        private float _idleTime = 0.0f;

        private Animator _animator;
        private CharacterController _controller;

        protected int _hashMove = Animator.StringToHash("Move");
        protected int _hashMoveSpeed = Animator.StringToHash("MoveSpeed");

        public override void OnInitialized()
        {
            _animator = _context.GetComponent<Animator>();
            _controller = _context.GetComponent<CharacterController>();
        }

        public override void OnEnter()
        {
            _animator?.SetBool(_hashMove, false);
            _animator?.SetFloat(_hashMoveSpeed, 0);
            _controller?.Move(Vector3.zero);

            if (_isPatrol)
            {
                _idleTime = Random.Range(_minIdleTime, _maxIdleTime);
            }
        }

        public override void Update(float deltaTime)
        {
            Transform enemy = _context.SearchEnemy();
            if (enemy)
            {
                if (_context.IsAvailableAttack)
                {
                    _stateMachine.ChangeState<AttackState>();
                }
                else
                {
                    _stateMachine.ChangeState<MoveState>();
                }
            }
            else if (_isPatrol && _stateMachine._ElapsedTimeInState > _idleTime)
            {
                _stateMachine.ChangeState<MoveToWaypoint>();
            }
        }

        public override void OnExit()
        {
        }
    }
}

