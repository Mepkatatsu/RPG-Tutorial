using FastCampus.Characters;
using FastCampus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.TrapSystem
{
    public class TrapController_New : MonoBehaviour
    {
        #region Variables
        public float _damageInterval = 0.5f;
        public float _damageDuration = 5f;
        public int _damage = 5;

        private float _calcDuration = 0.0f;

        [SerializeField]
        private ParticleSystem _effect;

        private IDamagable _damagable;
        #endregion Variables

        #region Unity Methods
        private void Update()
        {
            _calcDuration -= Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            _damagable = other.GetComponent<IDamagable>();
            if (_damagable != null)
            {
                _calcDuration = _damageDuration;

                _effect.Play();
                StartCoroutine(ProcessDamage());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _damagable = null;
            StopAllCoroutines();
            _effect.Stop();
        }
        #endregion Unity Methods

        #region Methods
        IEnumerator ProcessDamage()
        {
            while (_calcDuration > 0 && _damagable != null)
            {
                _damagable.TakeDamage(_damage, null);

                yield return new WaitForSeconds(_damageInterval);
            }

            _damagable = null;
            _effect.Stop();
        }
        #endregion Methods
    }
}