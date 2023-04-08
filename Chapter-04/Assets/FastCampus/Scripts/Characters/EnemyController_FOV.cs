﻿using FastCampus.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FastCampus.Characters
{
    public class EnemyController_FOV : EnemyController_ymkim50
    {
        #region Variables
        protected StateMachine_ymkim50<EnemyController_ymkim50> stateMachine;

        public LayerMask targetMask;
        public Collider weaponCollider;

        public GameObject hitEffect;

        private FieldOfView_ymkim50 fov;

        #endregion Variables

        #region Properties
        public override Transform Target => fov?.NearestTarget;
        #endregion Properties

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            stateMachine = new StateMachine_ymkim50<EnemyController_ymkim50>(this, new IdleState_ymkim50());
            stateMachine.AddState(new MoveState_ymkim50());
            stateMachine.AddState(new AttackState_ymkim50());

            fov = GetComponent<FieldOfView_ymkim50>();
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.Update(Time.deltaTime);

            if (!(stateMachine.CurrentState is MoveState_ymkim50))
            {
                FaceTarget();
            }
        }

        #endregion Unity Methods

        #region Helper Methods
        public R ChangeState<R>() where R : State_ymkim50<EnemyController_ymkim50>
        {
            return stateMachine.ChangeState<R>();
        }

        public override bool IsAvailableAttack
        {
            get
            {
                if (!Target)
                {
                    return false;
                }

                float distance = Vector3.Distance(transform.position, Target.position);
                return (distance <= CalcAttackRange);
            }
        }

        override public Transform SearchEnemy()
        {
            return Target;
        }

        public void FaceTarget()
        {
            if (!Target)
            {
                return;
            }

            Vector3 direction = (Target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }

        public void EnableAttackCollider()
        {
            Debug.Log("Check Attack Event");
            if (weaponCollider)
            {
                weaponCollider.enabled = true;
            }


            StartCoroutine("DisableAttackCollider");
        }

        IEnumerator DisableAttackCollider()
        {
            yield return new WaitForFixedUpdate();

            if (weaponCollider)
            {
                weaponCollider.enabled = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & targetMask) != 0)
            {
                //It matched one
                Debug.Log("Attack Trigger: " + other.name);
                PlayerCharacter playerCharacter = other.gameObject.GetComponent<PlayerCharacter>();
                playerCharacter?.TakeDamage(10, hitEffect);

            }

            if (((1 << other.gameObject.layer) & targetMask) == 0)
            {
                //It wasn't in an ignore layer
            }
        }

        private void OnDrawGizmos()
        {
            
        }
        #endregion Helper Methods
    }
}