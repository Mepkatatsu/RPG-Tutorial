using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacing_New : MonoBehaviour
{
    Camera _referenceCamera;
    public bool _reverseFace = false;

    public enum Axis
    {
        up, down, left, right, forward, back
    };

    public Axis _axis = Axis.up;

    public Vector3 GetAxis(Axis refAxis)
    {
        switch(refAxis)
        {
            case Axis.down:
                return Vector3.down;

            case Axis.left:
                return Vector3.back;

            case Axis.right:
                return Vector3.right;

            case Axis.forward:
                return Vector3.forward;

            case Axis.back:
                return Vector3.back;
        }

        return Vector3.up;
    }

    private void Awake()
    {
        if (!_referenceCamera)
        {
            _referenceCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        Vector3 targetPos = transform.position + _referenceCamera.transform.rotation * (_reverseFace ? Vector3.forward : Vector3.back);
        Vector3 targetOrientation = _referenceCamera.transform.rotation * GetAxis(_axis);

        transform.LookAt(targetPos, targetOrientation);
    }
}
