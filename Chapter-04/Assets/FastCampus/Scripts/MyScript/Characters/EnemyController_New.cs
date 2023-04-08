using FastCampus.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Characters
{
    public class EnemyController_New : MonoBehaviour
    {
        #region Variables
        protected StateMachine<EnemyController_New> _stateMachine;
        public StateMachine<EnemyController_New> _StateMachine => _stateMachine;

        private FieldOfView _fov;

        //        public LayerMask _targetMask;
        //        public Transform _target;
        //public float _viewRadius;
        public float _attackRange;
        public Transform _Target => _fov?._NearestTarget;

        public Transform[] _waypoints;
        [HideInInspector] public Transform _targetWaypoint = null;
        private int _waypointIndex = 0;

        #endregion Variables

        #region Unity Methods
        private void Start()
        {
            _stateMachine = new StateMachine<EnemyController_New>(this, new MoveToWaypoint());
            IdleState idleState = new IdleState();
            idleState._isPatrol = true;
            _stateMachine.AddState(idleState);
            _stateMachine.AddState(new MoveState());
            _stateMachine.AddState(new AttackState());

            _fov = GetComponent<FieldOfView>();
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
                if (!_Target)
                {
                    return false;
                }
                
                float distance = Vector3.Distance(transform.position, _Target.position);
                return (distance <= _attackRange);
            }
        }

        public Transform SearchEnemy()
        {
            return _Target;
            //_target = null;
            //Collider[] targetInViewRadius = new Collider[1];

            //Physics.OverlapSphereNonAlloc(transform.position, _viewRadius, targetInViewRadius, _targetMask);

            ////Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, _viewRadius, _targetMask);
            //if (targetInViewRadius[0])
            //{
            //    _target = targetInViewRadius[0].transform;
            //}

            //return _target;
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, _viewRadius);

        //    Gizmos.color = Color.green;
        //    Gizmos.DrawWireSphere(transform.position, _attackRange);
        //}

        public Transform FindNextWaypoint()
        {
            _targetWaypoint = null;
            if (_waypoints.Length > 0)
            {
                _targetWaypoint = _waypoints[_waypointIndex];
            }

            _waypointIndex = (_waypointIndex + 1) % _waypoints.Length;

            return _targetWaypoint;
        }

        #endregion Other Methods
    }
}
