using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Inventory_New
{
    public InventorySlot_New[] _slots = new InventorySlot_New[24];

    public void Clear()
    {
        foreach (InventorySlot_New slot in _slots)
        {
            slot.RemoveItem();
        }
    }

    public bool IsContain(ItemObject_New itemObject)
    {
        return IsContain(itemObject._data._id);
//        return Array.Find(_slots, i => i._item._id == itemObject._data._id) != null;
    }

    public bool IsContain(int id)
    {
        return _slots.FirstOrDefault(i => i._item._id == id) != null;
    }
}
