using FastCampus.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Characters
{
    public class EnemyController_New : MonoBehaviour
    {
        #region Variables
        protected StateMachine_New<EnemyController_New> _stateMachine;
        public StateMachine_New<EnemyController_New> _StateMachine => _stateMachine;

        public LayerMask _targetMask;
        public Transform _target;
        public float _viewRadius;
        public float _attackRange;

        #endregion Variables

        #region Unity Methods
        private void Start()
        {
            _stateMachine = new StateMachine_New<EnemyController_New>(this, new IdleState());
            _stateMachine.AddState(new MoveState());
            _stateMachine.AddState(new AttackState());
        }

        private void Update()
        {
            _stateMachine.Update(Time.deltaTime);
        }

        #endregion Unity Methods

        #region Other Methods

        public bool IsAvailableAttack
        {
            get
            {
                if (!_target)
                {
                    return false;
                }
                
                float distance = Vector3.Distance(transform.position, _target.position);
                return (distance <= _attackRange);
            }
        }

        public Transform SearchEnemy()
        {
            _target = null;
            Collider[] targetInViewRadius = new Collider[1];

            Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, targetInViewRadius, _targetMask);

            //Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
            if (targetInViewRadius[0])
            {
                _target = targetInViewRadius[0].transform;
            }

            return _target;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _viewRadius);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }

        #endregion Other Methods
    }
}
