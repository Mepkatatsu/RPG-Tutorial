using FastCampus.InventorySystem.Inventory;
using FastCampus.InventorySystem.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem_New : MonoBehaviour
{
    public InventoryObject_New equipmentObject;
    public InventoryObject_New inventoryObject;
    public ItemObjectDatabase_New databaseObject;

    public void AddNewItem()
    {
        if (databaseObject._itemObjects.Length > 0)
        {
            ItemObject_New newItemObject = databaseObject._itemObjects[Random.Range(0, databaseObject._itemObjects.Length)];
            //ItemObject newItemObject = databaseObject.itemObjects[databaseObject.itemObjects.Length - 1];
            Item_New newItem = new Item_New(newItemObject);

            inventoryObject.AddItem(newItem, 1);
        }
    }

    public void ClearInventory()
    {
        equipmentObject?.Clear();
        inventoryObject?.Clear();
    }
}
