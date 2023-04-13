using UnityEngine;


public class DoorTriggerArea_New : MonoBehaviour
{
    public DoorEventObject_New _doorEventObject;
    public DoorController_New _doorController;

    public bool _autoClose = true;

    private void OnTriggerEnter(Collider other)
    {
        _doorEventObject.OpenDoor(_doorController._id);
    }

    private void OnTriggerExit(Collider other)
    {
        if (_autoClose)
        {
            _doorEventObject.CloseDoor(_doorController._id);
        }
    }
}
