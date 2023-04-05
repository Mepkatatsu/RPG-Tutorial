using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace FastCampus.Cameras
{
    [CustomEditor(typeof(TopDownCamera))]
    public class TopDownCamera_SceneEditor : Editor
    {
        #region Variables
        private TopDownCamera _targetCamera;
        #endregion Variables

        public override void OnInspectorGUI()
        {
            _targetCamera = (TopDownCamera)target;
            base.OnInspectorGUI();
        }

        private void OnSceneGUI()
        {
            if (!_targetCamera || !_targetCamera._target)
            {
                return;
            }

            Transform cameraTarget = _targetCamera._target;
            Vector3 targetPosition = cameraTarget.position;
            targetPosition.y += _targetCamera._lookAtHeight;

            // Draw distance circle
            Handles.color = new Color(1f, 0f, 0f, 0.15f);
            Handles.DrawSolidDisc(targetPosition, Vector3.up, _targetCamera._distance);

            Handles.color = new Color(0f, 1f, 0f, 0.75f);
            Handles.DrawWireDisc(targetPosition, Vector3.up, _targetCamera._distance);

            // Create slider handles to adjust camera properties
            Handles.color = new Color(1f, 0f, 0f, 0.5f);
            _targetCamera._distance = Handles.ScaleSlider(_targetCamera._distance, targetPosition, -cameraTarget.forward, Quaternion.identity, _targetCamera._distance, 0.1f);

            _targetCamera._distance = Mathf.Clamp(_targetCamera._distance, 2f, float.MaxValue);

            Handles.color = new Color(0f, 0f, 1f, 0.5f);
            _targetCamera._height = Handles.ScaleSlider(_targetCamera._height, targetPosition, Vector3.up, Quaternion.identity, _targetCamera._height, 0.1f);
            _targetCamera._height = Mathf.Clamp(_targetCamera._height, 2f, float.MaxValue);

            // Create Labels
            GUIStyle labelStyle = new GUIStyle();
            labelStyle.fontSize = 15;
            labelStyle.normal.textColor = Color.white;
            labelStyle.alignment = TextAnchor.UpperCenter;

            Handles.Label(targetPosition + (-cameraTarget.forward * _targetCamera._distance), "Distance", labelStyle);

            labelStyle.alignment = TextAnchor.MiddleRight;
            Handles.Label(targetPosition + (Vector3.up * _targetCamera._height), "Height", labelStyle);

            _targetCamera.HandleCamera();
        }
    }

}