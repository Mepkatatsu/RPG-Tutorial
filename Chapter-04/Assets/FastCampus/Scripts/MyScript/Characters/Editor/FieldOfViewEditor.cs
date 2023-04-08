using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._viewRadius);

        Vector3 viewAngleLeft = fov.DirectionFromAngle(-fov._viewAngle / 2, false);
        Vector3 viewAngleRight = fov.DirectionFromAngle(fov._viewAngle / 2, false);

        //float x = Mathf.Sin(fov._viewAngle / 2 * Mathf.Deg2Rad) * fov._viewRadius;
        //float z = Mathf.Cos(fov._viewAngle / 2 * Mathf.Deg2Rad) * fov._viewRadius;

        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleLeft * fov._viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleRight * fov._viewRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in fov._VisibleTargets)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.position);
        }
    }
}
