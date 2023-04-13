using FastCampus.Datas;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DoorController_New : MonoBehaviour
{
    #region Variables
    public DoorEventObject_New _doorEventObject;

    public float _openOffset = 4f;
    public float _closeOffset = 1f;
    public int _id = 0;


    #endregion Variables

    #region Unity Methods
    private void OnEnable()
    {
        _doorEventObject._OnOpenDoor += OnOpenDoor;
        _doorEventObject._OnCloseDoor += OnCloseDoor;
    }

    private void OnDisable()
    {
        _doorEventObject._OnOpenDoor -= OnOpenDoor;
        _doorEventObject._OnCloseDoor -= OnCloseDoor;
    }
    #endregion Unity Methods

    #region Event Methods
    public void OnOpenDoor(int id)
    {
        if (id != _id)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(OpenDoor());
    }

    public void OnCloseDoor(int id)
    {
        if (id != _id)
        {
            return;
        }

        StopAllCoroutines();
        StartCoroutine(CloseDoor());
    }

    IEnumerator OpenDoor()
    {
        while (transform.position.y < _openOffset)
        {
            Vector3 calcPosition = transform.position;
            calcPosition.y += 0.01f;
            transform.position = calcPosition;

            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        while (transform.position.y > _closeOffset)
        {
            Vector3 calcPosition = transform.position;
            calcPosition.y -= 0.01f;
            transform.position = calcPosition;

            yield return null;
        }
    }
    #endregion Event Methods
}
