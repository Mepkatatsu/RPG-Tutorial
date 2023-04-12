using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public enum InterfaceType_New
{
    Inventory,
    Equipment,
    QuickSlot,
    Box,
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory_New")]
public class InventoryObject_New : ScriptableObject
{
    public ItemObjectDatabase_New _database;
    public InterfaceType_New _type;

    [SerializeField] private Inventory_New _container = new Inventory_New();

    public InventorySlot_New[] Slots => _container._slots;

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            foreach (InventorySlot_New slot in Slots)
            {
                if (slot._item._id < 0)
                {
                    counter++;
                }
            }

            return counter;
        }
    }

    public bool AddItem(Item_New item, int amount)
    {
        if (EmptySlotCount <= 0)
        {
            return false;
        }

        InventorySlot_New slot = FindItemInInventory(item);
        if (!_database._itemObjects[item._id]._stackable || slot == null)
        {
            GetEmptySlot().AddItem(item, amount);
        }
        else
        {
            slot.AddAmount(amount);
        }

        return true;
    }

    public void Clear()
    {
        _container.Clear();
    }

    public InventorySlot_New FindItemInInventory(Item_New item)
    {
        return Slots.FirstOrDefault(i => i._item._id == item._id);
    }

    public InventorySlot_New GetEmptySlot()
    {
        return Slots.FirstOrDefault(i => i._item._id < 0);
    }

    public bool IsContainItem(ItemObject_New itemObject)
    {
        return Slots.FirstOrDefault(i => i._item._id == itemObject._data._id) != null;
    }

    public void SwapItems(InventorySlot_New itemSlotA, InventorySlot_New itemSlotB)
    {
        if (itemSlotA == itemSlotB)
        {
            return;
        }

        if (itemSlotB.CanPlaceInSlot(itemSlotA.ItemObject) && itemSlotA.CanPlaceInSlot(itemSlotB.ItemObject))
        {
            InventorySlot_New tempSlot = new InventorySlot_New(itemSlotB._item, itemSlotB._amount);
            itemSlotB.UpdateSlot(itemSlotA._item, itemSlotA._amount);
            itemSlotA.UpdateSlot(tempSlot._item, tempSlot._amount);
        }
    }
}
