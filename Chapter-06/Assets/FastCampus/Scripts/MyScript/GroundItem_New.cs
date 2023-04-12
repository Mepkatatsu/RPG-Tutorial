using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem_New : MonoBehaviour
{
    public ItemObject_New _itemObject;

    private void OnValidate()
    {
#if UNITY_EDITOR
        GetComponent<SpriteRenderer>().sprite = _itemObject?._icon;
#endif
    }
}
