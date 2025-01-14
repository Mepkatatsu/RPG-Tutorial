﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FastCampus.AI
{
    [CustomEditor(typeof(FieldOfView_ymkim50))]
    public class FieldOfViewEditor_ymkim50 : Editor
    {
        void OnSceneGUI()
        {
            FieldOfView_ymkim50 fov = (FieldOfView_ymkim50)target;

            // Draw view radius
            Handles.color = Color.white;
            Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.viewRadius);

            Vector3 viewAngleA = fov.DirFromAngle(-fov.viewAngle / 2, false);
            Vector3 viewAngleB = fov.DirFromAngle(fov.viewAngle / 2, false);

            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewRadius);

            Handles.color = Color.red;
            foreach (Transform visibleTarget in fov.VisibleTargets)
            {
                if (fov.NearestTarget != visibleTarget)
                {
                    Handles.DrawLine(fov.transform.position, visibleTarget.position);
                }
            }

            Handles.color = Color.green;
            if (fov.NearestTarget)
            {
                Handles.DrawLine(fov.transform.position, fov.NearestTarget.position);
            }
        }
    }
}