using FastCampus.AI;
using FastCampus.Core;
using FastCampus.UIs;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Characters
{
    public class EnemyController_New : EnemyController, IAttackable_New, IDamageable_New
    {
        #region Variables
        public Transform _projectileTransform;
        public Transform _hitTransform;

        public int _maxHealth = 100;
        public int _health;

        [SerializeField]
        private List<AttackBehaviour_New> _attackBehaviours = new List<AttackBehaviour_New>();
        #endregion Variables

        #region Proeprties

        #endregion Properties

        #region Unity Methods

        protected override void Start()
        {
            base.Start();

            stateMachine.AddState(new MoveState());
            stateMachine.AddState(new AttackState());
            stateMachine.AddState(new DeadState());
            InitAttackBehaviour();

            _health = _maxHealth;
        }

        protected override void Update()
        {
            CheckAttackBehaviour();
            base.Update();
        }

        #endregion Unity Methods

        #region Helper Methods
        private void InitAttackBehaviour()
        {
            foreach (AttackBehaviour_New behaviour in _attackBehaviours)
            {
                if (_CurrentAttackBehaviour == null)
                {
                    _CurrentAttackBehaviour = behaviour;
                }

                behaviour._targetMask = TargetMask;
            }
        }

        private void CheckAttackBehaviour()
        {
            //if (_CurrentAttackBehaviour == null || !_CurrentAttackBehaviour._IsAvailable)
            //{
            //    _CurrentAttackBehaviour = null;

            //    foreach (AttackBehaviour_New behaviour in _attackBehaviours)
            //    {
            //        if (behaviour._IsAvailable)
            //        {
            //            if (_CurrentAttackBehaviour == null || _CurrentAttackBehaviour._priority < behaviour._priority)
            //            {
            //                _CurrentAttackBehaviour = behaviour;
            //            }
            //        }
            //    }
            //}
        }
        #endregion Helper Methods

        #region IAttackble interfaces
        public AttackBehaviour_New _CurrentAttackBehaviour
        {
            get;
            private set;
        }

        public void OnExecuteAttack(int attackIndex)
        {
            if (_CurrentAttackBehaviour != null && Target != null)
            {
                _CurrentAttackBehaviour.ExecuteAttack(Target.gameObject, _projectileTransform);
            }
        }

        #endregion

        #region IDamageable interfaces
        public bool _IsAlive => _health > 0;

        public void TakeDamage(int damage, GameObject hitEffectPrefabs)
        {
            if (!_IsAlive)
            {
                return;
            }

            _health -= damage;

            if (hitEffectPrefabs)
            {
                Instantiate(hitEffectPrefabs, _hitTransform.transform);
            }

            if (_IsAlive)
            {
                //animator?.SetTrigger(_hitTriggetHash);
            }
            else
            {
                stateMachine.ChangeState<DeadState>();
            }
        }
        #endregion
    }
}