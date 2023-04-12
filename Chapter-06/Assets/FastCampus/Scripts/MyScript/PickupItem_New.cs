using FastCampus.Characters;
using FastCampus.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem_New : MonoBehaviour, IInteractable_New
{
    private float _distance = 3.0f;
    public float Distance => _distance;

    public ItemObject_New _itemObject;

    public bool Interact(GameObject other)
    {
        float calcDistance = Vector3.Distance(transform.position, other.transform.position);
        if (calcDistance > _distance)
        {
            return false;
        }

        return other.GetComponent<PlayerCharacter>()?.PickupItem(this) ?? false;
    }

    public void StopInteract(GameObject other)
    {

    }

    private void OnValidate()
    {
#if UNITY_EDITOR
        GetComponent<SpriteRenderer>().sprite = _itemObject?._icon;
#endif
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _distance);
    }
}
