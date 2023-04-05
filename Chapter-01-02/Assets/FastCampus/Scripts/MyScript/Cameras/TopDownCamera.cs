using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FastCampus.Cameras
{
    public class TopDownCamera : MonoBehaviour
    {
        #region Variables
        public float _height = 5f;
        public float _distance = 10;
        public float _angle = 45f;
        public float _lookAtHeight = 2f;
        public float _smoothSpeed = 0.5f;

        private Vector3 _referenceVelocity;

        public Transform _target;
        #endregion Variable

        private void LateUpdate()
        {
            HandleCamera();
        }

        public void HandleCamera()
        {
            if (!_target)
            {
                return;
            }

            // Build world position vector
            Vector3 worldPosition = (Vector3.forward * -_distance) + (Vector3.up * _height);
            //Debug.DrawLine(_target.position, worldPosition, Color.red);

            // Build our rotated vector
            Vector3 rotatedVector = Quaternion.AngleAxis(_angle, Vector3.up) * worldPosition;
            //Debug.DrawLine(_target.position, rotatedVector, Color.green);

            // Move out position
            Vector3 finalTargetPosition = _target.position;
            finalTargetPosition.y += _lookAtHeight;

            Vector3 finalPosition = finalTargetPosition + rotatedVector;
            //Debug.DrawLine(_target.position, finalPosition, Color.blue);

            transform.position = Vector3.SmoothDamp(transform.position, finalPosition, ref _referenceVelocity, _smoothSpeed);

            transform.LookAt(_target.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
            if (_target)
            {
                Vector3 lookAtPosition = _target.position;
                lookAtPosition.y += _lookAtHeight;
                Gizmos.DrawLine(transform.position, lookAtPosition);
                Gizmos.DrawSphere(lookAtPosition, 0.25f);
            }

            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}