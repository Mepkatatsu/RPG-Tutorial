using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event system", menuName = "Event system/Door Event Object_New")]
public class DoorEventObject_New : ScriptableObject
{
    [NonSerialized]
    public Action<int> _OnOpenDoor;

    [NonSerialized]
    public Action<int> _OnCloseDoor;

    public void OpenDoor(int id)
    {
        _OnOpenDoor?.Invoke(id);
    }

    public void CloseDoor(int id)
    {
        _OnCloseDoor?.Invoke(id);
    }
}
