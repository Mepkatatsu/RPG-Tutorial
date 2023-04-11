using FastCampus.InventorySystem.Inventory;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot_New
{
    public ItemType_New[] _allowedItems = new ItemType_New[0];

    [NonSerialized] public InventoryObject_New _parent;
    [NonSerialized] public GameObject _slotUI;
    [NonSerialized] public Action<InventorySlot_New> _OnPreUpdate;
    [NonSerialized] public Action<InventorySlot_New> _OnPostUpdate;

    public Item_New _item;
    public int _amount;

    public ItemObject_New ItemObject
    {
        get
        {
            return _item._id >= 0 ? _parent._database._itemObjects[_item._id] : null;
        }
    }

    public InventorySlot_New() => UpdateSlot(new Item_New(), 0);
    public InventorySlot_New(Item_New item, int amount) => UpdateSlot(item, amount);

    public void AddItem(Item_New item, int amount) => UpdateSlot(item, amount);
    public void RemoveItem() => UpdateSlot(new Item_New(), 0);

    public void AddAmount(int value) => UpdateSlot(_item, _amount += value);

    public void UpdateSlot(Item_New item, int amount)
    {
        _OnPreUpdate?.Invoke(this);

        _item = item;
        _amount = amount;

        _OnPostUpdate?.Invoke(this);
    }

    public bool CanPlaceInSlot(ItemObject_New itemObejct)
    {
        if (_allowedItems.Length <= 0 || itemObejct == null || itemObejct._data._id < 0)
        {
            return true;
        }

        foreach (ItemType_New type in _allowedItems)
        {
            if (itemObejct._type == type)
            {
                return true;
            }
        }

        return false;
    }
}
