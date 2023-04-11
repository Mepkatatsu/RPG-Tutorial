using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database_New", menuName = "Inventory System/Items/DataBase_New")]
public class ItemObjectDatabase_New : ScriptableObject
{
    public ItemObject_New[] _itemObjects;

    public void OnValidate()
    {
        for (int i = 0; i < _itemObjects.Length; ++i)
        {
            _itemObjects[i]._data._id = i;
        }
    }
}
